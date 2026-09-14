using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Long-running HttpClient 設定（適合 Ollama 可能較長的生成時間）
builder.Services.AddHttpClient("LongRunningApi", c =>
{
    c.Timeout = TimeSpan.FromMinutes(10);
})
.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
{
    PooledConnectionLifetime = TimeSpan.FromMinutes(5),
    EnableMultipleHttp2Connections = true
});

// CORS（允許前端跨域）
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
//app.UseHttpsRedirection();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

//app.MapGet("/api/refresh", () => Results.Ok("Refreshed"));
app.MapGet("/api/refresh", () => { 
    Console.WriteLine($"[{DateTime.Now}] 收到 /api/refresh 請求，重新整理應用程式狀態");
    return Results.Ok("Refreshed"); } );


// ============================================================
// SSE 串流聊天端點
// 多圖片 OCR：逐張圖片送給 Ollama，避免只辨識第一張
// ============================================================
app.MapGet("/chat/stream", async (
    HttpContext ctx,
    string data,
    IHttpClientFactory httpFactory,
    IConfiguration config) =>
{
    Console.WriteLine($"收到 SSE 請求，data 長度: {data?.Length ?? 0}");

    ctx.RequestAborted.Register(() =>
    {
        Console.WriteLine("========== SSE CLIENT DISCONNECTED ==========");
    });

    if (string.IsNullOrEmpty(data))
        return Results.BadRequest("缺少 data 參數");

    // --------------------------------------------------------
    // 1. 解析 Payload
    // --------------------------------------------------------
    ChatPayload? req = null;

    try
    {
        var decodedData = Uri.UnescapeDataString(data);

        Console.WriteLine("解碼後: " + decodedData);

        req = JsonSerializer.Deserialize<ChatPayload>(decodedData);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Payload 解析失敗: {ex.Message}");
        Console.WriteLine("無效的 payload 格式");
        return Results.BadRequest("無效的 payload 格式");
    }

    if (req == null || string.IsNullOrWhiteSpace(req.ChatModel))
    {
        Console.WriteLine("缺少 ChatModel");
        return Results.BadRequest("缺少 ChatModel");
    }
    Console.WriteLine($"ChatModel: {req.ChatModel}");

    if (req == null || string.IsNullOrWhiteSpace(req.OcrModel))
    {
        Console.WriteLine("缺少 OCRmodel");        
        return Results.BadRequest("缺少 OCRmodel");
    }
    Console.WriteLine($"OCRmodel: {req.OcrModel}");

    // --------------------------------------------------------
    // 2. SSE Header
    // --------------------------------------------------------
    ctx.Response.Headers["Content-Type"] = "text/event-stream";
    ctx.Response.Headers["Cache-Control"] = "no-cache";
    ctx.Response.Headers["Connection"] = "keep-alive";


    // --------------------------------------------------------
    // 3. Ollama 設定
    // --------------------------------------------------------
    var ollamaBaseUrl =
        config["Ollama:BaseUrl"]
        ?? "http://localhost:11434";

    var client = httpFactory.CreateClient("LongRunningApi");

    string textContent =
        string.IsNullOrWhiteSpace(req.Text)
        ? "請自我介紹。"
        : req.Text;


    // --------------------------------------------------------
    // 4. 取得所有圖片 Base64
    // --------------------------------------------------------
    var imagesBase64 = new List<string>();

    var fileIds = req.FileIds ?? Array.Empty<string>();

    Console.WriteLine($"收到圖片數量: {fileIds.Length}");

    foreach (var fid in fileIds)
    {
        if (string.IsNullOrWhiteSpace(fid))
            continue;

        var dir = Path.Combine(Path.Combine(AppContext.BaseDirectory, "uploads"), fid);
        var infoPath = Path.Combine(dir, "info.json");

        Console.WriteLine($"處理 FileId: {fid}");

        if (!File.Exists(infoPath))
        {
            Console.WriteLine($"找不到 info.json: {infoPath}");
            continue;
        }

        InitRequest? info;

        try
        {
            var infoJson = await File.ReadAllTextAsync(
                infoPath,
                ctx.RequestAborted);

            info = JsonSerializer.Deserialize<InitRequest>(infoJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"讀取 info.json 失敗 {infoPath}: {ex.Message}");

            continue;
        }

        if (info == null)
        {
            Console.WriteLine($"info.json 無法解析: {infoPath}");
            continue;
        }

        var ext = Path.GetExtension(info.FileName)
            .ToLowerInvariant()
            .TrimStart('.');

        if (string.IsNullOrWhiteSpace(ext))
        {
            ext = "jpg";
        }

        var finalPath =
            Path.Combine(Path.Combine(AppContext.BaseDirectory, "uploads"), $"{fid}.{ext}");

        Console.WriteLine($"圖片路徑: {finalPath}");

        if (!File.Exists(finalPath))
        {
            Console.WriteLine(
                $"上傳檔案不存在: {finalPath}");

            await SendSseAsync(
                ctx,
                new
                {
                    error = $"上傳檔案不存在: {finalPath}"
                });

            continue;
        }

        try
        {
            var bytes = await File.ReadAllBytesAsync(
                finalPath,
                ctx.RequestAborted);

            var base64 = Convert.ToBase64String(bytes);

            imagesBase64.Add(base64);

            Console.WriteLine(
                $"圖片讀取成功: {info.FileName}, " +
                $"大小: {bytes.Length:N0} bytes");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"讀取圖片失敗 {finalPath}: {ex.Message}");
        }
    }


    // --------------------------------------------------------
    // 5. 如果沒有圖片 → 一般文字聊天
    // --------------------------------------------------------
    if (imagesBase64.Count == 0)
    {
        Console.WriteLine("沒有圖片，執行一般文字聊天");

        await StreamOllamaAsync(
            ctx,
            client,
            ollamaBaseUrl,
            req.ChatModel,
            req.OcrModel,
            textContent,
            null,String.IsNullOrEmpty(config["MCP:Url"])?null: config["MCP:Url"].Trim());

        await SendDoneAsync(ctx);

        return Results.Empty;
    }


    // --------------------------------------------------------
    // 6. 多圖片：逐張處理
    // --------------------------------------------------------
    Console.WriteLine(
        $"開始逐張 OCR，共 {imagesBase64.Count} 張圖片");


    for (int i = 0; i < imagesBase64.Count; i++)
    {        
       
        if (ctx.RequestAborted.IsCancellationRequested)
            break;         

        var imageBase64 = imagesBase64[i];

        Console.WriteLine(
            $"========== 開始處理第 {i + 1} / " +
            $"{imagesBase64.Count} 張 ==========");


        // ----------------------------------------------------
        // 顯示圖片編號
        // ----------------------------------------------------
        var header =
            $"\n\n===== 第 {i + 1} 張圖片 =====\n\n";

        await SendSseDeltaAsync(ctx, header);


        try
        {
            // =================================================
            // ★ 每次只傳一張圖片
            // =================================================
        
            await StreamOllamaAsync(
             ctx,
             client,
             ollamaBaseUrl,
             req.ChatModel,
             req.OcrModel,
             textContent,
             imageBase64,
             null);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"第 {i + 1} 張圖片發生錯誤: {ex}");


            // ★ 不讓第二張錯誤直接讓整個 Request 崩潰

            if (!ctx.RequestAborted.IsCancellationRequested)
            {
                await SendSseAsync(
                    ctx,
                    new
                    {
                        error =
                            $"第 {i + 1} 張圖片處理失敗: {ex.Message}"
                    });
            }
            continue;
        }


        Console.WriteLine(
            $"========== 第 {i + 1} 張處理完成 ==========");
    }


    // --------------------------------------------------------
    // 7. 所有圖片完成後才送 [DONE]
    // --------------------------------------------------------
    if (!ctx.RequestAborted.IsCancellationRequested)
    {
        await SendDoneAsync(ctx);
    }


    // --------------------------------------------------------
    // 8. 清理本次 Request 使用的檔案
    // --------------------------------------------------------
    //
    // 不再使用：
    //
    // Directory.Delete("uploads", true)
    //
    // 避免多使用者同時上傳時，
    // A 使用者把 B 使用者的圖片一起刪掉。
    //
    // --------------------------------------------------------
    foreach (var fid in fileIds)
    {
        if (string.IsNullOrWhiteSpace(fid))
            continue;

        try
        {
            var dir = Path.Combine(Path.Combine(AppContext.BaseDirectory, "uploads"), fid);

            var infoPath =
                Path.Combine(dir, "info.json");

            string? extension = null;

            if (File.Exists(infoPath))
            {
                try
                {
                    var infoJson =
                        await File.ReadAllTextAsync(infoPath);

                    var info =
                        JsonSerializer.Deserialize<InitRequest>(
                            infoJson);

                    if (info != null)
                    {
                        extension =
                            Path.GetExtension(info.FileName)
                                .ToLowerInvariant()
                                .TrimStart('.');
                    }
                }
                catch
                {
                    // 清理階段解析失敗可忽略
                }
            }

            if (!string.IsNullOrWhiteSpace(extension))
            {
                var finalPath =
                    Path.Combine(
                        Path.Combine(AppContext.BaseDirectory, "uploads"),
                        $"{fid}.{extension}");

                if (File.Exists(finalPath))
                {
                    File.Delete(finalPath);
                }
            }

            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
            }

            Console.WriteLine(
                $"已清理 FileId: {fid}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"清理 FileId {fid} 失敗: {ex.Message}");
        }
    }


    return Results.Empty;
})
.WithName("ChatStream");


static async Task StreamOllamaAsync(
    HttpContext ctx,
    HttpClient client,
    string ollamaBaseUrl,
    string Chatmodel,
    string OCRmodel,
    string textContent,
    string? imageBase64,
    string? mcpUrl)                     // ← 新增參數
{
    if (ctx.RequestAborted.IsCancellationRequested)
        return;

    McpClient? mcpClient = null;

    try
    {
        // ========================================================
        // 1. 建立 Ollama Payload / 決定走哪條路
        // ========================================================

        object payload;

        // --------------------------------------------------------
        // 有圖片 → OCR（永遠不使用 MCP）
        // --------------------------------------------------------
        if (!string.IsNullOrEmpty(imageBase64))
        {
            payload = new
            {
                model = OCRmodel,
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = textContent,
                        images = new[] { imageBase64 }
                    }
                },
                options = new
                {
                    temperature = 0.0,
                    num_predict = 4096
                },
                stream = true
            };

            Console.WriteLine(
                $"呼叫 Ollama，OcrModel={OCRmodel}, Image=YES");

            // 直接走後面共用的 streaming 邏輯
            await StreamOllamaResponseAsync(
                ctx, client, ollamaBaseUrl, payload);
            return;
        }

        // --------------------------------------------------------
        // 一般聊天
        // --------------------------------------------------------

        // ========== 無 MCP ==========
        if (string.IsNullOrWhiteSpace(mcpUrl))
        {
            Console.WriteLine("[Chat] No MCP");

            payload = new
            {
                model = Chatmodel,
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = textContent
                    }
                },
                options = new
                {
                    temperature = 0.0,
                    num_predict = 4096
                },
                stream = true
            };

            await StreamOllamaResponseAsync(
                ctx, client, ollamaBaseUrl, payload);
            return;
        }

        // ========== 有 MCP ==========
        Console.WriteLine($"[MCP] Connecting: {mcpUrl}");

        mcpClient = await McpHelper.CreateClientAsync(
            mcpUrl,
            ctx.RequestAborted);

        var mcpTools = await mcpClient.ListToolsAsync(
            cancellationToken: ctx.RequestAborted);

        Console.WriteLine($"[MCP] Tools: {mcpTools.Count}");

        var ollamaTools = McpHelper.ToOllamaTools(mcpTools);

        var messages = new List<object>
        {
            new
            {
                role = "user",
                content = textContent
            }
        };

        const int maxToolRounds = 10;

        // ========================================================
        // MCP Tool Calling Loop
        // ========================================================
        for (int round = 0; round < maxToolRounds; round++)
        {
            if (ctx.RequestAborted.IsCancellationRequested)
                return;

            Console.WriteLine($"[MCP] Ollama round {round + 1}");

            payload = new
            {
                model = Chatmodel,
                messages = messages.ToArray(),
                tools = ollamaTools,
                options = new
                {
                    temperature = 0.0,
                    num_predict = 4096
                },
                stream = true
            };

            var json = JsonSerializer.Serialize(payload);

            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{ollamaBaseUrl}/api/chat");

            request.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            using var response = await client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                ctx.RequestAborted);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(
                    ctx.RequestAborted);

                throw new Exception(
                    $"Ollama HTTP {(int)response.StatusCode}: {errorBody}");
            }

            await using var stream = await response.Content.ReadAsStreamAsync(
                ctx.RequestAborted);

            using var reader = new StreamReader(stream);

            string? line;
            var toolCalls = new List<OllamaToolCallData>();
            var assistantText = new StringBuilder();

            while ((line = await reader.ReadLineAsync(ctx.RequestAborted)) != null)
            {
                if (ctx.RequestAborted.IsCancellationRequested)
                    return;

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                try
                {
                    using var doc = JsonDocument.Parse(line);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("message", out var message))
                    {
                        // 一般文字
                        if (message.TryGetProperty("content", out var contentToken))
                        {
                            var delta = contentToken.GetString();
                            if (!string.IsNullOrEmpty(delta))
                            {
                                assistantText.Append(delta);
                                await SendSseDeltaAsync(ctx, delta);
                            }
                        }

                        // Tool Calls
                        if (message.TryGetProperty("tool_calls", out var toolCallsToken) &&
                            toolCallsToken.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var toolCall in toolCallsToken.EnumerateArray())
                            {
                                if (!toolCall.TryGetProperty("function", out var function))
                                    continue;

                                var name = function.TryGetProperty("name", out var nameToken)
                                    ? nameToken.GetString()
                                    : null;

                                if (string.IsNullOrWhiteSpace(name))
                                    continue;

                                var argumentsJson = function.TryGetProperty("arguments", out var argsToken)
                                    ? argsToken.GetRawText()
                                    : "{}";

                                toolCalls.Add(new OllamaToolCallData
                                {
                                    Name = name,
                                    ArgumentsJson = argumentsJson                                
                                });

                                Console.WriteLine($"[MCP] Tool call: {name}");
                                Console.WriteLine($"[MCP] Arguments: {argumentsJson}");
                            }
                        }
                    }

                    if (root.TryGetProperty("done", out var doneToken) &&
                        doneToken.ValueKind == JsonValueKind.True)
                    {
                        break;
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine(
                        $"解析 Ollama JSON 失敗: {ex.Message} → {line}");
                }
            }

            // 沒有 Tool Call → 完成
            if (toolCalls.Count == 0)
                return;

            // 加入 assistant 的 tool_calls message
            var assistantToolCalls = new List<object>();

            foreach (var call in toolCalls)
            {
                object arguments;
                try
                {
                    arguments = JsonSerializer.Deserialize<object>(call.ArgumentsJson)
                                ?? new Dictionary<string, object?>();
                }
                catch
                {
                    arguments = new Dictionary<string, object?>();
                }

                assistantToolCalls.Add(new
                {
                    function = new
                    {
                        name = call.Name,
                        arguments
                    }
                });
            }

            messages.Add(new
            {
                role = "assistant",
                content = assistantText.ToString(),
                tool_calls = assistantToolCalls.ToArray()
            });

            // 執行 MCP Tools
            foreach (var call in toolCalls)
            {
                Dictionary<string, object?> arguments;
                try
                {
                    arguments = JsonSerializer.Deserialize<Dictionary<string, object?>>(
                                    call.ArgumentsJson)
                                ?? new Dictionary<string, object?>();
                }
                catch
                {
                    arguments = new Dictionary<string, object?>();
                }
               
                var toolResult = await McpHelper.ExecuteToolAsync(
                    mcpClient,
                    call.Name,
                    arguments,
                    ctx.RequestAborted);

                messages.Add(new
                {
                    role = "tool",
                    content = toolResult
                });
            }

            // 繼續下一輪
        }

        // 超過最大輪數
        await SendSseAsync(ctx, new
        {
            error = "MCP tool calling exceeded maximum rounds."
        });
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Ollama / MCP 串流被取消");
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"呼叫 Ollama 失敗: {ex.Message}");

        if (!ctx.RequestAborted.IsCancellationRequested)
        {
            await SendSseAsync(ctx, new
            {
                error = $"呼叫 Ollama 失敗: {ex.Message}"
            });
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ollama / MCP 串流例外: {ex}");

        if (!ctx.RequestAborted.IsCancellationRequested)
        {
            await SendSseAsync(ctx, new
            {
                error = "Ollama / MCP 串流例外: " + ex.Message
            });
        }
    }
    finally
    {
        if (mcpClient != null)
        {
            try
            {
                await mcpClient.DisposeAsync();
            }
            catch
            {
            }
        }
    }
}

/// <summary>
/// 共用的 Ollama Streaming 邏輯（OCR 與 無 MCP 聊天共用）
/// </summary>
static async Task StreamOllamaResponseAsync(
    HttpContext ctx,
    HttpClient client,
    string ollamaBaseUrl,
    object payload)
{
    var payloadJson = JsonSerializer.Serialize(payload);

    using var request = new HttpRequestMessage(
        HttpMethod.Post,
        $"{ollamaBaseUrl}/api/chat");

    request.Content = new StringContent(
        payloadJson,
        Encoding.UTF8,
        "application/json");

    using var response = await client.SendAsync(
        request,
        HttpCompletionOption.ResponseHeadersRead,
        ctx.RequestAborted);

    if (!response.IsSuccessStatusCode)
    {
        var errorBody = await response.Content.ReadAsStringAsync(
            ctx.RequestAborted);

        throw new Exception(
            $"Ollama HTTP {(int)response.StatusCode}: {errorBody}");
    }

    await using var stream = await response.Content.ReadAsStreamAsync(
        ctx.RequestAborted);

    using var reader = new StreamReader(stream);

    string? line;

    while ((line = await reader.ReadLineAsync(ctx.RequestAborted)) != null)
    {
        if (ctx.RequestAborted.IsCancellationRequested)
            break;

        if (string.IsNullOrWhiteSpace(line))
            continue;

        try
        {
            using var doc = JsonDocument.Parse(line);
            var root = doc.RootElement;

            if (root.TryGetProperty("message", out var message) &&
                message.TryGetProperty("content", out var contentToken))
            {
                var delta = contentToken.GetString();
                if (!string.IsNullOrEmpty(delta))
                {
                    await SendSseDeltaAsync(ctx, delta);
                }
            }

            if (root.TryGetProperty("done", out var doneToken) &&
                doneToken.ValueKind == JsonValueKind.True)
            {
                break;
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine(
                $"解析 Ollama JSON 失敗: {ex.Message} → {line}");
        }
    }
}

// ============================================================
// SSE：送出 delta
// ============================================================
static async Task SendSseDeltaAsync(
    HttpContext ctx,
    string text)
{
    
    if (ctx.RequestAborted.IsCancellationRequested)
        return;

    // ★ 不要自己 Replace "\"、"\n"
    // ★ 使用 JsonSerializer 自動處理：
    //    "
    //    \
    //    \r
    //    \n
    //    Unicode
    //
    // 這樣 OCR 遇到 JSON 特殊字元也不會破壞 SSE JSON。
    //
    var json = JsonSerializer.Serialize(
        new
        {
            delta = text
        });


    await ctx.Response.WriteAsync(
        $"data: {json}\n\n",
        ctx.RequestAborted);

    await ctx.Response.Body.FlushAsync(
       ctx.RequestAborted);

    try
    {

        // 避免前端一次收到大量內容
        await Task.Delay(
            20,
            ctx.RequestAborted);
    }
    catch
    {

    }
}


// ============================================================
// SSE：送出一般 JSON
// ============================================================
static async Task SendSseAsync(
    HttpContext ctx,
    object data)
{
    
    if (ctx.RequestAborted.IsCancellationRequested)
        return;
   

    var json =
        JsonSerializer.Serialize(data);

    await ctx.Response.WriteAsync(
        $"data: {json}\n\n",
        ctx.RequestAborted);

    await ctx.Response.Body.FlushAsync(
         ctx.RequestAborted);    
}


// ============================================================
// SSE：全部完成
// ============================================================
static async Task SendDoneAsync(
    HttpContext ctx)
{
    if (ctx.RequestAborted.IsCancellationRequested)
        return;

    await ctx.Response.WriteAsync(
        "data: [DONE]\n\n",
        ctx.RequestAborted);

    await ctx.Response.Body.FlushAsync(
        ctx.RequestAborted);
}

app.Run();

// ────────────────────────────────────────────────
// 資料模型
// ────────────────────────────────────────────────

record ChatPayload(
    string ChatModel,
    string OcrModel,
    string? Text,
    string[]? FileIds);

public class InitRequest
{
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string MimeType { get; set; } = string.Empty;
}
sealed class OllamaToolCallData
{
    public string Name { get; set; } = "";

    public string ArgumentsJson { get; set; } = "{}";
}

public static class McpHelper
{
    public const string DefaultMcpUrl = "http://localhost:5000/mcp";

    // ============================================================
    // 建立 MCP Client
    // Streamable HTTP / AutoDetect
    // ============================================================
    public static async Task<McpClient> CreateClientAsync(
        string mcpUrl = DefaultMcpUrl,
        CancellationToken ct = default)
    {
        var transport = new HttpClientTransport(
            new HttpClientTransportOptions
            {
                Endpoint = new Uri(mcpUrl),

                // AutoDetect:
                // 先嘗試 Streamable HTTP
                // 不支援時再嘗試 legacy SSE
                //
                // 如果你的 MCP Server 確定是 Streamable HTTP，
                // 可以改成：
                //
                // TransportMode = HttpTransportMode.StreamableHttp

                ConnectionTimeout = TimeSpan.FromSeconds(30)
            });

        return await McpClient.CreateAsync(
            transport,
            cancellationToken: ct);
    }


    // ============================================================
    // MCP tools
    //
    // MCP:
    //   McpClientTool
    //
    // 轉成 Ollama:
    //
    // {
    //   "type": "function",
    //   "function": {
    //      "name": "...",
    //      "description": "...",
    //      "parameters": {...}
    //   }
    // }
    // ============================================================
    public static object[] ToOllamaTools(
        IList<McpClientTool> mcpTools)
    {
        return mcpTools
            .Select(t =>
            {
                object parameters;

                try
                {
                    parameters =
                        JsonSerializer.Deserialize<object>(
                            t.ProtocolTool.InputSchema.GetRawText())
                        ??
                        new
                        {
                            type = "object",
                            properties = new { }
                        };
                }
                catch
                {
                    parameters =
                        new
                        {
                            type = "object",
                            properties = new { }
                        };
                }

                return (object)new
                {
                    type = "function",

                    function = new
                    {
                        name = t.Name,

                        description =
                            string.IsNullOrWhiteSpace(t.Description)
                                ? t.Name
                                : t.Description,

                        parameters
                    }
                };
            })
            .ToArray();
    }


    // ============================================================
    // 執行 MCP Tool
    // ============================================================
    public static async Task<string> ExecuteToolAsync(
        McpClient client,
        string toolName,
        Dictionary<string, object?> arguments,
        CancellationToken ct = default)
    {
        try
        {
            Console.WriteLine(
                $"[MCP] Calling tool: {toolName}");

            Console.WriteLine(
                $"[MCP] Arguments: " +
                $"{JsonSerializer.Serialize(arguments)}");

            var result =
                await client.CallToolAsync(
                    toolName,
                    arguments,
                    cancellationToken: ct);

            var parts = new List<string>();

            if (result.Content != null)
            {
                foreach (var block in result.Content)
                {
                    if (block is TextContentBlock text)
                    {
                        parts.Add(text.Text ?? "");
                    }
                    else
                    {
                        parts.Add(
                            JsonSerializer.Serialize(block));
                    }
                }
            }

            var output =
                string.Join("\n", parts);

            if (result.IsError == true)
            {
                Console.WriteLine(
                    $"[MCP] Tool error: {output}");

                return
                    $"[Tool Error] {output}";
            }

            Console.WriteLine(
                $"[MCP] Tool result: {output}");

            return output;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine(
                $"[MCP] Tool cancelled: {toolName}");

            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"[MCP] Tool exception: {ex}");

            return
                $"[Tool Exception] {ex.Message}";
        }
    }
}
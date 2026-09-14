using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

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
        return Results.BadRequest("無效的 payload 格式");
    }

    if (req == null || string.IsNullOrWhiteSpace(req.Model))
        return Results.BadRequest("缺少 model");


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
            req.Model,
            textContent,
            null);

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
             req.Model,
             textContent,
             imageBase64);
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


// ============================================================
// Ollama 串流
// imageBase64 != null → Vision/OCR
// imageBase64 == null → 純文字
// ============================================================
static async Task StreamOllamaAsync(
    HttpContext ctx,
    HttpClient client,
    string ollamaBaseUrl,
    string model,
    string textContent,
    string? imageBase64)
{
    if (ctx.RequestAborted.IsCancellationRequested)
        return;


    // --------------------------------------------------------
    // 建立 Ollama Payload
    // --------------------------------------------------------
    object payload;

    if (!string.IsNullOrEmpty(imageBase64))
    {
        payload = new
        {
            model = model,

            messages = new[]
            {
                new
                {
                    role = "user",
                    content = textContent,

                    // ★ 每次只傳一張圖片
                    images = new[]
                    {
                        imageBase64
                    }
                }
            },

            options = new
            {
                temperature = 0.0,
                num_predict = 4096
            },

            stream = true
        };
    }
    else
    {
        payload = new
        {
            model = model,

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
    }


    // --------------------------------------------------------
    // Serialize JSON
    // --------------------------------------------------------
    var json = JsonSerializer.Serialize(payload);

    Console.WriteLine(
        $"呼叫 Ollama，Model={model}, " +
        $"Image={(imageBase64 != null ? "YES" : "NO")}");


    using var request = new HttpRequestMessage(
        HttpMethod.Post,
        $"{ollamaBaseUrl}/api/chat");

    request.Content = new StringContent(
        json,
        Encoding.UTF8,
        "application/json");


    HttpResponseMessage? response = null;

    try
    {
        // ★ ResponseHeadersRead 非常重要
        // 不要等整個 AI 回覆完成才取得 Response
        response = await client.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            ctx.RequestAborted);


        // ----------------------------------------------------
        // HTTP Error
        // ----------------------------------------------------
        if (!response.IsSuccessStatusCode)
        {
            var errorBody =
                await response.Content.ReadAsStringAsync(
                    ctx.RequestAborted);

            /*
            Console.WriteLine(
                $"Ollama HTTP {(int)response.StatusCode}: " +
                errorBody);

            await SendSseAsync(
                ctx,
                new
                {
                    error =
                        $"Ollama HTTP {(int)response.StatusCode}: " +
                        errorBody
                });

            return;
            */
            throw new Exception($"Ollama HTTP {(int)response.StatusCode}: "+ errorBody);
        }


        // ----------------------------------------------------
        // 讀取 Ollama NDJSON Stream
        // ----------------------------------------------------
        await using var stream =
            await response.Content.ReadAsStreamAsync(
                ctx.RequestAborted);

        using var reader =
            new StreamReader(stream);


        string? line;

        while (
            (line = await reader.ReadLineAsync(
                ctx.RequestAborted)) != null)
        {
            if (ctx.RequestAborted.IsCancellationRequested)
                break;

            if (string.IsNullOrWhiteSpace(line))
                continue;


            try
            {
                using var doc =
                    JsonDocument.Parse(line);

                var root = doc.RootElement;


                // ------------------------------------------------
                // message.content
                // ------------------------------------------------
                if (
                    root.TryGetProperty(
                        "message",
                        out var message) &&
                    message.TryGetProperty(
                        "content",
                        out var contentToken))
                {
                    var delta =
                        contentToken.GetString();

                    if (!string.IsNullOrEmpty(delta))
                    {
                        await SendSseDeltaAsync(
                            ctx,
                            delta);
                    }
                }


                // ------------------------------------------------
                // done
                // ------------------------------------------------
                if (
                    root.TryGetProperty(
                        "done",
                        out var doneToken) &&
                    doneToken.ValueKind ==
                        JsonValueKind.True)
                {
                    break;
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine(
                    $"解析 Ollama JSON 失敗: " +
                    $"{ex.Message} → {line}");
            }
        }
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine(
            "Ollama 串流被取消");
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine(
            $"呼叫 Ollama 失敗: {ex.Message}");
        
        if (!ctx.RequestAborted.IsCancellationRequested)
        {
            await SendSseAsync(
                ctx,
                new
                {
                    error =
                        $"呼叫 Ollama 失敗: {ex.Message}"
                });
        }
        
        //throw new Exception($"呼叫 Ollama 失敗: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"Ollama 串流例外: {ex}");

         
        if (!ctx.RequestAborted.IsCancellationRequested)
        {
            await SendSseAsync(
                ctx,
                new
                {
                    error = "Ollama 串流例外: " + ex.Message
                });
        }
         
        //throw new Exception("Ollama 串流例外: " + ex.Message);
    }
    finally
    {
        try
        {
            response?.Dispose();
        }
        catch
        {

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
    string Model,
    string? Text,
    string[]? FileIds);

public class InitRequest
{
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string MimeType { get; set; } = string.Empty;
}
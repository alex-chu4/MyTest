using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class OcrController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OcrController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost("init")]
    public IActionResult InitUpload([FromBody] InitRequest req)
    {
        string fileId = Guid.NewGuid().ToString("N");
        var dir = Path.Combine(Path.Combine(AppContext.BaseDirectory, "uploads"), fileId);        

        try
        {

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var infoPath = Path.Combine(dir, "info.json");
            Console.WriteLine(infoPath);
            System.IO.File.WriteAllText(infoPath, JsonSerializer.Serialize(req));            
        }
        catch (Exception e)
        {
            Console.WriteLine("InitUpload Error --> " + e.Message + $" (UploadDir: {dir})");
            return StatusCode(500, new { success = false, message = "初始化上傳失敗: " + e.Message + $" (UploadDir: {dir})" });
        }
        return Ok(new { fileId });
    }

    public class InitRequest
    {
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string MimeType { get; set; } = string.Empty;
    }

    [HttpPost("chunk")]
    [RequestSizeLimit(20 * 1024 * 1024)]
    public async Task<IActionResult> UploadChunk([FromForm] ChunkUploadRequest request)
    {
        if (request.chunk == null || request.chunk.Length == 0)
            return BadRequest(new { success = false, message = "缺少 chunk" });

        if (string.IsNullOrWhiteSpace(request.fileId))
            return BadRequest(new { success = false, message = "缺少 fileId" });

        var dir = Path.Combine(Path.Combine(AppContext.BaseDirectory, "uploads"), request.fileId);
        if (!Directory.Exists(dir)) return BadRequest(new { success = false, message = "無效 fileId" });

        var chunkPath = Path.Combine(dir, $"{request.chunkIndex:D5}.chunk");

        await using (var fs = new FileStream(chunkPath, FileMode.Create))
        {
            await request.chunk.CopyToAsync(fs);
        }

        // ── 新增：如果是最後一塊，自動合併 ────────────────────────────────
        if (request.chunkIndex == request.totalChunks - 1)
        {
            try
            {
                var infoPath = Path.Combine(dir, "info.json");
                if (!System.IO.File.Exists(infoPath))
                    return Ok(new { success = true, message = "最後一塊已儲存，但缺少 metadata" });

                var metadata = JsonSerializer.Deserialize<InitRequest>(System.IO.File.ReadAllText(infoPath));
                var ext = Path.GetExtension(metadata.FileName).ToLowerInvariant();

                var finalPath = Path.Combine(Path.Combine(AppContext.BaseDirectory, "uploads"), $"{request.fileId}{ext}");

                // 合併所有 chunk
                var chunkFiles = Directory.GetFiles(dir, "*.chunk")
                                         .OrderBy(f => Path.GetFileName(f))
                                         .ToList();

                if (chunkFiles.Count != request.totalChunks)
                    return Ok(new { success = true, message = "最後一塊已儲存，但分塊數量不完整" });

                await using (var finalStream = new FileStream(finalPath, FileMode.Create, FileAccess.Write))
                {
                    foreach (var chunkFile in chunkFiles)
                    {
                        await using var cs = new FileStream(chunkFile, FileMode.Open, FileAccess.Read);
                        await cs.CopyToAsync(finalStream);
                    }
                }

                // 可選：合併成功後刪除臨時 chunk 檔案，節省空間
                // foreach (var chunkFile in chunkFiles) System.IO.File.Delete(chunkFile);

                // 回傳合併成功的訊息（前端可依此做進一步處理）
                return Ok(new { success = true, merged = true, finalPath = $"{request.fileId}{ext}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "合併失敗: " + ex.Message });
            }
        }

        await Task.Delay(100); // 模擬處理時間
        return Ok(new { success = true });
    }

    public class ChunkUploadRequest
    {
        public IFormFile? chunk { get; set; }
        public int chunkIndex { get; set; }
        public int totalChunks { get; set; }
        public string fileId { get; set; } = string.Empty;
        public string fileName { get; set; } = string.Empty;
    }    
}
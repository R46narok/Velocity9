using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeroGravity.Application;
using ZeroGravity.DeepLearning.Common;

namespace ZeroGravity.Services.Coach.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class AnomalyController : ApiController
{
    private readonly IPredictionPipeline _pipeline;

    public AnomalyController(IPredictionPipeline pipeline)
    {
        _pipeline = pipeline;
    }

    [HttpGet]
    public async Task<IActionResult> DetailsAsync()
    {
        return Ok();
    }

    [HttpPost]
    // [Authorize]
    public async Task<FileResult> PredictAsync(IFormFile file)
    {
        var filePath = Path.Combine(Path.GetTempPath(), file.FileName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        _pipeline.Run(filePath);

        var fn = Path.GetFileNameWithoutExtension(file.FileName);
        var predictedFilePath = Path.Combine(Path.GetTempPath(), $"predicted_{fn}.mp4");
        var bytes = await System.IO.File.ReadAllBytesAsync(predictedFilePath);
        return File(bytes, "video/mp4");
    }
}
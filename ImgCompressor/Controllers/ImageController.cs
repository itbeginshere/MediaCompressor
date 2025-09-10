using MediaCompressor.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaCompressor.API.Controllers;

public class ImageController(IImageService imageService) : ControllerBase
{
    private readonly IImageService _imageService = imageService;

    [HttpPost]
    [ActionName("compress")]
    public async Task<IActionResult> Compress()
    {
        var result = await _imageService.CompressAsync();

        return Ok();
    }
}

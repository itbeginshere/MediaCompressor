using MediaCompressor.Application.Services;
using MediaCompressor.Core.Images;
using Microsoft.AspNetCore.Mvc;

namespace MediaCompressor.API.Controllers;

public class ImageController(IImageService imageService) : ControllerBase
{
    private readonly IImageService _imageService = imageService;

    [HttpPost]
    [ActionName("compress")]
    public async Task<IActionResult> Compress(ImageCompressRequest request)
    {
        if (request.File is null || request.File.Length == 0)
        {
            return BadRequest("No file was uploaded.");
        }

        using var fileStream = new MemoryStream();
        await request.File.CopyToAsync(fileStream);

        if (fileStream is null)
        {
            return BadRequest("Could not read the uploaded file.");
        }

        var data = new ImageCompressDto(
            Quality: request.Quality,
            FileName: request.File.FileName,
            FileFormat: Path.GetExtension(request.File.FileName).TrimStart('.'),
            FileBytes: fileStream.ToArray());

        byte[] result = _imageService.Compress(data);

        return Ok();
    }
}

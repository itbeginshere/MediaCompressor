using MediaCompressor.Application.Helpers;
using MediaCompressor.Application.Services;
using MediaCompressor.Core.Images.Compress;
using MediaCompressor.Core.Images.Resize;
using Microsoft.AspNetCore.Mvc;

namespace MediaCompressor.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ImageController(IImageService imageService) : ControllerBase
{
    private readonly IImageService _imageService = imageService;

    [HttpPost]
    [ActionName("compress")]
    public async Task<IActionResult> Compress([FromForm] ImageCompressRequest request)
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

        byte[] compressedImage = _imageService.Compress(data);

        var result = File(
            compressedImage,
            MimeTypeHelper.FromFileFormat(data.FileFormat),
            $"conpressed_image_{DateTime.Now.Ticks}_{request.File.FileName}");

        return result;
    }

    [HttpPost]
    [ActionName("resize")]
    public async Task<IActionResult> Resize([FromForm] ImageResizeRequest request)
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

        var data = new ImageResizeDto(
            Width: request.Width,
            Height: request.Height,
            FileName: request.File.FileName,
            FileFormat: Path.GetExtension(request.File.FileName).TrimStart('.'),
            FileBytes: fileStream.ToArray());

        byte[] resizedImage = _imageService.Resize(data);

        var result = File(
            resizedImage,
            MimeTypeHelper.FromFileFormat(data.FileFormat),
            $"resized_image_{DateTime.Now.Ticks}_{request.File.FileName}");

        return result;
    }

}

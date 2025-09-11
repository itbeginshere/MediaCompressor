using Microsoft.AspNetCore.Http;

namespace MediaCompressor.Core.Images.Resize;
public sealed record ImageResizeRequest(
    int Width,
    int Height,
    IFormFile File);

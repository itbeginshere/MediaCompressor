using Microsoft.AspNetCore.Http;

namespace MediaCompressor.Core.Images.Compress;
public sealed record ImageCompressRequest(
    int Quality,
    IFormFile File);

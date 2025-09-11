using Microsoft.AspNetCore.Http;

namespace MediaCompressor.Core.Images;
public sealed record ImageCompressRequest(
    int Quality,
    IFormFile File);

namespace MediaCompressor.Core.Images.Resize;
public sealed record ImageResizeDto(
    int Width,
    int Height,
    string FileName,
    string FileFormat,
    byte[] FileBytes);
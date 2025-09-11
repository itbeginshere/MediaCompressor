namespace MediaCompressor.Core.Images.Compress;
public sealed record ImageCompressDto(
    int Quality,
    string FileName,
    string FileFormat,
    byte[] FileBytes);

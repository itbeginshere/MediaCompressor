namespace MediaCompressor.Core.Images.Compress;
public record ImageCompressDto(
    int Quality,
    string FileName,
    string FileFormat,
    byte[] FileBytes);

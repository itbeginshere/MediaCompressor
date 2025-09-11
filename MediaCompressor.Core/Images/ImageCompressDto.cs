namespace MediaCompressor.Core.Images;
public record ImageCompressDto(
    int Quality,
    string FileName,
    string FileFormat,
    byte[] FileBytes);

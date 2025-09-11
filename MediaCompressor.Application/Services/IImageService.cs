using MediaCompressor.Core.Images;

namespace MediaCompressor.Application.Services;
public interface IImageService
{
    byte[] Compress(ImageCompressDto data);
}

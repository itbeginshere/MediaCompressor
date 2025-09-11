using MediaCompressor.Core.Images.Compress;
using MediaCompressor.Core.Images.Resize;

namespace MediaCompressor.Application.Services;
public interface IImageService
{
    byte[] Compress(ImageCompressDto data);
    byte[] Resize(ImageResizeDto data);
}

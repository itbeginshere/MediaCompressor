using MediaCompressor.Core.Images;
using SkiaSharp;

namespace MediaCompressor.Application.Services.Implementation;
public sealed class ImageService : IImageService
{
    public byte[] Compress(ImageCompressDto data)
    {
        using var inputStream = new MemoryStream(data.FileBytes);
        using var skStream = new SKManagedStream(inputStream);
        using var original = SKBitmap.Decode(skStream);

        if (original is null)
            throw new Exception("Invalid image file.");

        using var image = SKImage.FromBitmap(original);

        var skFormat = data.FileFormat.ToLower() switch
        {
            "webp" => SKEncodedImageFormat.Webp,
            "png" => SKEncodedImageFormat.Png,
            "jpg" => SKEncodedImageFormat.Jpeg,
            _ => throw new NotSupportedException($"Unsupported image format: {data.FileFormat}.")
        };

        using var compressed = image.Encode(
            skFormat,
            data.Quality);

        return compressed.ToArray();
    }
}

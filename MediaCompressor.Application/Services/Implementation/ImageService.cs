using MediaCompressor.Core.Images.Compress;
using MediaCompressor.Core.Images.Resize;
using SkiaSharp;

namespace MediaCompressor.Application.Services.Implementation;
public sealed class ImageService : IImageService
{
    public byte[] Compress(ImageCompressDto data)
    {
        if (data.Quality < 0 || data.Quality > 100)
            throw new ArgumentException("Quality must be between 0 and 100.");

        using var original = GetOriginalBitMap(data.FileBytes);

        using var image = SKImage.FromBitmap(original);

        var skFormat = GetFileExtension(data.FileFormat);

        using var compressed = image.Encode(
            skFormat,
            data.Quality);

        return compressed.ToArray();
    }

    public byte[] Resize(ImageResizeDto data)
    {
        if (data.Width <= 0 || data.Height <= 0)
            throw new ArgumentException("Width and Height must be greater than zero.");

        using var original = GetOriginalBitMap(data.FileBytes);

        using var image = SKImage.FromBitmap(original);

        using var resized = original.Resize(
            new SKImageInfo(data.Width, data.Height),
            SKFilterQuality.High);

        if (resized == null)
            throw new FileLoadException("Failed to resize image.");

        var skFormat = GetFileExtension(data.FileFormat);

        using var compressed = resized.Encode(
            skFormat,
            100);

        return compressed.ToArray();
    }

    private SKBitmap GetOriginalBitMap(byte[] fileBytes)
    {
        using var inputStream = new MemoryStream(fileBytes);
        using var skStream = new SKManagedStream(inputStream);
        var original = SKBitmap.Decode(skStream);

        if (original is null)
            throw new Exception("Invalid image file.");

        return original;
    }

    private SKEncodedImageFormat GetFileExtension(string format)
    {
        return format.ToLower() switch
        {
            "gif" => SKEncodedImageFormat.Gif,
            "webp" => SKEncodedImageFormat.Webp,
            "png" => SKEncodedImageFormat.Png,
            "jpg" => SKEncodedImageFormat.Jpeg,
            "jpeg" => SKEncodedImageFormat.Jpeg,
            _ => throw new NotSupportedException($"Unsupported image format: {format}.")
        };
    }


}

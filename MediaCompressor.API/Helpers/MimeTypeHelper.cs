namespace MediaCompressor.Application.Helpers;
public sealed class MimeTypeHelper
{
    public static string FromFileFormat(string fileFormat)
    {
        return fileFormat switch
        {
            "gif" => "image/gif",
            "webp" => "image/webp",
            "png" => "image/png",
            "jpg" => "image/jpeg",
            "jpeg" => "image/jpeg",
            _ => throw new NotImplementedException("File format not supported.")
        };
    }
}

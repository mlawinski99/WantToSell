using HeyRed.Mime;

namespace WantToSell.Storage.Helpers;

public static class MimeTypeHelper
{
    public static string GetMimeType(string extension)
    {
        var mimeType = MimeTypesMap.GetMimeType(extension);
        return mimeType;
    }
}
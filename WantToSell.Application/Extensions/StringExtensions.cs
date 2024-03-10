namespace WantToSell.Application.Extensions;

public static class StringExtensions
{
    public static string AppendGuid(this string originalString)
    {
        return $"{Guid.NewGuid()}-{originalString}";
    }
}

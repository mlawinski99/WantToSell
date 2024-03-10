namespace WantToSell.Application.Extensions;

public static class EnumerableExtensions
{
    public static bool NotContains<T>(this IEnumerable<T> source, T value)
    {
        return !source.Contains(value);
    }
    
    public static void SetProperty<T>(this IEnumerable<T> items, Action<T> updateAction)
    {
        foreach (var item in items)
        {
            updateAction(item);
        }
    }
}
namespace PrimativeExtensions;

public static class IEnumerableExtensions
{
    
    public static bool Empty<T>(this IEnumerable<T> source) => !source.Any();
    public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T item)
    {
        foreach (var i in source)
        {
            yield return i;
        }
        yield return item;
    }
    
    public static IEnumerable<T> Concat<T>(this T source, T item)
    {
        yield return source;
        yield return item;
    }
    
    public static IEnumerable<T> Concat<T>(this T source, params T[] items)
    {
        yield return source;
        foreach (T item in items)
        {
            yield return item;
        }
    }

    public static bool ContainedIn<T>(this T item, IEnumerable<T> source) => source.Contains(item);

    public static bool ContainedIn<T>(this T item, params T[] source) => source.Contains(item);

    
    
}
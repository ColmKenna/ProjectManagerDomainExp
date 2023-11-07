namespace Measurements;

public static class DistanceExtensions
{
    public static IEnumerable<Distance> GetInOrder(this IEnumerable<Distance> distances)
    {
        
        return distances.OrderBy(d => d);
        var asSmallestUnit = distances.Select(d => (original: d, asSmallest: d.GetAs(DistanceUnit.Millimeters)));
        return asSmallestUnit.OrderBy(d => d).Select(d => d.original);
    }
    
    public static IEnumerable<Distance> GetInOrderDescending(this IEnumerable<Distance> distances)
    {
        var asSmallestUnit = distances.Select(d => (original: d, asSmallest: d.GetAs(DistanceUnit.Millimeters)));
        return asSmallestUnit.OrderByDescending(d => d).Select(d => d.original);
    }
    
}
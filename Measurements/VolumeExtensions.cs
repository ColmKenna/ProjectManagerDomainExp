namespace Measurements;

public static class VolumeExtensions
{
    public static IEnumerable<Volume> GetInOrder(this IEnumerable<Volume> volumes)
    {
        var withSmallestUnit = volumes.Select(a => (original: a, asSmallest: a.GetAs(VolumeUnit.Milliliters)));
        return withSmallestUnit.OrderBy(a => a).Select(a => a.original);
    }

    public static IEnumerable<Volume> GetInOrderDescending(this IEnumerable<Volume> volumes)
    {
        var withSmallestUnit = volumes.Select(a => (original: a, asSmallest: a.GetAs(VolumeUnit.Milliliters)));
        return withSmallestUnit.OrderByDescending(a => a).Select(a => a.original);

    }
}
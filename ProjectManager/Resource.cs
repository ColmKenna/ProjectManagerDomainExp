using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

public class Resource
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ResourceProvider ResourceProvider { get; private set; }
    
    private IList<string> KeyWords { get; set; } = new List<string>();

    private Resource()
    {
        
    }
    public static Resource Create(string resourceName, string resourceDescription, ResourceProvider resourceProvider)
    {
        return new Resource
        {
            Name = resourceName,
            Description = resourceDescription,
            ResourceProvider = resourceProvider
        };
    }
    
    public override string ToString()
    {
        return $"{Name} - {Description}";
    }
}

public static class ResourceExtensions
{
    public static IList<(Measurement Quantity, Resource Resource)> GroupByResource(
        this IEnumerable<(Measurement Quantity, Resource Resource)> itemsToCheck)
    {
        var returnList = new List<(Measurement Quantity, Resource Resource)>();
        foreach (var item in itemsToCheck)
        {
            if (returnList.Any(x => x.Resource == item.Resource && x.Quantity.HasSameMeasurementTypeAs(item.Quantity)))
            {
                var existingItem = returnList.First(x => x.Resource == item.Resource);
                returnList.Remove(existingItem);
                returnList.Add((existingItem.Quantity + item.Quantity, item.Resource));
            }
            else
            {
                returnList.Add(item);
            }
        }

        return returnList;
    }
    
    public static IList<(Measurement Quantity, Resource Resource)> GroupByResourceForType(
        this IEnumerable<(Measurement Quantity, Resource Resource)> itemsToCheck, Measurement.MeasurementType measurementType)
    {
        var returnList = new List<(Measurement Quantity, Resource Resource)>();
        foreach (var item in itemsToCheck)
        {
            if (item.Quantity.HasSameMeasurementTypeAs(measurementType) &&   returnList.Any(x => x.Resource == item.Resource && x.Quantity.HasSameMeasurementTypeAs(item.Quantity)))
            {
                var existingItem = returnList.First(x => x.Resource == item.Resource);
                returnList.Remove(existingItem);
                returnList.Add((existingItem.Quantity + item.Quantity, item.Resource));
            }
            else
            {
                returnList.Add(item);
            }
        }

        return returnList;
    }

    public static IList<(Measurement Quantity, Resource Resource)> GroupByResourceForType(
        this IEnumerable<(Measurement Quantity, Resource Resource)> itemsToCheck, IDictionary<Resource, int> minimumQtyForBucket)
    {
        var returnList = new List<(Measurement Quantity, Resource Resource)>();
        foreach (var item in itemsToCheck)
        {
            if (minimumQtyForBucket.ContainsKey(item.Resource) && item.Quantity.IsInt())
            {
                var minimumQty = minimumQtyForBucket[item.Resource];
                if (item.Quantity.GetQty().ToInt() >= minimumQty)
                {
                    var numberOfBuckets = item.Quantity.GetQty().ToInt() / minimumQty;
                    var remainder = item.Quantity.GetQty().ToInt() % minimumQty;
                    Enumerable.Range(1, numberOfBuckets).ToList().ForEach(x => returnList.Add((minimumQty, item.Resource)));
                    if (remainder > 0)
                    {
                        returnList.Add((remainder, item.Resource));
                    }
                }
                else
                {
                    returnList.Add(item);
                }
            }
            else
            {
                returnList.Add(item);
            }
        }

        return returnList;

        
    }
    
    public static IList<(Measurement Measurement, Resource Resource)> GetAmountOfResource(this IEnumerable<(Measurement Measurement, Resource Resource)> itemsToCheck, Resource resource)   
    {
        return itemsToCheck.Where(x => x.Resource == resource).ToList();
    }


}
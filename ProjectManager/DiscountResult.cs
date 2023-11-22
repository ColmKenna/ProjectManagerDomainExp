using Measurements;

namespace ProjectManager;

public struct DiscountResult
{
    public IList<(Measurement Measurement, Resource Resource)> ItemsUsedForDiscount { get; set; }

    public IList<(Measurement Measurement, Resource Resource)> ItemsUsedForDiscountGrouped => ItemsUsedForDiscount.GroupByResource();

    public IList<(Measurement Measurement, Resource Resource)> ItemsDiscounted { get; set; }
    public IList<(Measurement Measurement, Resource Resource)> ItemsDiscountedGrouped => ItemsDiscounted.GroupByResource();

    
    

    
    public IList<string> GetItemsUsedForDiscount() => GetItemsUsedForDiscountSummarised().Select(x => $"{x.Measurement} - {x.Resource.Name}").ToList();
    public IList<string> GetItemsDiscounted() => GetItemsDiscountedSummarised().Select(x => $"{x.Measurement} - {x.Resource.Name}").ToList();

    public decimal Discount { get; set; }

    public DiscountResult()
    {
        ItemsUsedForDiscount = new List<(Measurement Measurement, Resource Resource)>();
        ItemsDiscounted = new List<(Measurement Measurement, Resource Resource)>();
        Discount = 0;
    
    }
    
    public IList<(Measurement Measurement, Resource Resource)> GetItemsUsedForDiscountSummarised() => GetCompressedItems(ItemsUsedForDiscount);

    public IList<(Measurement Measurement, Resource Resource)> GetItemsDiscountedSummarised() => GetCompressedItems(ItemsDiscounted);

    public IList<(Measurement Measurement, Resource Resource)> GetCompressedItems(IList<(Measurement Measurement, Resource Resource)> items)
    {
        var compressed = new List<(Measurement Measurement, Resource Resource)>();
        
        // group by resource
        var groupedByResource = items.GroupBy(x => x.Resource);
        // further group by measurement
        foreach (var resourceGroup in groupedByResource)
        {
            IEnumerable<IGrouping<Measurement.MeasurementType, (Measurement Measurement, Resource Resource)>> groupedByMeasurement = resourceGroup.GroupBy(x => x.Measurement.GetMeasurementType());
            // for each measurement type
            foreach (var measurementGroup in groupedByMeasurement)
            {
                // sum the measurements
                var total = measurementGroup.Aggregate(Measurement.Zero(measurementGroup.Key), (current, next) => current + next.Measurement);
                // add to compressed list
                compressed.Add((total, resourceGroup.Key));
            }
        }

        return compressed;
    }
    
    
    
    
}


using Measurements;

namespace ProjectManager;

public struct DiscountResult
{
    public IList<(Measurement Measurement, Resource Resource)> ItemsUsedForDiscount { get; set; }
    public IList<(Measurement Measurement, Resource Resource)> ItemsDiscounted { get; set; }

    public decimal Discount { get; set; }

    public DiscountResult()
    {
        ItemsUsedForDiscount = new List<(Measurement Measurement, Resource Resource)>();
        ItemsDiscounted = new List<(Measurement Measurement, Resource Resource)>();
        Discount = 0;
    
    }
}
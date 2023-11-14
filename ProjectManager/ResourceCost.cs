using Measurements;

namespace ProjectManager;

public class ResourceCost 
{
    private ResourceCost(Measurement quantity, Resource resource, decimal cost)
    {
        Quantity = quantity;
        Resource = resource;
        Cost = cost;
    }

    public static ResourceCost CreateInstance(Measurement quantity, Resource resource, decimal cost)
    {
        return new ResourceCost(quantity, resource, cost);
    }

    public int Id { get; private set; }
    public Measurement Quantity { get; private set; }
    public Resource Resource { get; private set; }
    public decimal Cost { get; private set; }
    public Measurement GetAs(Measurement.MeasurementType measurementType, DateTime? onDate = null)
    {
        return Quantity.GetAs(measurementType, onDate);
    }
    
    public decimal GetCostPer(Measurement measurement, DateTime? onDate = null)
    {
        
        return Cost / GetAs(measurement.GetMeasurementType() , onDate).GetQty();
    }
    

}
using Measurements;
using PrimativeExtensions;

namespace ProjectManager;


public enum CostType
{
    ExactOnly,
    ExactMultiplesOnly,
    AnyAmount,
    AnyAmountAbove
}
public class ResourceCost: IComparable<ResourceCost> 
{
    
 private ResourceCost(Measurement quantity, Resource resource, decimal cost, CostType costType = CostType.ExactMultiplesOnly)
    {
        Quantity = quantity;
        Resource = resource;
        Cost = cost;
        CostType = costType;
    }

    public static ResourceCost CreateInstance(Measurement quantity, Resource resource, decimal cost, CostType costType = CostType.ExactMultiplesOnly)
    {
        return new ResourceCost(quantity, resource, cost, costType);
    }

    public int Id { get; private set; }
    public Measurement Quantity { get; private set; }
    public Resource Resource { get; private set; }
    public decimal Cost { get; private set; }
    public CostType CostType { get; private set; } 
    public Measurement GetAs(Measurement.MeasurementType measurementType, DateTime? onDate = null)
    {
        return Quantity.GetAs(measurementType, onDate);
    }
    
    private Validation<decimal> GetCost(Measurement currentMeasurement)
    {
        if(this.CostType == CostType.ExactOnly && currentMeasurement != Quantity)
        {
            return Validation<decimal>.Fail($"The quantity {currentMeasurement} is not an exact match for the cost {Quantity}");
        }
        if(this.CostType == CostType.ExactMultiplesOnly && currentMeasurement.GetQty() % Quantity.GetQty() != 0)
        {
            return Validation<decimal>.Fail($"The quantity {currentMeasurement} is not an exact multiple for the cost {Quantity}");
        }
        if (this.CostType == CostType.AnyAmountAbove && currentMeasurement.GetQty() < Quantity.GetQty())
        {
            return Validation<decimal>.Fail($"The quantity {currentMeasurement} is not above the cost {Quantity}");
        } 
                
        var total = this.Cost *  (currentMeasurement.GetQty()/Quantity.GetQty() );
        return Validation<decimal>.Success(total);
                
    }
    public Validation<decimal> GetCostFor(Measurement measurement, DateTime? onDate = null)
    {
        var requestedQty = measurement.GetAs(Quantity.GetMeasurementType(), onDate);

        return requestedQty.Map(GetCost);
    }


    public int CompareTo(ResourceCost? other)
    {
        if (other == null) return 1;
        var otherAsSameMeasurementType = other.GetCostFor(this.Quantity);
        if (otherAsSameMeasurementType.IsFailure) return 1;

        var thisCost = this.GetCostFor(Quantity,new DateTime(2023,1,1));
        var otherCost = other.GetCostFor(other.Quantity,new DateTime(2023,1,1));
        return thisCost.Value.CompareTo(otherCost.Value);
    }
}
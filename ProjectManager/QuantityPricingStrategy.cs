using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

public class QuantityPricingStrategy 
{
    private IList<ResourceCost> quantityPrices;

    public QuantityPricingStrategy(ResourceCost quantityCost)
    {
        quantityPrices = new List<ResourceCost>();
        quantityPrices.Add(quantityCost);
    }

    public Validation<QuantityPricingStrategy> AddPrice(ResourceCost quantity)
    {

        if (quantityPrices.Any(x => x.Resource != quantity.Resource))
        {
            return Validation<QuantityPricingStrategy>.Fail(
                $"Can't add {quantity.Resource.Name} to price strategy for ${quantityPrices.First().Resource.Name} ");
        }

        var a = quantityPrices.First().Quantity.GetMeasurementType();
        var b = quantity.Quantity.GetMeasurementType();
            
        if (quantityPrices.Any(x => x.Quantity.HasSameMeasurementTypeAs(quantity.Quantity) == false))
        {
            return Validation<QuantityPricingStrategy>.Fail(
                $"Can't add {quantity.Quantity} to price strategy for ${quantityPrices.First().Quantity} ");
        }
        
        
        var first = quantityPrices.FirstOrDefault();
        var tempDate = new DateTime(2020, 2, 1);
        // order quantityPrices by the key
        var orderedQuantityPrices = 
            quantityPrices
                .OrderBy(x => x.GetAs( first.Quantity.GetMeasurementType(), tempDate).GetQty())
                .ToList(); 

        var updatedList =
            quantityPrices.Concat(quantity)
                .OrderBy(x => x.GetAs( first.Quantity.GetMeasurementType(), tempDate).GetQty())
                
                .ToList(); 

        var previous = orderedQuantityPrices.First();
        var initalType = previous.Quantity.GetMeasurementType();
        foreach (var current in updatedList.Skip(1))
        {
            var currentAs = current.GetAs(initalType, tempDate);
            var previousAs = previous.GetAs(initalType, tempDate);
            var currentCost = current.Cost/ currentAs.GetQty();;            
            var previousCost = previous.Cost/ previousAs.GetQty();            

            
            if (currentCost  > previousCost)
            {
                return Validation<QuantityPricingStrategy>.Fail(
                    $"Price for {current.Quantity} {current.Resource.Name} is less than the previous price of {previous.Quantity} {previous.Resource.Name} ");
            }
        }

        quantityPrices.Add(quantity);
        return this;
    }
    
    public decimal GetPrice(Measurement quantity, DateTime? onDate = null)
    {
        var orderedQuantityPrices = quantityPrices
            .OrderByDescending(x => x.GetAs( quantity.GetMeasurementType(), onDate).GetQty())
            .ToList();
        
        
        var remainingQty = quantity.GetQty();
        var totalCost = 0m;
        var used = Measurement.Zero(quantity.GetMeasurementType());
        foreach (var quantityCost in orderedQuantityPrices)
        {
            var currentQty = quantityCost.GetAs(quantity.GetMeasurementType(), onDate);
            var am = Math.Floor((remainingQty / currentQty.GetQty())).ToInt();
            if (am > 0)
            {
                totalCost += quantityCost.Cost * am;
                remainingQty -= currentQty.GetQty() * am;
                used += currentQty ;
            }
        }
         
        return totalCost;
    }
    
    
}
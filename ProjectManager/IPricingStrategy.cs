using System.Diagnostics;
using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

public interface IPricingStrategy
{
}

public class QuantityPricingStrategy : IPricingStrategy
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
        foreach (var quantityCost in orderedQuantityPrices)
        {
            var currentQty = quantityCost.GetAs(quantity.GetMeasurementType(), onDate);
            var am = Math.Floor((remainingQty / currentQty.GetQty())).ToInt();
            if (am > 0)
            {
                totalCost += quantityCost.Cost * am;
                remainingQty -= currentQty.GetQty() * am;
            }
            
            
            
        }
        
        return totalCost;
    }
    
    
}

public class BuyNGetCheapestFreeStrategy : IPricingStrategy
{
    private int buyN;
    private int getCheapestFree;
    private List<ResourceCost> quantityPrices;

    public BuyNGetCheapestFreeStrategy(int buyN, int getCheapestFree)
    {
        this.buyN = buyN;
        this.getCheapestFree = getCheapestFree;
        quantityPrices = new List<ResourceCost>();
    }

    public Validation<BuyNGetCheapestFreeStrategy> AddPrice(ResourceCost resourceCost)
    {
        quantityPrices.Add(resourceCost);
        return this;
    }


    public DiscountResult GetPrice(IList<ResourceCost> items)
    {
        // find all items that are in the quantityPrices
        var itemsInQuantityPrices =
            items.Where(x => quantityPrices.Any(y => y.Resource == x.Resource  && y.Quantity == x.Quantity ))
                .OrderByDescending(x => x.Cost)
                .ToList();

        if (itemsInQuantityPrices.Count() < buyN)
        {
            return new DiscountResult
            {
                Discount = 0,
                ItemsUsedForDiscount = new List<(Measurement, Resource)>()
            };
        }

        // get the largest number divisible by buyN
        var itemsToUseInDiscount = itemsInQuantityPrices.Count() - (itemsInQuantityPrices.Count() % buyN);


        

        // loop through orderedItems and get the price of every buyN item
        var totalDiscount = 0m;
        var itemsUsedForDiscount = items.Take(itemsToUseInDiscount).Select(x => (x.Quantity, x.Resource)).ToList();
        var itemsDiscounted = new List<(Measurement, Resource)>();
        for (var i = 1; i <=itemsInQuantityPrices.Count(); i++)
        {
            if (i % buyN == 0)
            {
                totalDiscount += itemsInQuantityPrices.ElementAt(i-1).Cost;
                itemsDiscounted.Add((itemsInQuantityPrices.ElementAt(i-1).Quantity ,itemsInQuantityPrices.ElementAt(i-1).Resource));
            }
        }

        return new DiscountResult()
        {
            Discount = totalDiscount, ItemsUsedForDiscount = itemsUsedForDiscount , ItemsDiscounted = itemsDiscounted
        };
    }
}

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

public class MealDealStyleStrategy : IPricingStrategy
{
    public decimal MealDealPrice { get; }
    private List<MealDealGroup> mealDealGroups;

    public MealDealStyleStrategy(decimal mealDealPrice)
    {
        this.MealDealPrice = mealDealPrice;
        mealDealGroups = new List<MealDealGroup>();
    }

    public Validation<MealDealStyleStrategy> AddMealDealGroup(MealDealGroup mealDealGroup)
    {
        mealDealGroups.Add(mealDealGroup);
        return this;
    }



    public Validation<MealDealStyleStrategy> AddMealDealGroup(string name, string description, int numberToAdd,params ResourceCost[] items) 
    {
        var mealDealGroup = new MealDealGroup(name, description, numberToAdd);
        foreach (var mealDealItem in items)
        {
            mealDealGroup.AddResource(mealDealItem);
        }
        mealDealGroups.Add(mealDealGroup);
        return this;
    }

    public Validation<MealDealStyleStrategy> AddResourceToMealDealGroup(MealDealGroup mealDealGroup,
        Measurement quantity, Resource resource, decimal cost)
    {
        var mealDealGroupToUpdate = mealDealGroups.First(x => x == mealDealGroup);
        mealDealGroupToUpdate.AddResource(quantity, resource, cost);
        return this;
    }

    public IReadOnlyCollection<MealDealGroup> GetMealDealGroups()
    {
        return mealDealGroups;
    }

    public DiscountResult GetPrice(
        IList<(Measurement, Resource)> items)
    {
        // Create a dictionary of each meal deal group and the number of items in that group
        var mealDealGroupsWithCounts = mealDealGroups.ToDictionary(x => x, x => x.GetItemToRemove(items));

        if (mealDealGroupsWithCounts.Any(x => x.Value.Count() < x.Key.NumberToAdd))
        {
            return new DiscountResult
            {
                Discount = 0m,//MealDealPrice,
                ItemsUsedForDiscount =  new List<(Measurement, Resource)>()
            };
        }

        var itemsToRemove = mealDealGroupsWithCounts.SelectMany(x => x.Value).ToList();
        var totalBeforeDiscount = itemsToRemove.Sum(x => x.Cost);
        var totalDiscount = totalBeforeDiscount - MealDealPrice;
        if (totalDiscount < 0)
        {
            totalDiscount = 0;
        }

        return new DiscountResult()
        {
            Discount = totalDiscount,
            ItemsUsedForDiscount = itemsToRemove.Select(x => (x.Quantity, x.Resource)).ToList(),
            ItemsDiscounted = itemsToRemove.Select(x => (x.Quantity, x.Resource)).ToList()
        };
    }
}


public class MealDealGroup
{
    private List<ResourceCost> resources;
    private string name;
    private string description;
    public int NumberToAdd { get; }

    public MealDealGroup(string name, string description, int numberToAdd)
    {
        this.name = name;
        this.description = description;
        this.NumberToAdd = numberToAdd;
        resources = new List<ResourceCost>();
    }

    public Validation<MealDealGroup> AddResource(Measurement quantity, Resource resource, decimal cost)
    {
        resources.Add(ResourceCost.CreateInstance(quantity, resource, cost));
        return this;
    }

    public Validation<MealDealGroup> AddResource(ResourceCost resourceCost)
    {
        resources.Add(resourceCost);
        return this;
    }

    private bool ItemIsInGroup(Measurement quantity, Resource resource)
    {
        return resources.Any(x => x.Resource == resource && x.Quantity == quantity);
    }
    
      
    

    public IList<ResourceCost> GetItemToRemove(
        IEnumerable<(Measurement Quantity, Resource Resource) > itemsToCheck)
    {
        var itemsToRemove = new List<ResourceCost>();
        var itemsInGroup = itemsToCheck.Where(x => ItemIsInGroup(x.Quantity , x.Resource )).ToList();
        
        
        
        if (itemsInGroup.Count() < NumberToAdd)
        {
            return itemsToRemove;
        }
        var itemsInGroupByCost = itemsInGroup.Select(x => this.resources.First(y => y.Resource == x.Resource && y.Quantity == x.Quantity)).ToList();

        return
            itemsInGroupByCost
                .OrderByDescending(x => x.Cost )
                .Take(NumberToAdd)
                .ToList();
    }
}
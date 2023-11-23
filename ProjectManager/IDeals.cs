using System.Diagnostics;
using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

public interface IDeals
{
    public DiscountResult GetDiscount(IList<(Measurement measurement, Resource resource) > itemsToCheck, Func<(Measurement, Resource), ResourceCost> getResourceCost);
}

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

public class BuyNGetCheapestFreeStrategy : IDeals
{
    private int buyN;
    private int getCheapestFree;
    private List<(Measurement Quantity, Resource Resource)> eligibleProducts;

    public BuyNGetCheapestFreeStrategy(int buyN, int getCheapestFree)
    {
        this.buyN = buyN;
        this.getCheapestFree = getCheapestFree;
        eligibleProducts = new List<(Measurement, Resource)>();
    }

    public Validation<BuyNGetCheapestFreeStrategy> AddPrice((Measurement, Resource) resourceCost)
    {
        eligibleProducts.Add(resourceCost);
        return this;
    }

    public Validation<BuyNGetCheapestFreeStrategy> AddPrice(Measurement measurement, Resource resource)
    {
        return AddPrice((measurement, resource));
    }


    public DiscountResult GetDiscount(IList<(Measurement measurement, Resource resource)> itemsToCheck, Func<(Measurement, Resource), ResourceCost> getResourceCost)
    {
        var quantityPrices = eligibleProducts.Select(x => getResourceCost(x)).ToList();
        // create a copy of items
        var withNumericItemsSplitByAmountToBuy = 
            GetListWithNumericItemsSplitByAmountToBuy(itemsToCheck).Where(x => quantityPrices.Any(y => y.Resource == x.resource && y.Quantity == x.measurement)).ToList();  

        var itemsInQuantityPrices = 
            withNumericItemsSplitByAmountToBuy.Select(x => quantityPrices.First(y => y.Resource == x.resource && y.Quantity == x.measurement))
                .OrderByDescending(x => x.Cost)
                .ToList();


        decimal totalDiscount = 0;
        IList<(Measurement Measurement, Resource Resource)> itemsDiscounted = new List<(Measurement Measurement, Resource Resource)>();
        IList<(Measurement Measurement, Resource Resource)> itemsUsedForDiscount = new List<(Measurement Measurement, Resource Resource)>();

        var numberOfItemsToUseForDeal = itemsInQuantityPrices.Count() - (itemsInQuantityPrices.Count() % buyN);
        
        for (var i = 1; i <= numberOfItemsToUseForDeal; i++)
        {
            if (i % buyN == 0)
            {
                totalDiscount += itemsInQuantityPrices.ElementAt(i-1).Cost;
                itemsDiscounted.Add((itemsInQuantityPrices.ElementAt(i-1).Quantity ,itemsInQuantityPrices.ElementAt(i-1).Resource));
            }
            itemsUsedForDiscount.Add((itemsInQuantityPrices.ElementAt(i-1).Quantity ,itemsInQuantityPrices.ElementAt(i-1).Resource)); 
        }

        return new DiscountResult()
        {
            Discount = totalDiscount, 
            ItemsUsedForDiscount = itemsUsedForDiscount , 
            ItemsDiscounted = itemsDiscounted
        };
   }

    private List<(Measurement measurement, Resource resource)> GetListWithNumericItemsSplitByAmountToBuy(IList<(Measurement measurement, Resource resource)> items)
    {
        
        IDictionary<Resource, int> minimumQtyForBucket = new Dictionary<Resource, int>();
        minimumQtyForBucket = eligibleProducts.Where(x => x.Quantity.IsInt()) .ToDictionary(x => x.Resource, x => x.Quantity.GetQty().ToInt());
        return items.GroupByResourceForType(minimumQtyForBucket).ToList();  
    }
}

public class MealDealStyleStrategy : IDeals
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



    public Validation<MealDealStyleStrategy> AddMealDealGroup(string name, string description, int numberToAdd,params (
        Measurement, Resource )[] items) 
    {
        var mealDealGroup = new MealDealGroup(name, description, numberToAdd);
        foreach (var mealDealItem in items)
        {
            mealDealGroup.AddResource(mealDealItem);
        }
        mealDealGroups.Add(mealDealGroup);
        return this;
    }

    public IReadOnlyCollection<MealDealGroup> GetMealDealGroups()
    {
        return mealDealGroups;
    }

    public DiscountResult GetDiscount(
        IList<(Measurement, Resource)> items, Func<(Measurement, Resource), ResourceCost> getResourceCost)
    {
        var itemsToCheck = items.GroupByResourceForType(new Measurement.MeasurementType(1) );
        
        // Create a dictionary of each meal deal group and the number of items in that group
        var mealDealGroupsWithCounts = mealDealGroups.ToDictionary(x => x, x => x.GetItemsToUse(itemsToCheck,getResourceCost));

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

    public IEnumerable<IGrouping<MealDealGroup, (Measurement Quantity, Resource Resource)>> GroupItemsByMealDealGroup()
    {
        var p = 
         mealDealGroups
            .SelectMany(mealDealGroup => mealDealGroup.GetItems(), (mealDealGroup, item) => new { MealDealGroup = mealDealGroup, Items = item })
            .GroupBy(x => x.MealDealGroup, x => x.Items);
        return p;
    }
}


public class MealDealGroup
{
    private List<(Measurement, Resource)> products;
    public string Name { get; }
    public string Description { get; }
    public int NumberToAdd { get; }

    public MealDealGroup(string name, string description, int numberToAdd)
    {
        this.Name = name;
        this.Description = description;
        this.NumberToAdd = numberToAdd;
        products = new List<(Measurement, Resource)>();
    }

    public Validation<MealDealGroup> AddResource(Measurement quantity, Resource resource)
    {
        products.Add((quantity, resource));
        return this;
    }

    public Validation<MealDealGroup> AddResource((Measurement quantity, Resource resource) resource)
    {
        products.Add( resource);
        return this;
    }


    private bool ItemIsInGroup(Measurement quantity, Resource resource)
    {
        return products.Any(x => x.Item2 == resource && x.Item1 == quantity);
    }
    
      
    

    public IList<ResourceCost> GetItemsToUse(
        IEnumerable<(Measurement Quantity, Resource Resource) > itemsToCheck, Func<(Measurement, Resource), ResourceCost> getResourceCost)
    {
        var productsWithPrices = this.products.Select(x => getResourceCost((x.Item1, x.Item2))).ToList();
        var itemsToUse = new List<ResourceCost>();
        var minimumQtyForBucket = productsWithPrices.Where(x => x.Quantity.IsInt()) .ToDictionary(x => x.Resource, x => x.Quantity.GetQty().ToInt());
        var itemsToCheckGrouped = itemsToCheck.GroupByResourceForType(minimumQtyForBucket).ToList();
        
        
        var itemsInGroup = 
            itemsToCheckGrouped
                .Where(x => ItemIsInGroup(x.Quantity , x.Resource ))
                .ToList();
        
        
        
        if (itemsInGroup.Count() < NumberToAdd)
        {
            return itemsToUse;
        }
        var itemsInGroupByCost =  itemsInGroup.Select(x => productsWithPrices.First(y => y.Resource == x.Resource && y.Quantity == x.Quantity)).ToList();

        return
            itemsInGroupByCost
                .OrderByDescending(x => x.Cost )
                .Take(NumberToAdd)
                .ToList();
    }

    public IList<(Measurement Quantity, Resource Resource) > GetItems()
    {
        return products ;
    }
    
    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (MealDealGroup) obj;
        return this.Name == other.Name && this.Description == other.Description && this.NumberToAdd == other.NumberToAdd;
    }
    public override int GetHashCode()
    {
        return (Name, Description, NumberToAdd).GetHashCode();
    }
    
    public static bool operator ==(MealDealGroup lhs, MealDealGroup rhs)
    {
        if (ReferenceEquals(lhs, rhs))
        {
            return true;
        }

        if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
        {
            return false;
        }

        return lhs.Equals(rhs);
    }
    
    public static bool operator !=(MealDealGroup lhs, MealDealGroup rhs)
    {
        return !(lhs == rhs);
    }
}
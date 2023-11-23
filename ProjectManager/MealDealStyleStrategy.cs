
using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

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
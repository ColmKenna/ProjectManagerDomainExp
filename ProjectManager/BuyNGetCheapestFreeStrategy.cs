using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

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
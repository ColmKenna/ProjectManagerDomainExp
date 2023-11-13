using ProjectManager;

namespace ProjectManagerTests.PricingTests;

public class BuyNGetCheapestFreeStrategyTests
{
    BuyNGetCheapestFreeStrategy buyNGetCheapestFreeStrategy;


    [Fact]
    public void CanCreateBuyNGetCheapestFreeStrategyWithJustOneItem()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var cost = 100m;
        var quantity = 1;
        var buyNGetCheapestFreeStrategy = new BuyNGetCheapestFreeStrategy(3, 1);
        buyNGetCheapestFreeStrategy.AddPrice(ResourceCost.CreateInstance(quantity, resource, cost));

        var items = Enumerable.Range(1, 3).Select(x => ResourceCost.CreateInstance(quantity, resource, cost)).ToList();


        var result = buyNGetCheapestFreeStrategy.GetPrice(items);
        Assert.Equal(100m, result.Discount);
        Assert.Equal(items.Count(), result.ItemsUsedForDiscount.Count());
        foreach (var itemToBeDiscounted in items)
        {
            var item = result.ItemsUsedForDiscount.FirstOrDefault(x => x.Resource == itemToBeDiscounted.Resource);
            Assert.Equal(itemToBeDiscounted.Quantity, item.Measurement);
        }

        Assert.Single(result.ItemsDiscounted);
        Assert.Equal(items.First().Quantity, result.ItemsDiscounted.First().Measurement);
        Assert.Equal(items.First().Resource, result.ItemsDiscounted.First().Resource);
    }

    [Fact]
    public void CanCreateBuyNGetCheapestFreeStrategyWithMultipleItems()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var resource2 = Resource.Create("Test Resource 2", "Test Resource Description 2",
            ResourceProvider.Create("Test Provider 2", "Test Provider Description 2"));


        var buyNGetCheapestFreeStrategy = new BuyNGetCheapestFreeStrategy(3, 1);
        buyNGetCheapestFreeStrategy.AddPrice(ResourceCost.CreateInstance(1, resource, 20));
        buyNGetCheapestFreeStrategy.AddPrice(ResourceCost.CreateInstance(1, resource2, 15));



        var items = new List<ResourceCost>();
        items.AddRange(Enumerable.Range(1, 2).Select(x => ResourceCost.CreateInstance(1, resource, 20)));
        items.Add(ResourceCost.CreateInstance(1, resource2, 15));

        var result = buyNGetCheapestFreeStrategy.GetPrice(items);
        Assert.Equal(15m, result.Discount);
        Assert.Equal(items.Count(), result.ItemsUsedForDiscount.Count());
        foreach (var itemToBeDiscounted in items)
        {
            var item = result.ItemsUsedForDiscount.FirstOrDefault(x => x.Resource == itemToBeDiscounted.Resource);
            Assert.Equal(itemToBeDiscounted.Quantity, item.Measurement);
        }

    }
    
    [Fact]
    public void CanCreateBuyNGetCheapestFreeStrategyGivesBestForCustomerOnTheRemainderItems()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var resource2 = Resource.Create("Test Resource 2", "Test Resource Description 2",
            ResourceProvider.Create("Test Provider 2", "Test Provider Description 2"));


        var buyNGetCheapestFreeStrategy = new BuyNGetCheapestFreeStrategy(3, 1);
        buyNGetCheapestFreeStrategy.AddPrice(ResourceCost.CreateInstance(1, resource, 20));
        buyNGetCheapestFreeStrategy.AddPrice(ResourceCost.CreateInstance(1, resource2, 15));



        var items = new List<ResourceCost>();
        items.AddRange(Enumerable.Range(1, 3).Select(x => ResourceCost.CreateInstance(1, resource, 20)));
        items.Add(ResourceCost.CreateInstance(1, resource2, 15));

        var result = buyNGetCheapestFreeStrategy.GetPrice(items);
        var itemsUsedForDiscount = result.ItemsDiscounted.ToList();
        
        Assert.Equal(20m, result.Discount);
        Assert.Equal(items.Count()-1, result.ItemsUsedForDiscount.Count());
        Assert.True( result.ItemsUsedForDiscount.All(x => x.Resource == resource)) ;

        Assert.Single(itemsUsedForDiscount);
        Assert.Equal(resource, itemsUsedForDiscount.First().Resource);
        
    }
}
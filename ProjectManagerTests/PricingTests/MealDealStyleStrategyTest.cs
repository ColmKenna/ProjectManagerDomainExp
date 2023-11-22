using Measurements;
using PrimativeExtensions;
using ProjectManager;

namespace ProjectManagerTests.PricingTests;

public class MealDealStyleStrategyTest
{

    private Resource group1resource1, group1resource2, group1resource3, group1resource4;
    private Resource group2resource1, group2resource2, group2resource3, group2resource4;
    private Resource group3resource1, group3resource2, group3resource3, group3resource4;
    private ResourceCost resourceGroup1Cost1, resourceGroup1Cost2, resourceGroup1Cost3, resourceGroup1Cost4;
    private ResourceCost resourceGroup2Cost1, resourceGroup2Cost2, resourceGroup2Cost3, resourceGroup2Cost4;
    private ResourceCost resourceGroup3Cost1, resourceGroup3Cost2, resourceGroup3Cost3, resourceGroup3Cost4;

    public MealDealStyleStrategyTest()
    {
        group1resource1 = Resource.Create("Test Resource", "Test Resource Description", ResourceProvider.Create("Test Provider", "Test Provider Description"));
        group1resource2 = Resource.Create("Test Resource 2", "Test Resource Description 2", ResourceProvider.Create("Test Provider 2", "Test Provider Description 2"));
        group1resource3 = Resource.Create("Test Resource 3", "Test Resource Description 3", ResourceProvider.Create("Test Provider 3", "Test Provider Description 3"));
        group1resource4 = Resource.Create("Test Resource 4", "Test Resource Description 4", ResourceProvider.Create("Test Provider 4", "Test Provider Description 4"));

        resourceGroup1Cost1 = ResourceCost.CreateInstance(1, group1resource1, 100m);
        resourceGroup1Cost2 = ResourceCost.CreateInstance(1, group1resource2, 120m);
        resourceGroup1Cost3 = ResourceCost.CreateInstance(1, group1resource3, 150m);
        resourceGroup1Cost4 = ResourceCost.CreateInstance(1, group1resource4, 200m);

        group2resource1 = Resource.Create("Test Resource 5", "Test Resource Description 5", ResourceProvider.Create("Test Provider 5", "Test Provider Description 5"));
        group2resource2 = Resource.Create("Test Resource 6", "Test Resource Description 6", ResourceProvider.Create("Test Provider 6", "Test Provider Description 6"));
        group2resource3 = Resource.Create("Test Resource 7", "Test Resource Description 7", ResourceProvider.Create("Test Provider 7", "Test Provider Description 7"));
        group2resource4 = Resource.Create("Test Resource 8", "Test Resource Description 8", ResourceProvider.Create("Test Provider 8", "Test Provider Description 8"));

        resourceGroup2Cost1 = ResourceCost.CreateInstance(1, group2resource1, 200m);
        resourceGroup2Cost2 = ResourceCost.CreateInstance(1, group2resource2, 110m);
        resourceGroup2Cost3 = ResourceCost.CreateInstance(1, group2resource3, 140m);
        resourceGroup2Cost4 = ResourceCost.CreateInstance(1, group2resource4, 180m);

        group3resource1 = Resource.Create("Test Resource 9", "Test Resource Description 9", ResourceProvider.Create("Test Provider 9", "Test Provider Description 9"));
        group3resource2 = Resource.Create("Test Resource 10", "Test Resource Description 10", ResourceProvider.Create("Test Provider 10", "Test Provider Description 10"));
        group3resource3 = Resource.Create("Test Resource 11", "Test Resource Description 11", ResourceProvider.Create("Test Provider 11", "Test Provider Description 11"));
        group3resource4 = Resource.Create("Test Resource 12", "Test Resource Description 12", ResourceProvider.Create("Test Provider 12", "Test Provider Description 12"));

        resourceGroup3Cost1 = ResourceCost.CreateInstance(1, group3resource1, 300m);
        resourceGroup3Cost2 = ResourceCost.CreateInstance(1, group3resource2, 130m);
        resourceGroup3Cost3 = ResourceCost.CreateInstance(1, group3resource3, 160m);
        resourceGroup3Cost4 = ResourceCost.CreateInstance(1, group3resource4, 190m);
    }

    [Fact]
    public void CanAddMultipleGroups()
    {

        var mealDealStyleStrategy = new MealDealStyleStrategy(300m);
        var group1 = mealDealStyleStrategy.AddMealDealGroup("group1", "First group of items", 2, resourceGroup1Cost1, resourceGroup1Cost2, resourceGroup1Cost3, resourceGroup1Cost4);
        var group2 = mealDealStyleStrategy.AddMealDealGroup("group2", "Second group of items", 1, resourceGroup2Cost1, resourceGroup2Cost2, resourceGroup2Cost3, resourceGroup2Cost4);
        var group3 = mealDealStyleStrategy.AddMealDealGroup("group3", "Third group of items", 2, resourceGroup3Cost1, resourceGroup3Cost2, resourceGroup3Cost3, resourceGroup3Cost4);

        var itemsOrdered = new List<(Measurement measurement, Resource resource)>();
        itemsOrdered.Add((1, group1resource1)); // 100
        itemsOrdered.Add((1, group1resource2)); // 120
        itemsOrdered.Add((1, group2resource1)); // 200
        itemsOrdered.Add((1, group2resource2)); // 110
        itemsOrdered.Add((1, group3resource1)); // 300
        itemsOrdered.Add((1, group3resource2)); // 130
        
        var itemsToBeDiscounted = new List<(Measurement measurement, Resource resource)>
        {
            (1, group1resource1), // 100
            (1, group1resource2), // 120
            (1, group2resource1), // 200
            (1, group3resource1), // 300
            (1, group3resource2) // 130
        };

        var totalBeforeDiscount = 100m + 120m + 200m  + 300m + 130m;
        var expectedDiscount = totalBeforeDiscount - 300m;
        var result = mealDealStyleStrategy.GetDiscount(itemsOrdered);
        
        Assert.Equal(expectedDiscount, result.Discount );
        Assert.Equal(itemsToBeDiscounted.Count, result.ItemsUsedForDiscount.Count());
        foreach (var itemToBeDiscounted in itemsToBeDiscounted)
        {
            var item = result.ItemsUsedForDiscount.FirstOrDefault(x => x.Resource == itemToBeDiscounted.resource);
            Assert.Equal(itemToBeDiscounted.measurement, item.Measurement);
        }
        
    }

    [Fact]  
    public void CanGetGroupedListOfProducts()
    {
        var mealDealStyleStrategy = new MealDealStyleStrategy(300m);
        var group1 = mealDealStyleStrategy.AddMealDealGroup("group1", "First group of items", 2, resourceGroup1Cost1, resourceGroup1Cost2, resourceGroup1Cost3, resourceGroup1Cost4);
        var group2 = mealDealStyleStrategy.AddMealDealGroup("group2", "Second group of items", 1, resourceGroup2Cost1, resourceGroup2Cost2, resourceGroup2Cost3, resourceGroup2Cost4);
        var group3 = mealDealStyleStrategy.AddMealDealGroup("group3", "Third group of items", 2, resourceGroup3Cost1, resourceGroup3Cost2, resourceGroup3Cost3, resourceGroup3Cost4);


        var mealDealGroupedItems = mealDealStyleStrategy.GroupItemsByMealDealGroup();
        Assert.Equal(3, mealDealGroupedItems.Count());
        Assert.Contains(mealDealGroupedItems, x => x.Key.Name == "group1");
        Assert.Contains(mealDealGroupedItems, x => x.Key.Name == "group2");
        Assert.Contains(mealDealGroupedItems, x => x.Key.Name == "group3");
        
        var group1Items = mealDealGroupedItems.FirstOrDefault(x => x.Key.Name == "group1").ToList();
       Assert.True(group1Items.IsComposedOf(resourceGroup1Cost1, resourceGroup1Cost2, resourceGroup1Cost3, resourceGroup1Cost4));
       
       var group2Items = mealDealGroupedItems.FirstOrDefault(x => x.Key.Name == "group2").ToList();
       Assert.True(group2Items.IsComposedOf(resourceGroup2Cost1, resourceGroup2Cost2, resourceGroup2Cost3, resourceGroup2Cost4));
       
       var group3Items = mealDealGroupedItems.FirstOrDefault(x => x.Key.Name == "group3").ToList();
       Assert.True(group3Items.IsComposedOf(resourceGroup3Cost1, resourceGroup3Cost2, resourceGroup3Cost3, resourceGroup3Cost4));
       
       
           
        
        
        
        

    }
}
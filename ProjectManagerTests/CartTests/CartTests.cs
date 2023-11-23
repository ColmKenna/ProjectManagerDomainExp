using ExtensionHelpersForTests;
using Measurements;
using PrimativeExtensions;
using ProjectManager;

namespace ProjectManagerTests.CartTests;

public class CartTests
{

    private Resource group1resource1, group1resource2, group1resource3, group1resource4;
    private Resource group2resource1, group2resource2, group2resource3, group2resource4;
    private Resource group3resource1, group3resource2, group3resource3, group3resource4;
    private ResourceCost resourceGroup1Cost1, resourceGroup1Cost2, resourceGroup1Cost3, resourceGroup1Cost4;
    private ResourceCost resourceGroup2Cost1, resourceGroup2Cost2, resourceGroup2Cost3, resourceGroup2Cost4;
    private ResourceCost resourceGroup3Cost1, resourceGroup3Cost2, resourceGroup3Cost3, resourceGroup3Cost4;

    public CartTests()
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
    public void CanCreateCart()
    {
        var projectOwner = ProjectOwner.Create("Test Project Owner", "Test Project Owner Description");
        var cart = Cart.Create(projectOwner, Mock.Create<IDealsRepository>(), Mock.Create<IResourceRepository>());

        Assert.NotNull(cart);
        Assert.Equal(projectOwner, cart.ProjectOwner);
    }

    [Fact]
    public void CanAddItemToCart()
    {
        var projectOwner = ProjectOwner.Create("Test Project Owner", "Test Project Owner Description");
        var dealsRepository = Mock.Create<IDealsRepository>();

        var cart = Cart.Create(projectOwner, dealsRepository, Mock.Create<IResourceRepository>());
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var measurement = new Measurement(1);
        cart.AddItem(measurement, resource);
        Assert.Single(cart.Items);
        Assert.Equal(measurement, cart.Items.First().measurement);
        Assert.Equal(resource, cart.Items.First().resource);
    }


    [Fact]
    public void CanAddMultipleItemsToCart()
    {
        var projectOwner = ProjectOwner.Create("Test Project Owner", "Test Project Owner Description");
        var dealsRepository = Mock.Create<IDealsRepository>();
        var cart = Cart.Create(projectOwner, dealsRepository, Mock.Create<IResourceRepository>());
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var resource2 = Resource.Create("Test Resource 2", "Test Resource Description 2",
            ResourceProvider.Create("Test Provider 2", "Test Provider Description 2"));
        var measurement = new Measurement(1);
        var measurement2 = new Measurement(2);
        cart.AddItem(measurement, resource);
        cart.AddItem(measurement2, resource2);
        Assert.Equal(2, cart.Items.Count);
        Assert.Equal(measurement, cart.Items.First().measurement);
        Assert.Equal(resource, cart.Items.First().resource);
        Assert.Equal(measurement2, cart.Items.Last().measurement);
        Assert.Equal(resource2, cart.Items.Last().resource);
    }


    [Fact]
    public void CalculateTotalReturnTotalOfAllItemsInCart()
    {
        var projectOwner = ProjectOwner.Create("Test Project Owner", "Test Project Owner Description");
        var dealsRepository = Mock.Create<IDealsRepository>();
        var resourceRepository = Mock.Create<IResourceRepository>();
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var resource2 = Resource.Create("Test Resource 2", "Test Resource Description 2",
            ResourceProvider.Create("Test Provider 2", "Test Provider Description 2"));

        dealsRepository.Arrange(x => x.GetDealsByResourceProvider(projectOwner, Arg.IsAny<DateTime>()))
            .Returns(new List<IDeals>()).MustBeCalled();
        resourceRepository.Arrange(x => x.GetResourceCosts()).Returns(new List<ResourceCost>()
        {
            ResourceCost.CreateInstance(1, resource, 100m),
            ResourceCost.CreateInstance(1, resource2, 200m)
        }).MustBeCalled();

        var cart = Cart.Create(projectOwner, dealsRepository, resourceRepository);
        var measurement = new Measurement(1);
        var measurement2 = new Measurement(2);
        cart.AddItem(measurement, resource);
        cart.AddItem(measurement2, resource2);

        var date = new DateTime(2023, 1, 1);
        var result = cart.CalculateTotal(date);
        result.AssertSuccess(resultValue =>
        {
            Assert.Equal(500m, resultValue.TotalAfterDiscounts);
            Assert.Equal(0m, resultValue.TotalDiscounts);
            Assert.Equal(500m, resultValue.TotalBeforeDiscounts);
            Assert.Equal(2, resultValue.Items.Count);
            Assert.Contains(resultValue.Items,
                x => x.Measurement == measurement && x.Resource == resource && x.Cost == 100m);
            Assert.Contains(resultValue.Items,
                x => x.Measurement == measurement2 && x.Resource == resource2 && x.Cost == 400m);
        });
    }


    [Fact]
    public void CalculateTotalSetsTheDiscountWhenItIsApplicable()
    {
        var projectOwner = ProjectOwner.Create("Test Project Owner", "Test Project Owner Description");
        var dealsRepository = Mock.Create<IDealsRepository>();
        var resourceRepository = Mock.Create<IResourceRepository>();
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var resource2 = Resource.Create("Test Resource 2", "Test Resource Description 2",
            ResourceProvider.Create("Test Provider 2", "Test Provider Description 2"));

        var buy3Get1Free = new BuyNGetCheapestFreeStrategy(3, 1);

        // buy3Get1Free.AddPrice(ResourceCost.CreateInstance(1, resource, 100m));
        // buy3Get1Free.AddPrice(ResourceCost.CreateInstance(1, resource2, 200m));
        buy3Get1Free.AddPrice(1, resource);
        buy3Get1Free.AddPrice(1, resource2);



        dealsRepository.Arrange(x => x.GetDealsByResourceProvider(projectOwner, Arg.IsAny<DateTime>())).Returns(
            new List<IDeals>()
            {
                buy3Get1Free
            }).MustBeCalled();
        resourceRepository.Arrange(x => x.GetResourceCosts()).Returns(new List<ResourceCost>()
        {
            ResourceCost.CreateInstance(1, resource, 100m),
            ResourceCost.CreateInstance(1, resource2, 200m)
        }).MustBeCalled();

        var cart = Cart.Create(projectOwner, dealsRepository, resourceRepository);
        var measurement = new Measurement(1);
        var measurement2 = new Measurement(2);
        cart.AddItem(measurement, resource);
        cart.AddItem(measurement2, resource2);

        var date = new DateTime(2023, 1, 1);
        var result = cart.CalculateTotal(date);
        result.AssertSuccess(resultValue =>
        {
            Assert.Equal(500m, resultValue.TotalBeforeDiscounts);
            Assert.Equal(100m, resultValue.TotalDiscounts);
            Assert.Equal(400m, resultValue.TotalAfterDiscounts);
            Assert.Equal(2, resultValue.Items.Count);
            Assert.Equal(1,resultValue.Discounts.Count);
            // Discounts applied to cheapest item
            // the items uses in the discount lists all the items
            Assert.Equal(3,resultValue.Discounts.First().ItemsUsedForDiscount.Count);
            Assert.Equal(1, resultValue.Discounts.First().ItemsUsedForDiscount.Count(x=>x.Resource == resource));
            Assert.Equal(2, resultValue.Discounts.First().ItemsUsedForDiscount.Count(x=>x.Resource == resource2));
            Assert.Single(resultValue.Discounts.First().ItemsDiscounted);
            Assert.Equal(1, resultValue.Discounts.First().ItemsDiscounted.Count(x=>x.Resource == resource));
            
            

        });
    }
    
    [Fact]
    public void CalculateTotalSetsTheDiscountWhenItIsApplicableMultipleTimes()
    {
        var projectOwner = ProjectOwner.Create("Test Project Owner", "Test Project Owner Description");
        var dealsRepository = Mock.Create<IDealsRepository>();
        var resourceRepository = Mock.Create<IResourceRepository>();
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var resource2 = Resource.Create("Test Resource 2", "Test Resource Description 2",
            ResourceProvider.Create("Test Provider 2", "Test Provider Description 2"));

        var buy3Get1Free = new BuyNGetCheapestFreeStrategy(3, 1);

        buy3Get1Free.AddPrice(1, resource);
        buy3Get1Free.AddPrice(1, resource2);


        dealsRepository.Arrange(x => x.GetDealsByResourceProvider(projectOwner, Arg.IsAny<DateTime>())).Returns(
            new List<IDeals>()
            {
                buy3Get1Free
            }).MustBeCalled();
        resourceRepository.Arrange(x => x.GetResourceCosts()).Returns(new List<ResourceCost>()
        {
            ResourceCost.CreateInstance(1, resource, 100m),
            ResourceCost.CreateInstance(1, resource2, 200m)
        }).MustBeCalled();

        var cart = Cart.Create(projectOwner, dealsRepository, resourceRepository);
        var measurement = new Measurement(2);
        var measurement2 = new Measurement(4);
        cart.AddItem(measurement, resource);
        cart.AddItem(measurement2, resource2);

        var date = new DateTime(2023, 1, 1);
        var result = cart.CalculateTotal(date);
        result.AssertSuccess(resultValue =>
        {
            var aa = resultValue.Discounts.First().GetItemsUsedForDiscountSummarised();
            var bb = resultValue.Discounts.First().GetItemsDiscountedSummarised();
            Assert.Equal(1000m, resultValue.TotalBeforeDiscounts);
            Assert.Equal(300m, resultValue.TotalDiscounts);
            Assert.Equal(700m, resultValue.TotalAfterDiscounts);
            Assert.Equal(2, resultValue.Items.Count);
            Assert.Equal(1,resultValue.Discounts.Count);

            

            Assert.Equal(2, resultValue.Discounts.First().ItemsUsedForDiscountGrouped.GetAmountOfResource(resource).First().Measurement.GetQty().ToInt() );
            Assert.Equal(4, resultValue.Discounts.First().ItemsUsedForDiscountGrouped.GetAmountOfResource(resource2).First().Measurement.GetQty().ToInt() );
            
            Assert.Equal(1, resultValue.Discounts.First().ItemsDiscountedGrouped.GetAmountOfResource(resource).First().Measurement.GetQty().ToInt() );
            Assert.Equal(1, resultValue.Discounts.First().ItemsDiscountedGrouped.GetAmountOfResource(resource2).First().Measurement.GetQty().ToInt() );
            
        });
    }

    [Fact]
    public void AppliesWhenMultipleDifferentDealApplicipable()
    {
        var mealDealStyleStrategy = new MealDealStyleStrategy(300m);
var group1 = mealDealStyleStrategy.AddMealDealGroup("group1", "First group of items", 2, (resourceGroup1Cost1.Quantity, resourceGroup1Cost1.Resource), (resourceGroup1Cost2.Quantity, resourceGroup1Cost2.Resource), (resourceGroup1Cost3.Quantity, resourceGroup1Cost3.Resource), (resourceGroup1Cost4.Quantity, resourceGroup1Cost4.Resource));
var group2 = mealDealStyleStrategy.AddMealDealGroup("group2", "Second group of items", 1, (resourceGroup2Cost1.Quantity, resourceGroup2Cost1.Resource), (resourceGroup2Cost2.Quantity, resourceGroup2Cost2.Resource), (resourceGroup2Cost3.Quantity, resourceGroup2Cost3.Resource), (resourceGroup2Cost4.Quantity, resourceGroup2Cost4.Resource));
var group3 = mealDealStyleStrategy.AddMealDealGroup("group3", "Third group of items", 2, (resourceGroup3Cost1.Quantity, resourceGroup3Cost1.Resource), (resourceGroup3Cost2.Quantity, resourceGroup3Cost2.Resource), (resourceGroup3Cost3.Quantity, resourceGroup3Cost3.Resource), (resourceGroup3Cost4.Quantity, resourceGroup3Cost4.Resource));

        var projectOwner = ProjectOwner.Create("Test Project Owner", "Test Project Owner Description");
        var dealsRepository = Mock.Create<IDealsRepository>();
        var resourceRepository = Mock.Create<IResourceRepository>();
        var resource = Resource.Create("Test Resource", "Test Resource Description", ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var resource2 = Resource.Create("Test Resource 2", "Test Resource Description 2", ResourceProvider.Create("Test Provider 2", "Test Provider Description 2"));

        var buy3Get1Free = new BuyNGetCheapestFreeStrategy(3, 1);

        buy3Get1Free.AddPrice(1, resource);
        buy3Get1Free.AddPrice(1, resource2);

        dealsRepository.Arrange(x => x.GetDealsByResourceProvider(projectOwner, Arg.IsAny<DateTime>())).Returns(
            new List<IDeals>()
            {
                buy3Get1Free,mealDealStyleStrategy
            }).MustBeCalled();
        resourceRepository.Arrange(x => x.GetResourceCosts()).Returns(new List<ResourceCost>()
        {
            ResourceCost.CreateInstance(1, resource, 100m),
            ResourceCost.CreateInstance(1, resource2, 200m),
            resourceGroup1Cost1, resourceGroup1Cost2, resourceGroup1Cost3, resourceGroup1Cost4,
            resourceGroup2Cost1, resourceGroup2Cost2, resourceGroup2Cost3, resourceGroup2Cost4,
            resourceGroup3Cost1, resourceGroup3Cost2, resourceGroup3Cost3, resourceGroup3Cost4
        }).MustBeCalled();

        var cart = Cart.Create(projectOwner, dealsRepository, resourceRepository);
        var measurement = new Measurement(1);
        var measurement2 = new Measurement(2);
        cart.AddItem(measurement, resource);
        cart.AddItem(measurement2, resource2);
        cart.AddItem(measurement2, group1resource1);
        cart.AddItem(measurement2, group1resource2);
        cart.AddItem(measurement, group2resource2);
        cart.AddItem(measurement2, group3resource1);
        cart.AddItem(measurement2, group3resource2);
        
        var date = new DateTime(2023, 1, 1);
        var result = cart.CalculateTotal(date);
        
        Assert.True(result.IsValid );
        
        
    }
    
    [Fact]
    public void ItemCantbeUsedForTwiceInDeals()
    {
        var mealDealStyleStrategy = new MealDealStyleStrategy(300m);
var group1 = mealDealStyleStrategy.AddMealDealGroup("group1", "First group of items", 2, (resourceGroup1Cost1.Quantity, resourceGroup1Cost1.Resource), (resourceGroup1Cost2.Quantity, resourceGroup1Cost2.Resource), (resourceGroup1Cost3.Quantity, resourceGroup1Cost3.Resource), (resourceGroup1Cost4.Quantity, resourceGroup1Cost4.Resource));
var group2 = mealDealStyleStrategy.AddMealDealGroup("group2", "Second group of items", 1, (resourceGroup2Cost1.Quantity, resourceGroup2Cost1.Resource), (resourceGroup2Cost2.Quantity, resourceGroup2Cost2.Resource), (resourceGroup2Cost3.Quantity, resourceGroup2Cost3.Resource), (resourceGroup2Cost4.Quantity, resourceGroup2Cost4.Resource));
var group3 = mealDealStyleStrategy.AddMealDealGroup("group3", "Third group of items", 2, (resourceGroup3Cost1.Quantity, resourceGroup3Cost1.Resource), (resourceGroup3Cost2.Quantity, resourceGroup3Cost2.Resource), (resourceGroup3Cost3.Quantity, resourceGroup3Cost3.Resource), (resourceGroup3Cost4.Quantity, resourceGroup3Cost4.Resource));

        var projectOwner = ProjectOwner.Create("Test Project Owner", "Test Project Owner Description");
        var dealsRepository = Mock.Create<IDealsRepository>();
        var resourceRepository = Mock.Create<IResourceRepository>();
        var resource = Resource.Create("Test Resource", "Test Resource Description", ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var resource2 = Resource.Create("Test Resource 2", "Test Resource Description 2", ResourceProvider.Create("Test Provider 2", "Test Provider Description 2"));

        var buy3Get1Free = new BuyNGetCheapestFreeStrategy(3, 1);

        buy3Get1Free.AddPrice(resourceGroup1Cost1.Quantity , resourceGroup1Cost1.Resource);
        buy3Get1Free.AddPrice(1, resource2);
        

        dealsRepository.Arrange(x => x.GetDealsByResourceProvider(projectOwner, Arg.IsAny<DateTime>())).Returns(
            new List<IDeals>()
            {
                buy3Get1Free,mealDealStyleStrategy
            }).MustBeCalled();
        resourceRepository.Arrange(x => x.GetResourceCosts()).Returns(new List<ResourceCost>()
        {
            ResourceCost.CreateInstance(1, resource, 100m),
            ResourceCost.CreateInstance(1, resource2, 200m),
            resourceGroup1Cost1, resourceGroup1Cost2, resourceGroup1Cost3, resourceGroup1Cost4,
            resourceGroup2Cost1, resourceGroup2Cost2, resourceGroup2Cost3, resourceGroup2Cost4,
            resourceGroup3Cost1, resourceGroup3Cost2, resourceGroup3Cost3, resourceGroup3Cost4
        }).MustBeCalled();

        var cart = Cart.Create(projectOwner, dealsRepository, resourceRepository);
        var measurement = new Measurement(1);
        var measurement2 = new Measurement(2);
        cart.AddItem(measurement, resource);
        cart.AddItem(measurement2, resource2);
        cart.AddItem(measurement2, group1resource1);
        cart.AddItem(measurement2, group1resource2);
        cart.AddItem(measurement, group2resource2);
        cart.AddItem(measurement2, group3resource1);
        cart.AddItem(measurement2, group3resource2);
        
        var date = new DateTime(2023, 1, 1);
        var result = cart.CalculateTotal(date);
        
        result.AssertSuccess(cart =>
        {
            var discountsContaingGroup1Resource1 = cart.Discounts.Where(x => x.ItemsUsedForDiscount.Any(y => y.Resource == group1resource1));
            Assert.Single(discountsContaingGroup1Resource1);
        });
            
        }
        
    }

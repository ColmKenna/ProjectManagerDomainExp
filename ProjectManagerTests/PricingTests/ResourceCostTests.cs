using Measurements;
using ProjectManager;

namespace ProjectManagerTests.PricingTests;

public class ResourceCostTests
{
    private ResourceProvider resourceProvider;

    public ResourceCostTests()
    {
        resourceProvider = ResourceProvider.Create("Test Provider", "Test Provider Description");
    }


    [Fact]
    public void CanCreateResourceCostForExactAmount()
    {
        Resource resource;
        resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceCost = ResourceCost.CreateInstance(1, resource, 10m);
        Assert.Equal(1, resourceCost.Quantity.GetQty());
        Assert.Equal(resource, resourceCost.Resource);
        Assert.Equal(10m, resourceCost.Cost);
        Assert.Equal(CostType.ExactMultiplesOnly, resourceCost.CostType);
    }
    
    [Fact]
    public void CanCreateResourceCostForExactAmountWithExactOnlyCostType()
    {
        Resource resource;
        resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceCost = ResourceCost.CreateInstance(1, resource, 10m, CostType.ExactOnly);
        
        var result = resourceCost.GetCostFor(1);
        Assert.True(result.IsValid);
        Assert.Equal(10m, result.Value);
    }
    
    [Fact]
    public void ResultFailsIfExactOnlyAskedForMoreThanExactOnly()
    {
        Resource resource;
        resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceCost = ResourceCost.CreateInstance(1, resource, 10m, CostType.ExactOnly);
        
        var result = resourceCost.GetCostFor(2);
        Assert.True(result.IsFailure);
        Assert.Equal("The quantity 2 is not an exact match for the cost 1", result.ErrorMessage);
    }
    
    
    [Fact]
    public void CanCreateResourceCostForExactAmountWithExactMultiplesOnlyCostType()
    {
        Resource resource;
        resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceCost = ResourceCost.CreateInstance(1, resource, 10m, CostType.ExactMultiplesOnly);
        
        var result = resourceCost.GetCostFor(1);
        Assert.True(result.IsValid);
        Assert.Equal(10m, result.Value);
    }
    

    
    [Fact]
    public void CanCreateResourceCostForExactAmountWithAnyAmountCostType()
    {
        Resource resource;
        resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceCost = ResourceCost.CreateInstance(Weight.Kilograms(1)  , resource, 10m, CostType.AnyAmount);
        
        var result = resourceCost.GetCostFor(Weight.Kilograms(1.5m) + Weight.Grams(500m)); 
        Assert.True(result.IsValid);
        Assert.Equal(20m, result.Value);
    }
    
    [Fact]
    public void AnyAmountAboveFailsIfAmountIsBelowInitialAmount()
    {
        Resource resource;
        resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceCost = ResourceCost.CreateInstance(Weight.Kilograms(1)  , resource, 10m, CostType.AnyAmountAbove);
        
        var result = resourceCost.GetCostFor(Weight.Kilograms(0.5m)); 
        Assert.True(result.IsFailure);
        Assert.Equal("The quantity 0.5 Kilograms is not above the cost 1 Kilogram", result.ErrorMessage);
    }
    
    // Any amount works even if amount is below initial amount
    [Fact]
    public void AnyAmountWorksEvenIfAmountIsBelowInitialAmount()
    {
        Resource resource;
        resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceCost = ResourceCost.CreateInstance(Weight.Kilograms(1)  , resource, 10m, CostType.AnyAmount);
        
        var result = resourceCost.GetCostFor(Weight.Kilograms(0.5m)); 
        Assert.True(result.IsValid);
        Assert.Equal(5m, result.Value);
    }
    
    // Any amount above works can for for days to weeks if no starting date is sent
    [Fact]
    public void AnyAmountAboveWorksCanForForDaysToWeeksIfNoStartingDateIsSent()
    {
        Resource resource;
        resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceCost = ResourceCost.CreateInstance( Duration.Days(1)  , resource, 10m, CostType.AnyAmountAbove);
        
        var result = resourceCost.GetCostFor(Duration.Weeks(1)); 
        Assert.True(result.IsValid);
        Assert.Equal(70m, result.Value);
    }
    
    


    // Any amount above fails converting to month if no starting date is sent
    [Fact]
    public void AnyAmountAboveFailsForDaysToMonthIfNoStartingDateIsSent()
    {
        Resource resource;
        resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceCost = ResourceCost.CreateInstance(Duration.Days(1), resource, 10m, CostType.AnyAmountAbove);

        var result = resourceCost.GetCostFor(Duration.Months(1));
        Assert.True(result.IsFailure);
        Assert.Equal("Date must be provided when converting to Months from Days", result.ErrorMessage);
    }
    

    // Any amount above works for days to month if no starting date is sent
    [Fact]
    public void AnyAmountAboveWorksForDaysToMonthIfStartingDateIsSent()
    {
        Resource resource;
        resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceCost = ResourceCost.CreateInstance(Duration.Days(1), resource, 10m, CostType.AnyAmountAbove);

        var result31DayMonth = resourceCost.GetCostFor(Duration.Months(1), new DateTime(2023, 1, 1));
        var result28dayMonth = resourceCost.GetCostFor(Duration.Months(1), new DateTime(2023, 2, 1));

        Assert.True(result31DayMonth.IsValid);
        Assert.Equal(310m, result31DayMonth.Value);

        Assert.True(result28dayMonth.IsValid);
        Assert.Equal(280m, result28dayMonth.Value);

    }
    
    

    // Other conversions tests are in the measurement tests


    
    
}
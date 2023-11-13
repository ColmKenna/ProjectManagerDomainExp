using Measurements;
using ProjectManager;

namespace ProjectManagerTests.PricingTests;

public class QuantityPricingStrategyTests
{
    //QuantityPricingStrategy quantityPricingStrategy;

    [Fact]
    public void CanCreateQtyPricingStrategyWithInitialPrice()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var quantity = Duration.Hours(1);
        var cost = 100m;
        var quantityCost = ResourceCost.CreateInstance(quantity, resource, cost);
        var quantityPricingStrategy = new QuantityPricingStrategy(quantityCost);
        var result = quantityPricingStrategy.GetPrice(Duration.Hours(3));
        Assert.Equal(cost * 3, result);
    }

    [Fact]
    public void CanAddQuantityWithBetterPriceForMore()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var quantity1 = Duration.Hours(1);
        var quantity3 = Duration.Hours(3);
        var cost1 = 100m;
        var cost3 = 250m;
        var quantityCost = ResourceCost.CreateInstance(quantity1, resource, cost1);
        var quantityPricingStrategy = new QuantityPricingStrategy(quantityCost);
        var result3 = quantityPricingStrategy.AddPrice(ResourceCost.CreateInstance(quantity3, resource, cost3));
        var result = quantityPricingStrategy.GetPrice(Duration.Hours(3));

        Assert.True(result3.IsValid);
        Assert.Equal(cost3, result);
    }

    [Fact]
    public void GetsCorrectPriceWhenUsingMultiplePricePoints()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var quantity1 = Duration.Hours(1);
        var quantity3 = Duration.Hours(3);
        var cost1 = 100m;
        var cost3 = 250m;
        var quantityCost = ResourceCost.CreateInstance(quantity1, resource, cost1);
        var quantityPricingStrategy = new QuantityPricingStrategy(quantityCost);
        var result3 = quantityPricingStrategy.AddPrice(ResourceCost.CreateInstance(quantity3, resource, cost3));
        var result = quantityPricingStrategy.GetPrice(Duration.Hours(4));

        Assert.True(result3.IsValid);
        Assert.Equal(cost3 + cost1, result);
    }

    [Fact]
    public void CantAddQuantityWithWorsePriceForHigherQty()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var quantity1 = Duration.Hours(1);
        var quantity3 = Duration.Hours(3);
        var quantity5 = Duration.Hours(5);
        var cost1 = 100m;
        var cost3 = 250m;
        var cost4 = 850m;
        var quantityCost = ResourceCost.CreateInstance(quantity1, resource, cost1);
        var quantityPricingStrategy = new QuantityPricingStrategy(quantityCost);
        var result3 = quantityPricingStrategy.AddPrice(ResourceCost.CreateInstance(quantity3, resource, cost3));
        var result4 = quantityPricingStrategy.AddPrice(ResourceCost.CreateInstance(quantity5, resource, cost4));

        Assert.True(result3.IsValid);
        Assert.False(result4.IsValid);
    }

    [Fact]
    public void CanUseDifferentMeasurementsForPrice()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var quantity1 = Weight.Grams(1);
        var quantity3 = Weight.Kilograms(1);
        var cost1 = 10m;
        var cost3 = 800m;
        var quantityCost = ResourceCost.CreateInstance(quantity1, resource, cost1);
        var quantityPricingStrategy = new QuantityPricingStrategy(quantityCost);
        var result3 = quantityPricingStrategy.AddPrice(ResourceCost.CreateInstance(quantity3, resource, cost3));
        var result = quantityPricingStrategy.GetPrice(Weight.Kilograms(1.5m));

        var expected = (cost1 * 500) + cost3;
        Assert.True(result3.IsValid);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CantAddQuantityWithDifferentMeasurementType()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var quantity1 = Duration.Hours(1);
        var quantity3 = Weight.Kilograms(3);
        var cost1 = 100m;
        var cost3 = 250m;
        var quantityCost = ResourceCost.CreateInstance(quantity1, resource, cost1);
        var quantityPricingStrategy = new QuantityPricingStrategy(quantityCost);
        var result3 = quantityPricingStrategy.AddPrice(ResourceCost.CreateInstance(quantity3, resource, cost3));

        Assert.False(result3.IsValid);
        Assert.Equal($"Can't add {quantity3} to price strategy for ${quantity1} ", result3.ErrorMessage);
    }

    [Fact]
    public void CantAddQuantityWithDifferentResource()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var resource2 = Resource.Create("Test Resource 2", "Test Resource Description 2",
            ResourceProvider.Create("Test Provider 2", "Test Provider Description 2"));
        var quantity1 = Duration.Hours(1);
        var quantity3 = Duration.Hours(3);
        var cost1 = 100m;
        var cost3 = 250m;
        var quantityCost = ResourceCost.CreateInstance(quantity1, resource, cost1);
        var quantityPricingStrategy = new QuantityPricingStrategy(quantityCost);
        var result3 = quantityPricingStrategy.AddPrice(ResourceCost.CreateInstance(quantity3, resource2, cost3));

        Assert.False(result3.IsValid);
        Assert.Equal($"Can't add {resource2.Name} to price strategy for ${resource.Name} ", result3.ErrorMessage);
    }

    [Fact]
    public void ThirtyTwoDaysComesToMonthAndonedayFor30DayMonth()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var quantity1 = Duration.Days(1);
        var quantity3 = Duration.Months(1);
        var cost1 = 100m;
        var cost3 = 2500m;
        var quantityCost = ResourceCost.CreateInstance(quantity1, resource, cost1);
        var quantityPricingStrategy = new QuantityPricingStrategy(quantityCost);
        var result3 = quantityPricingStrategy.AddPrice(ResourceCost.CreateInstance(quantity3, resource, cost3));

        var month = new DateTime(2020, 4, 1);


        Assert.True(result3.IsValid);
        Assert.Equal(2700m, quantityPricingStrategy.GetPrice(Duration.Days(32), month));
    }

    [Fact]
    public void ThirtyTwoDaysComesToMonthAndonedayFor31DayMonth()
    {
        var resource = Resource.Create("Test Resource", "Test Resource Description",
            ResourceProvider.Create("Test Provider", "Test Provider Description"));
        var quantity1 = Duration.Days(1);
        var quantity3 = Duration.Months(1);
        var cost1 = 100m;
        var cost3 = 2500m;
        var quantityCost = ResourceCost.CreateInstance(quantity1, resource, cost1);
        var quantityPricingStrategy = new QuantityPricingStrategy(quantityCost);
        var result3 = quantityPricingStrategy.AddPrice(ResourceCost.CreateInstance(quantity3, resource, cost3));

        var month = new DateTime(2020, 1, 1);


        Assert.True(result3.IsValid);
        Assert.Equal(2600m, quantityPricingStrategy.GetPrice(Duration.Days(32), month));
    }
}
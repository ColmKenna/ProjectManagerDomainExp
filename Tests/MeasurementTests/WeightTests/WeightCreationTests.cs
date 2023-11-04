using Measurements;

namespace MeasurementTests.WeightTests;

public class WeightCreationTests
{
    [Fact]
    public void Milligrams_Creation_Test()
    {
        var weight = Weight.Milligrams(100);
        Assert.Equal(100, weight.GetAsAmount(WeightUnit.Milligrams));
    }

    [Fact]
    public void Grams_Creation_Test()
    {
        var weight = Weight.Grams(50);
        Assert.Equal(50, weight.GetAsAmount(WeightUnit.Grams));
    }

    [Fact]
    public void Kilograms_Creation_Test()
    {
        var weight = Weight.Kilograms(2);
        Assert.Equal(2, weight.GetAsAmount(WeightUnit.Kilograms));
    }

    [Fact]
    public void MetricTons_Creation_Test()
    {
        var weight = Weight.MetricTons(1);
        Assert.Equal(1, weight.GetAsAmount(WeightUnit.MetricTons));
    }

    [Fact]
    public void Tons_Creation_Test()
    {
        var weight = Weight.USTons(1);
        Assert.Equal(1, weight.GetAsAmount(WeightUnit.USTons));
    }

    [Fact]
    public void Ounces_Creation_Test()
    {
        var weight = Weight.Ounces(16);
        Assert.Equal(16, weight.GetAsAmount(WeightUnit.Ounces));
    }

    [Fact]
    public void Pounds_Creation_Test()
    {
        var weight = Weight.Pounds(10);
        Assert.Equal(10, weight.GetAsAmount(WeightUnit.Pounds));
    }

    [Fact]
    public void Stones_Creation_Test()
    {
        var weight = Weight.Stones(5);
        Assert.Equal(5, weight.GetAsAmount(WeightUnit.Stones));
    }
}

public class WeightAdditionTests
{
    [Fact]
    public void Add_Milligrams_To_Milligrams()
    {
        var weight1 = Weight.Milligrams(100);
        var weight2 = Weight.Milligrams(150);
        var result = weight1 + weight2;

        Assert.Equal(250, result.GetAsAmount(WeightUnit.Milligrams));
    }

    [Fact]
    public void Add_Grams_To_Grams()
    {
        var weight1 = Weight.Grams(50);
        var weight2 = Weight.Grams(25);
        var result = weight1 + weight2;

        Assert.Equal(75, result.GetAsAmount(WeightUnit.Grams));
    }

    [Fact]
    public void Add_Kilograms_To_Kilograms()
    {
        var weight1 = Weight.Kilograms(2);
        var weight2 = Weight.Kilograms(3);
        var result = weight1 + weight2;

        Assert.Equal(5, result.GetAsAmount(WeightUnit.Kilograms));
    }

    [Fact]
    public void Add_MetricTons_To_MetricTons()
    {
        var weight1 = Weight.MetricTons(1);
        var weight2 = Weight.MetricTons(2);
        var result = weight1 + weight2;

        Assert.Equal(3, result.GetAsAmount(WeightUnit.MetricTons));
    }

    [Fact]
    public void Add_Ounces_To_Ounces()
    {
        var weight1 = Weight.Ounces(16);
        var weight2 = Weight.Ounces(32);
        var result = weight1 + weight2;

        Assert.Equal(48, result.GetAsAmount(WeightUnit.Ounces));
    }


    [Fact]
    public void Add_Different_Units()
    {
        var weight1 = Weight.Grams(500); // 500 grams
        var weight2 = Weight.Kilograms(1); // 1 kilogram
        var result = weight1 + weight2;

        Assert.Equal(1.5m, result.GetAsAmount(WeightUnit.Kilograms)); // 1.5 kilograms
    }


    [Fact]
    public void Add_Pounds_To_Pounds()
    {
        var weight1 = Weight.Pounds(10);
        var weight2 = Weight.Pounds(20);
        var result = weight1 + weight2;

        Assert.Equal(30, result.GetAsAmount(WeightUnit.Pounds));
    }

    [Fact]
    public void Add_Stones_To_Stones()
    {
        var weight1 = Weight.Stones(5);
        var weight2 = Weight.Stones(7);
        var result = weight1 + weight2;

        Assert.Equal(12, result.GetAsAmount(WeightUnit.Stones));
    }

    [Fact]
    public void Add_Tons_To_Tons()
    {
        var weight1 = Weight.USTons(3);
        var weight2 = Weight.USTons(2);
        var result = weight1 + weight2;

        Assert.Equal(5, result.GetAsAmount(WeightUnit.USTons));
    }

    [Fact]
    public void Add_Milligrams_To_Grams()
    {
        var weight1 = Weight.Milligrams(2000); // 2000 milligrams
        var weight2 = Weight.Grams(1); // 1 gram
        var result = weight1 + weight2;

        Assert.Equal(3, result.GetAsAmount(WeightUnit.Grams)); // 2 grams
    }

    [Fact]
    public void Add_Grams_To_Kilograms()
    {
        var weight1 = Weight.Grams(500); // 500 grams
        var weight2 = Weight.Kilograms(1); // 1 kilogram
        var result = weight1 + weight2;

        Assert.Equal(1.5m, result.GetAsAmount(WeightUnit.Kilograms)); // 1.5 kilograms
    }

    [Fact]
    public void Add_Kilograms_To_MetricTons()
    {
        var weight1 = Weight.Kilograms(500); // 500 kilograms
        var weight2 = Weight.MetricTons(1); // 1 metric ton
        var result = weight1 + weight2;

        Assert.Equal(1.5m, result.GetAsAmount(WeightUnit.MetricTons)); // 1.5 metric tons
    }

    [Fact]
    public void Add_Ounces_To_Pounds()
    {
        var weight1 = Weight.Ounces(16); // 16 ounces
        var weight2 = Weight.Pounds(1); // 1 pound
        var result = weight1 + weight2;

        Assert.Equal(2, result.GetAsAmount(WeightUnit.Pounds)); // 2 pounds
    }
    
    [Fact]
    public void Add_Pounds_To_Stones()
    {
        var weight1 = Weight.Pounds(14); // 14 pounds
        var weight2 = Weight.Stones(1); // 1 stone
        var result = weight1 + weight2;

        Assert.Equal(2, result.GetAsAmount(WeightUnit.Stones)); // 2 stones
    }
   
    
    [Fact]
    public void Add_Milligrams_To_Kilograms()
    {
        var weight1 = Weight.Milligrams(2000); // 2000 milligrams
        var weight2 = Weight.Kilograms(1); // 1 kilogram
        var result = weight1 + weight2;

        Assert.Equal(1.002m, result.GetAsAmount(WeightUnit.Kilograms)); // 2.002 kilograms
    }
    
    
}
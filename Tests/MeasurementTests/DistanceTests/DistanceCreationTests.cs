using Measurements;

namespace MeasurementTests.DistanceTests;

public class DistanceCreationTests
{
    [Fact]
    public void CreateMillimetersTest()
    {
        var distance = Distance.Millimeters(5);
        Assert.Equal(DistanceUnit.Millimeters, distance.DistanceType);
        Assert.Equal(5, distance.GetAs(DistanceUnit.Millimeters) );
    }

    [Fact]
    public void CreateCentimetersTest()
    {
        var distance = Distance.Centimeters(10);
        Assert.Equal(DistanceUnit.Centimeters, distance.DistanceType);
        Assert.Equal(10, distance.GetAs(DistanceUnit.Centimeters));
    }

    [Fact]
    public void CreateMetersTest()
    {
        var distance = Distance.Meters(1);
        Assert.Equal(DistanceUnit.Meters, distance.DistanceType);
        Assert.Equal(1, distance.GetAs(DistanceUnit.Meters));
    }

    [Fact]
    public void CreateKilometersTest()
    {
        var distance = Distance.Kilometers(2);
        Assert.Equal(DistanceUnit.Kilometers, distance.DistanceType);
        Assert.Equal(2, distance.GetAs(DistanceUnit.Kilometers));
    }

    [Fact]
    public void CreateInchesTest()
    {
        var distance = Distance.Inches(12);
        Assert.Equal(DistanceUnit.Inches, distance.DistanceType);
        Assert.Equal(12, distance.GetAs(DistanceUnit.Inches));
    }

    [Fact]
    public void CreateFeetTest()
    {
        var distance = Distance.Feet(3);
        Assert.Equal(DistanceUnit.Feet, distance.DistanceType);
        Assert.Equal(3, distance.GetAs(DistanceUnit.Feet));
    }

    [Fact]
    public void CreateYardsTest()
    {
        var distance = Distance.Yards(4);
        Assert.Equal(DistanceUnit.Yards, distance.DistanceType);
        Assert.Equal(4, distance.GetAs(DistanceUnit.Yards));
    }

    [Fact]
    public void CreateMilesTest()
    {
        var distance = Distance.Miles(5);
        Assert.Equal(DistanceUnit.Miles, distance.DistanceType);
        Assert.Equal(5, distance.GetAs(DistanceUnit.Miles));
    }
}
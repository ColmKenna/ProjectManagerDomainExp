using Measurements;

namespace MeasurementTests.DistanceTests;

public class DistanceAdditionTests
{
    [Fact]
    public void AddDistance_SameUnit()
    {
        var distance1 = new Measurements.Distance(DistanceUnit.Inches, 5);
        var distance2 = new Measurements.Distance(DistanceUnit.Inches, 10);
        distance1.AddDistance(distance2);
        Assert.Equal(15, distance1.Amount);
    }

    [Fact]
    public void AddDistance_DifferentUnits()
    {
        var distance1 = new Measurements.Distance(DistanceUnit.Feet, 1);
        var distance2 = new Measurements.Distance(DistanceUnit.Inches, 12);
        distance1.AddDistance(distance2);
        Assert.Equal(1, distance1.Amount);
        Assert.Contains(distance1.OtherDistances, d => d.DistanceType == DistanceUnit.Inches && d.Amount == 12);
    }

    [Fact]
    public void AddDistance_MultipleTimes()
    {
        var distance1 = new Distance(DistanceUnit.Feet, 1);
        var distance2 = new Distance(DistanceUnit.Inches, 12);
        var distance3 = new Distance(DistanceUnit.Yards, 3);
        distance1.AddDistance(distance2).AddDistance(distance3);
        Assert.Equal(1, distance1.Amount);
        Assert.Contains(distance1.OtherDistances, d => d.DistanceType == DistanceUnit.Inches && d.Amount == 12);
        Assert.Contains(distance1.OtherDistances, d => d.DistanceType == DistanceUnit.Yards && d.Amount == 3);
    }

    [Fact]
    public void AddDistance_WithOtherDistances()
    {
        var distance1 = new Distance(DistanceUnit.Feet, 1);
        var subDistance = new Distance(DistanceUnit.Inches, 10);
        var distance2 = new Distance(DistanceUnit.Yards, 3);
        distance2.AddDistance(subDistance); // Add subDistance to distance2's OtherDistances
        distance1.AddDistance(distance2);
        Assert.Equal(1, distance1.Amount);
        Assert.Contains(distance1.OtherDistances, d => d.DistanceType == DistanceUnit.Yards && d.Amount == 3);
        Assert.Contains(distance1.OtherDistances, d => d.DistanceType == DistanceUnit.Inches && d.Amount == 10);
    }
}
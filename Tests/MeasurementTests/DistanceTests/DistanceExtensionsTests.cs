namespace MeasurementTests.DistanceTests;

public class DistanceExtensionsTests
{
    [Fact]
    public void CanOrderWhenAllTheSameDistanceUnit()
    {
        var distance1 = new Distance(DistanceUnit.Meters, 1);
        var distance2 = new Distance(DistanceUnit.Meters, 4);
        var distance3 = new Distance(DistanceUnit.Meters, 3);
        var distance4 = new Distance(DistanceUnit.Meters, 2);
        var distance5 = new Distance(DistanceUnit.Meters, 5);

        var distances = new List<Distance> {distance5, distance4, distance3, distance2, distance1};
        var distacesInOrder =  distances.GetInOrder().ToList();
        Assert.Equal(distance1, distacesInOrder[0]);
        Assert.Equal(distance4, distacesInOrder[1]);
        Assert.Equal(distance3, distacesInOrder[2]);
        Assert.Equal(distance2, distacesInOrder[3]);
        Assert.Equal(distance5, distacesInOrder[4]);
    }
    

    [Fact]
    public void CanOrderWhenAllDifferentDistanceUnit()
    {
        var distance1 = new Distance(DistanceUnit.Meters, 1);
        var distance2 = new Distance(DistanceUnit.Millimeters, 4);
        var distance3 = new Distance(DistanceUnit.Centimeters, 3);
        var distance4 = new Distance(DistanceUnit.Kilometers, 2);
        var distance5 = new Distance(DistanceUnit.Inches, 5);
        var distance6 = new Distance(DistanceUnit.Miles, 2);

        var distances = new List<Distance> {distance5, distance4, distance3, distance2, distance1, distance6};
        var distancesInorder =  distances.GetInOrder().ToList();
        Assert.Equal(distance2, distancesInorder[0]);
        Assert.Equal(distance3, distancesInorder[1]);
        Assert.Equal(distance5, distancesInorder[2]);
        Assert.Equal(distance1, distancesInorder[3]);
        Assert.Equal(distance4, distancesInorder[4]);
        Assert.Equal(distance6, distancesInorder[5]);
    }
    
    // Same tests as above, but descending
    [Fact]
    public void CanOrderDescendingWhenAllTheSameDistanceUnit()
    {
        var distance1 = new Distance(DistanceUnit.Meters, 1);
        var distance2 = new Distance(DistanceUnit.Meters, 4);
        var distance3 = new Distance(DistanceUnit.Meters, 3);
        var distance4 = new Distance(DistanceUnit.Meters, 2);
        var distance5 = new Distance(DistanceUnit.Meters, 5);

        var distances = new List<Distance> {distance5, distance4, distance3, distance2, distance1};
        var distacesInOrder =  distances.GetInOrderDescending().ToList();
        Assert.Equal(distance5, distacesInOrder[0]);
        Assert.Equal(distance2, distacesInOrder[1]);
        Assert.Equal(distance3, distacesInOrder[2]);
        Assert.Equal(distance4, distacesInOrder[3]);
        Assert.Equal(distance1, distacesInOrder[4]);
    }
    
    [Fact]
    public void CanOrderDescendingWhenAllDifferentDistanceUnit()
    {
        var distance1 = new Distance(DistanceUnit.Meters, 1);
        var distance2 = new Distance(DistanceUnit.Millimeters, 4);
        var distance3 = new Distance(DistanceUnit.Centimeters, 3);
        var distance4 = new Distance(DistanceUnit.Kilometers, 2);
        var distance5 = new Distance(DistanceUnit.Inches, 5);
        var distance6 = new Distance(DistanceUnit.Miles, 2);

        var distances = new List<Distance> {distance5, distance4, distance3, distance2, distance1, distance6};
        var distancesInorder =  distances.GetInOrderDescending().ToList();
        Assert.Equal(distance6, distancesInorder[0]);
        Assert.Equal(distance4, distancesInorder[1]);
        Assert.Equal(distance1, distancesInorder[2]);
        Assert.Equal(distance5, distancesInorder[3]);
        Assert.Equal(distance3, distancesInorder[4]);
        Assert.Equal(distance2, distancesInorder[5]);
    }
    
}
// Same as above but for AreaUnit
public class AreaExtensionsTests
{
    
}

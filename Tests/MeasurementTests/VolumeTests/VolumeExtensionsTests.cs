namespace MeasurementTests.VolumeTests;

public class VolumeExtensionsTests
{
    [Fact]
    public void CanOrderWhenAllTheSameVolumeUnit()
    {
        var volume1 = new Volume(VolumeUnit.Liters, 1);
        var volume2 = new Volume(VolumeUnit.Liters, 4);
        var volume3 = new Volume(VolumeUnit.Liters, 3);
        var volume4 = new Volume(VolumeUnit.Liters, 2);
        var volume5 = new Volume(VolumeUnit.Liters, 5);

        var volumes = new List<Volume> {volume5, volume4, volume3, volume2, volume1};
        var volumesInOrder =  volumes.GetInOrder().ToList();
        Assert.Equal(volume1, volumesInOrder[0]);
        Assert.Equal(volume4, volumesInOrder[1]);
        Assert.Equal(volume3, volumesInOrder[2]);
        Assert.Equal(volume2, volumesInOrder[3]);
        Assert.Equal(volume5, volumesInOrder[4]);
    }
    

    [Fact]
    public void CanOrderWhenAllDifferentVolumeUnit()
    {
        var volume1 = new Volume(VolumeUnit.Liters, 1);
        var volume2 = new Volume(VolumeUnit.Milliliters, 4);
        var volume3 = new Volume(VolumeUnit.CubicCentimeters, 3);
        var volume4 = new Volume(VolumeUnit.CubicMeters, 2);
        var volume5 = new Volume(VolumeUnit.Gallons, 5);
        var volume6 = new Volume(VolumeUnit.Quarts, 2);

        var volumes = new List<Volume> {volume5, volume4, volume3, volume2, volume1, volume6};
        var volumesInOrder = volumes.GetInOrder().ToList(); // Assuming GetInOrder is for ascending order
        Assert.Equal(volume3, volumesInOrder[0]);
        Assert.Equal(volume2, volumesInOrder[1]);
        Assert.Equal(volume1, volumesInOrder[2]);
        Assert.Equal(volume6, volumesInOrder[3]);
        Assert.Equal(volume5, volumesInOrder[4]);
        Assert.Equal(volume4, volumesInOrder[5]);
    }

    
    [Fact]
    public void CanOrderDescendingWhenAllTheSameVolumeUnit()
    {
        var volume1 = new Volume(VolumeUnit.Liters, 1);
        var volume2 = new Volume(VolumeUnit.Liters, 4);
        var volume3 = new Volume(VolumeUnit.Liters, 3);
        var volume4 = new Volume(VolumeUnit.Liters, 2);
        var volume5 = new Volume(VolumeUnit.Liters, 5);

        var volumes = new List<Volume> {volume5, volume4, volume3, volume2, volume1};
        var volumesInOrder =  volumes.GetInOrderDescending().ToList();
        Assert.Equal(volume5, volumesInOrder[0]);
        Assert.Equal(volume2, volumesInOrder[1]);
        Assert.Equal(volume3, volumesInOrder[2]);
        Assert.Equal(volume4, volumesInOrder[3]);
        Assert.Equal(volume1, volumesInOrder[4]);
    }
    
    [Fact]
    public void CanOrderDescendingWhenAllDifferentVolumeUnit()
    {
        var volume1 = new Volume(VolumeUnit.Liters, 1);
        var volume2 = new Volume(VolumeUnit.Milliliters, 4);
        var volume3 = new Volume(VolumeUnit.CubicCentimeters, 3);
        var volume4 = new Volume(VolumeUnit.CubicMeters, 2);
        var volume5 = new Volume(VolumeUnit.Gallons, 5);
        var volume6 = new Volume(VolumeUnit.Quarts, 2);

        var volumes = new List<Volume> {volume5, volume4, volume3, volume2, volume1, volume6};
        var volumesInOrder =  volumes.GetInOrderDescending().ToList();
        Assert.Equal(volume4, volumesInOrder[0]);
        Assert.Equal(volume5, volumesInOrder[1]);
        Assert.Equal(volume6, volumesInOrder[2]);
        Assert.Equal(volume1, volumesInOrder[3]);
        Assert.Equal(volume2, volumesInOrder[4]);
        Assert.Equal(volume3, volumesInOrder[5]);
    }
}
namespace MeasurementTests.AreaTests;

public class AreaExtensionsTests
{
    [Fact]
    public void CanOrderWhenAllTheSameAreaUnit()
    {
        var area1 = new Area(AreaUnit.SquareMeters, 1);
        var area2 = new Area(AreaUnit.SquareMeters, 4);
        var area3 = new Area(AreaUnit.SquareMeters, 3);
        var area4 = new Area(AreaUnit.SquareMeters, 2);
        var area5 = new Area(AreaUnit.SquareMeters, 5);

        var areas = new List<Area> {area5, area4, area3, area2, area1};
        var areasInOrder =  areas.GetInOrder().ToList();
        Assert.Equal(area1, areasInOrder[0]);
        Assert.Equal(area4, areasInOrder[1]);
        Assert.Equal(area3, areasInOrder[2]);
        Assert.Equal(area2, areasInOrder[3]);
        Assert.Equal(area5, areasInOrder[4]);
    }
    

    [Fact]
    public void CanOrderWhenAllDifferentAreaUnit()
    {
        var area1 = new Area(AreaUnit.SquareMeters, 1);
        var area2 = new Area(AreaUnit.SquareMillimeters, 4);
        var area3 = new Area(AreaUnit.SquareCentimeters, 3);
        var area4 = new Area(AreaUnit.SquareKilometers, 2);
        var area5 = new Area(AreaUnit.SquareInches, 5);
        var area6 = new Area(AreaUnit.SquareMiles, 2);

        var areas = new List<Area> {area5, area4, area3, area2, area1, area6};
        var areasInOrder =  areas.GetInOrder().ToList();
        Assert.Equal(area2, areasInOrder[0]);
        Assert.Equal(area3, areasInOrder[1]);
        Assert.Equal(area5, areasInOrder[2]);
        Assert.Equal(area1, areasInOrder[3]);
        Assert.Equal(area4, areasInOrder[4]);
        Assert.Equal(area6, areasInOrder[5]);
    }
    
    [Fact]
    public void CanOrderDescendingWhenAllTheSameAreaUnit()
    {
        var area1 = new Area(AreaUnit.SquareMeters, 1);
        var area2 = new Area(AreaUnit.SquareMeters, 4);
        var area3 = new Area(AreaUnit.SquareMeters, 3);
        var area4 = new Area(AreaUnit.SquareMeters, 2);
        var area5 = new Area(AreaUnit.SquareMeters, 5);

        var areas = new List<Area> {area5, area4, area3, area2, area1};
        var areasInOrder =  areas.GetInOrderDescending().ToList();
        Assert.Equal(area5, areasInOrder[0]);
        Assert.Equal(area2, areasInOrder[1]);
        Assert.Equal(area3, areasInOrder[2]);
        Assert.Equal(area4, areasInOrder[3]);
        Assert.Equal(area1, areasInOrder[4]);
    }
    
    [Fact]
    public void CanOrderDescendingWhenAllDifferentAreaUnit()
    {
        var area1 = new Area(AreaUnit.SquareMeters, 1);
        var area2 = new Area(AreaUnit.SquareMillimeters, 4);
        var area3 = new Area(AreaUnit.SquareCentimeters, 3);
        var area4 = new Area(AreaUnit.SquareKilometers, 2);
        var area5 = new Area(AreaUnit.SquareInches, 5);
        var area6 = new Area(AreaUnit.SquareMiles, 2);

        var areas = new List<Area> {area5, area4, area3, area2, area1, area6};
        var areasInOrder =  areas.GetInOrderDescending().ToList();
        Assert.Equal(area6, areasInOrder[0]);
        Assert.Equal(area4, areasInOrder[1]);
        Assert.Equal(area1, areasInOrder[2]);
        Assert.Equal(area5, areasInOrder[3]);
        Assert.Equal(area3, areasInOrder[4]);
        Assert.Equal(area2, areasInOrder[5]);
    }
}
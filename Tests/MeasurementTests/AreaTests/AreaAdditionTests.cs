using Measurements;

namespace MeasurementTests.AreaTests;

public class AreaAdditionTests
{
    
    [Fact]
    public void AddArea_SameUnit()
    {
        var area1 = new Area(AreaUnit.SquareMeters, 5);
        var area2 = new Area(AreaUnit.SquareMeters, 10);
        area1.AddArea(area2);
        Assert.Equal(15, area1.Amount);
    }
    
    [Fact]
    public void AddArea_DifferentUnits()
    {
        var area1 = new Area(AreaUnit.SquareFeet, 1);
        var area2 = new Area(AreaUnit.SquareInches, 12);
        area1.AddArea(area2);
        Assert.Equal(1, area1.Amount);
        Assert.Contains(area1.OtherAreas, d => d.AreaType == AreaUnit.SquareInches && d.Amount == 12);
    }
    
    [Fact]
    public void AddArea_MultipleTimes()
    {
        var area1 = new Area(AreaUnit.SquareFeet, 1);
        var area2 = new Area(AreaUnit.SquareInches, 12);
        var area3 = new Area(AreaUnit.SquareYards, 3);
    
        area1.AddArea(area2).AddArea(area3);

        Assert.Equal(1, area1.Amount);
        Assert.Contains(area1.OtherAreas, d => d.AreaType == AreaUnit.SquareInches && d.Amount == 12);
        Assert.Contains(area1.OtherAreas, d => d.AreaType == AreaUnit.SquareYards && d.Amount == 3);
    }

    [Fact]
    public void GetArea_GetAsReturnsAllCombined()
    {
        var area1 = new Area(AreaUnit.SquareFeet, 1);
        var area2 = new Area(AreaUnit.SquareInches, 12);
        var area3 = new Area(AreaUnit.SquareYards, 3);

        area1.AddArea(area2).AddArea(area3);

        var inSquareMeters = area1.GetAs(AreaUnit.SquareMeters);
    
        Assert.Equal(2.609027m, inSquareMeters);
    }
    
    [Fact]
    public void AddArea_WithOtherAreas()
    {
        var area1 = new Area(AreaUnit.SquareFeet, 1);
        var subArea = new Area(AreaUnit.SquareInches, 10);
        var area2 = new Area(AreaUnit.SquareYards, 3);
        area2.AddArea(subArea); // Add subArea to area2's OtherAreas
        area1.AddArea(area2);
        Assert.Equal(1, area1.Amount);
        Assert.Contains(area1.OtherAreas, d => d.AreaType == AreaUnit.SquareYards && d.Amount == 3);
        Assert.Contains(area1.OtherAreas, d => d.AreaType == AreaUnit.SquareInches && d.Amount == 10);
    }
}
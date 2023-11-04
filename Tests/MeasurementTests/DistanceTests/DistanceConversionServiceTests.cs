using Measurements;

namespace MeasurementTests.DistanceTests;

public class DistanceConversionServiceTests
{

    [Theory]
    [InlineData(1, DistanceUnit.Meters, 1000)]
    [InlineData(1, DistanceUnit.Centimeters, 10)]
    [InlineData(1, DistanceUnit.Kilometers, 1000000)]
    [InlineData(1, DistanceUnit.Inches, 25.4)]
    [InlineData(1, DistanceUnit.Feet, 304.8)]
    [InlineData(1, DistanceUnit.Yards, 914.4)]
    [InlineData(1, DistanceUnit.Miles, 1609344)]
    public void ConvertTo_Millimeters(decimal value, DistanceUnit fromUnit, decimal expectedResult)
    {
        var result = DistanceConversionService.ConvertDistance(value, fromUnit, DistanceUnit.Millimeters);
        Assert.Equal(expectedResult, result, 1);
    }

    [Theory]
    [InlineData(1, DistanceUnit.Meters, 100)]
    [InlineData(1, DistanceUnit.Millimeters, 0.1)]
    [InlineData(1, DistanceUnit.Kilometers, 100000)]
    [InlineData(1, DistanceUnit.Inches, 2.54)]
    [InlineData(1, DistanceUnit.Feet, 30.48)]
    [InlineData(1, DistanceUnit.Yards, 91.44)]
    [InlineData(1, DistanceUnit.Miles, 160934.4)]
    public void ConvertTo_Centimeters(decimal value, DistanceUnit fromUnit, decimal expectedResult)
    {
        var result = DistanceConversionService.ConvertDistance(value, fromUnit, DistanceUnit.Centimeters);
        Assert.Equal(expectedResult, result, 2);
    }
    

    [Theory]
    [InlineData(1, DistanceUnit.Millimeters, 0.001)]
    [InlineData(1, DistanceUnit.Centimeters, 0.01)]
    [InlineData(1, DistanceUnit.Kilometers, 1000)]
    [InlineData(1, DistanceUnit.Inches, 0.0254)]
    [InlineData(1, DistanceUnit.Feet, 0.3048)]
    [InlineData(1, DistanceUnit.Yards, 0.9144)]
    [InlineData(1, DistanceUnit.Miles, 1609.344)]
    public void ConvertTo_Meters(decimal value, DistanceUnit fromUnit, decimal expectedResult)
    {
        var result = DistanceConversionService.ConvertDistance(value, fromUnit, DistanceUnit.Meters);
        Assert.Equal(expectedResult, result, 5);
    }


 
    [Theory]
    [InlineData(1, DistanceUnit.Meters, 0.001)]
    [InlineData(1, DistanceUnit.Millimeters, 0.000001)]
    [InlineData(1, DistanceUnit.Centimeters, 0.00001)]
    [InlineData(1, DistanceUnit.Inches, 0.0000254)]
    [InlineData(1, DistanceUnit.Feet, 0.0003048)]
    [InlineData(1, DistanceUnit.Yards, 0.0009144)]
    [InlineData(1, DistanceUnit.Miles, 1.609344)]
    public void ConvertTo_Kilometers(decimal value, DistanceUnit fromUnit, decimal expectedResult)
    {
        var result = DistanceConversionService.ConvertDistance(value, fromUnit, DistanceUnit.Kilometers);
        Assert.Equal(expectedResult, result, 6);
    }

    [Theory]
    [InlineData(1, DistanceUnit.Meters, 39.3701)]
    [InlineData(1, DistanceUnit.Millimeters, 0.0393701)]
    [InlineData(1, DistanceUnit.Centimeters, 0.393701)]
    [InlineData(1, DistanceUnit.Kilometers, 39370.07874016)]
    [InlineData(1, DistanceUnit.Feet, 12)]
    [InlineData(1, DistanceUnit.Yards, 36)]
    [InlineData(1, DistanceUnit.Miles, 63360)]
    public void ConvertTo_Inches(decimal value, DistanceUnit fromUnit, decimal expectedResult)
    {
        var result = DistanceConversionService.ConvertDistance(value, fromUnit, DistanceUnit.Inches);
        Assert.Equal(expectedResult, result, 4);
    }

    [Theory]
    [InlineData(1, DistanceUnit.Meters, 3.28084)]
    [InlineData(1, DistanceUnit.Millimeters, 0.00328084)]
    [InlineData(1, DistanceUnit.Centimeters, 0.0328084)]
    [InlineData(1, DistanceUnit.Kilometers, 3280.84)]
    [InlineData(1, DistanceUnit.Inches, 0.0833333)]
    [InlineData(1, DistanceUnit.Yards, 3)]
    [InlineData(1, DistanceUnit.Miles, 5280)]
    public void ConvertTo_Feet(decimal value, DistanceUnit fromUnit, decimal expectedResult)
    {
        var result = DistanceConversionService.ConvertDistance(value, fromUnit, DistanceUnit.Feet);
        Assert.Equal(expectedResult, result, 2);
    }

    [Theory]
    [InlineData(1, DistanceUnit.Meters, 1.09361)]
    [InlineData(1, DistanceUnit.Millimeters, 0.00109361)]
    [InlineData(1, DistanceUnit.Centimeters, 0.0109361)]
    [InlineData(1, DistanceUnit.Kilometers, 1093.61)]
    [InlineData(1, DistanceUnit.Inches, 0.0277778)]
    [InlineData(1, DistanceUnit.Feet, 0.333333)]
    [InlineData(1, DistanceUnit.Miles, 1760)]
    public void ConvertTo_Yards(decimal value, DistanceUnit fromUnit, decimal expectedResult)
    {
        var result = DistanceConversionService.ConvertDistance(value, fromUnit, DistanceUnit.Yards);
        Assert.Equal(expectedResult, result, 2);
    }

    [Theory]
    [InlineData(1, DistanceUnit.Meters, 0.000621371)]
    [InlineData(1, DistanceUnit.Millimeters, 0.000000621371)]
    [InlineData(1, DistanceUnit.Centimeters, 0.00000621371)]
    [InlineData(1, DistanceUnit.Kilometers, 0.621371)]
    [InlineData(1, DistanceUnit.Inches, 0.0000157828)]
    [InlineData(1, DistanceUnit.Feet, 0.000189394)]
    [InlineData(1, DistanceUnit.Yards, 0.000568182)]
    public void ConvertTo_Miles(decimal value, DistanceUnit fromUnit, decimal expectedResult)
    {
        var result = DistanceConversionService.ConvertDistance(value, fromUnit, DistanceUnit.Miles);
        Assert.Equal(expectedResult, result, 5);
    }
}
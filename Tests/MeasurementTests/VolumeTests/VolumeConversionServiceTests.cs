using Measurements;

namespace MeasurementTests.VolumeTests;

public class VolumeConversionServiceTests
{
    [Theory]
    [InlineData(1, VolumeUnit.Milliliters, 0.001)]
    [InlineData(1, VolumeUnit.CubicMeters, 1000)]
    [InlineData(1, VolumeUnit.CubicCentimeters, 0.001)]
    [InlineData(1, VolumeUnit.CubicInches, 0.0163871)]
    [InlineData(1, VolumeUnit.CubicFeet, 28.316846592)]
    [InlineData(1, VolumeUnit.Gallons, 3.78541)]
    [InlineData(1, VolumeUnit.Quarts, 0.946353)]
    [InlineData(1, VolumeUnit.USPints, 0.473176)]
    [InlineData(1, VolumeUnit.FluidOunces, 0.0295735)]
    [InlineData(1, VolumeUnit.CubicYards, 764.554858)]
    [InlineData(1, VolumeUnit.UKPints, 0.568261)]
    [InlineData(1, VolumeUnit.Liters, 1)]
    [InlineData(2, VolumeUnit.Milliliters, 0.002)]
    [InlineData(2, VolumeUnit.CubicMeters, 2000)]
    [InlineData(2, VolumeUnit.CubicCentimeters, 0.002)]
    [InlineData(2, VolumeUnit.CubicInches, 0.0327742)]
    [InlineData(2, VolumeUnit.CubicFeet, 56.633693184)]
    [InlineData(2, VolumeUnit.Gallons, 7.57082)]
    [InlineData(2, VolumeUnit.Quarts, 1.892705892)]
    [InlineData(2, VolumeUnit.USPints, 0.946353)]
    [InlineData(2, VolumeUnit.FluidOunces, 0.0591471)]
    [InlineData(2, VolumeUnit.CubicYards, 1529.109716)]
    [InlineData(2, VolumeUnit.Liters, 2)]
    [InlineData(2, VolumeUnit.UKPints, 1.136522)]
    public void ConvertTo_Liters(decimal value, VolumeUnit fromUnit, decimal expectedResult)
    {
        var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.Liters);
        Assert.Equal(expectedResult, result, 6);
    }
    
    [Theory]
    [InlineData(1, VolumeUnit.Milliliters, 1)]
    [InlineData(1, VolumeUnit.CubicMeters, 1000000)]
    [InlineData(1, VolumeUnit.CubicCentimeters, 1)]
    [InlineData(1, VolumeUnit.CubicInches, 16.387064)]
    [InlineData(1, VolumeUnit.CubicFeet, 28316.846592)]
    [InlineData(1, VolumeUnit.Gallons, 3785.41)]
    [InlineData(1, VolumeUnit.Quarts, 946.353)]
    [InlineData(1, VolumeUnit.USPints, 473.176473)]
    [InlineData(1, VolumeUnit.FluidOunces, 29.5735295625)]
    [InlineData(1, VolumeUnit.CubicYards, 764554.858)]
    [InlineData(1, VolumeUnit.UKPints, 568.26125)]
    [InlineData(1, VolumeUnit.Liters, 1000)]
    [InlineData(2, VolumeUnit.Milliliters, 2)]
    [InlineData(2, VolumeUnit.CubicMeters, 2000000)]
    [InlineData(2, VolumeUnit.CubicCentimeters, 2)]
    [InlineData(2, VolumeUnit.CubicInches, 32.774128)]
    [InlineData(2, VolumeUnit.CubicFeet, 56633.693184)]
    [InlineData(2, VolumeUnit.Gallons, 7570.82)]
    [InlineData(2, VolumeUnit.Quarts, 1892.706)]
    [InlineData(2, VolumeUnit.USPints, 946.352946)]
    [InlineData(2, VolumeUnit.FluidOunces, 59.147059)]
    [InlineData(2, VolumeUnit.CubicYards, 1529109.716)]
    [InlineData(2, VolumeUnit.UKPints, 1136.5225)]
    [InlineData(2, VolumeUnit.Liters, 2000)]
    public void ConvertTo_Milliliters(decimal value, VolumeUnit fromUnit, decimal expectedResult)
    {
        var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.Milliliters);
        Assert.Equal(expectedResult, result, 6);
    }
    
    [Theory]
    [InlineData(1, VolumeUnit.Milliliters, 0.000001)]
    [InlineData(1, VolumeUnit.CubicMeters, 1)]
    [InlineData(1, VolumeUnit.CubicCentimeters, 0.000001)]
    [InlineData(1, VolumeUnit.CubicInches, 0.000016387064)]
    [InlineData(1, VolumeUnit.CubicFeet, 0.028316846592)]
    [InlineData(1, VolumeUnit.Gallons, 0.00378541)]
    [InlineData(1, VolumeUnit.Quarts, 0.000946353)]
    [InlineData(1, VolumeUnit.USPints, 0.000473176473)]
    [InlineData(1, VolumeUnit.FluidOunces, 0.0000295735)]
    [InlineData(1, VolumeUnit.CubicYards, 0.764554857984)]
    [InlineData(1, VolumeUnit.UKPints, 0.00056826125)]
    [InlineData(1, VolumeUnit.Liters, 0.001)]
    [InlineData(2, VolumeUnit.Milliliters, 0.000002)]
    [InlineData(2, VolumeUnit.CubicMeters, 2)]
    [InlineData(2, VolumeUnit.CubicCentimeters, 0.000002)]
    [InlineData(2, VolumeUnit.CubicInches, 0.000032774128)]
    [InlineData(2, VolumeUnit.CubicFeet, 0.056633693184)]
    [InlineData(2, VolumeUnit.Gallons, 0.00757082)]
    [InlineData(2, VolumeUnit.Quarts, 0.001892706)]
    [InlineData(2, VolumeUnit.USPints, 0.000946352946)]
    [InlineData(2, VolumeUnit.FluidOunces, 0.000059147)]
    [InlineData(2, VolumeUnit.CubicYards, 1.529109716)]
    [InlineData(2, VolumeUnit.UKPints, 0.0011365225)]
    [InlineData(2, VolumeUnit.Liters, 0.002)]
    public void ConvertTo_CubicMeters(decimal value, VolumeUnit fromUnit, decimal expectedResult)
    {
        var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.CubicMeters);
        Assert.Equal(expectedResult, result, 9);
    }
    
    [Theory]
    [InlineData(1, VolumeUnit.Milliliters, 1)]
    [InlineData(1, VolumeUnit.CubicMeters, 1000000)]
    [InlineData(1, VolumeUnit.CubicCentimeters, 1)]
    [InlineData(1, VolumeUnit.CubicInches, 16.387064)]
    [InlineData(1, VolumeUnit.CubicFeet, 28316.846592)]
    [InlineData(1, VolumeUnit.Gallons, 3785.41)]
    [InlineData(1, VolumeUnit.Quarts, 946.353)]
    [InlineData(1, VolumeUnit.USPints, 473.176473)]
    [InlineData(1, VolumeUnit.FluidOunces, 29.57353)]
    [InlineData(1, VolumeUnit.CubicYards, 764554.858)]
    [InlineData(1, VolumeUnit.UKPints, 568.26125)]
    [InlineData(1, VolumeUnit.Liters, 1000)]
    [InlineData(2, VolumeUnit.Milliliters, 2)]
    [InlineData(2, VolumeUnit.CubicMeters, 2000000)]
    [InlineData(2, VolumeUnit.CubicCentimeters, 2)]
    [InlineData(2, VolumeUnit.CubicInches, 32.774128)]
    [InlineData(2, VolumeUnit.CubicFeet, 56633.693184)]
    [InlineData(2, VolumeUnit.Gallons, 7570.82)]
    [InlineData(2, VolumeUnit.Quarts, 1892.706)]
    [InlineData(2, VolumeUnit.USPints, 946.352946)]
    [InlineData(2, VolumeUnit.FluidOunces, 59.147059)]
    [InlineData(2, VolumeUnit.CubicYards, 1529109.716)]
    [InlineData(2, VolumeUnit.UKPints, 1136.5225)]
    [InlineData(2, VolumeUnit.Liters, 2000)]
    public void ConvertTo_CubicCentimeters(decimal value, VolumeUnit fromUnit, decimal expectedResult)
    {
        var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.CubicCentimeters);
        Assert.Equal(expectedResult, result, 6);
    }
    
    [Theory]
    [InlineData(1, VolumeUnit.Milliliters, 0.0610237441)]
    [InlineData(1, VolumeUnit.CubicMeters, 61023.7440947)]
    [InlineData(1, VolumeUnit.CubicCentimeters, 0.0610237441)]
    [InlineData(1, VolumeUnit.CubicInches, 1)]
    [InlineData(1, VolumeUnit.CubicFeet, 1728)]
    [InlineData(1, VolumeUnit.Gallons, 231)]
    [InlineData(1, VolumeUnit.Quarts, 57.75)]
    [InlineData(1, VolumeUnit.USPints, 28.875)]
    [InlineData(1, VolumeUnit.FluidOunces, 1.8046875)]
    [InlineData(1, VolumeUnit.CubicYards, 46656)]
    [InlineData(1, VolumeUnit.UKPints, 34.6774290989527)]
    [InlineData(1, VolumeUnit.Liters, 61.0237441)]
    [InlineData(2, VolumeUnit.Milliliters, 0.1220474882)]
    [InlineData(2, VolumeUnit.CubicMeters, 122047.4881895)]
    [InlineData(2, VolumeUnit.CubicCentimeters, 0.1220474882)]
    [InlineData(2, VolumeUnit.CubicInches, 2)]
    [InlineData(2, VolumeUnit.CubicFeet, 3456)]
    [InlineData(2, VolumeUnit.Gallons, 462)]
    [InlineData(2, VolumeUnit.Quarts, 115.5)]
    [InlineData(2, VolumeUnit.USPints, 57.75)]
    [InlineData(2, VolumeUnit.FluidOunces, 3.609375)]
    [InlineData(2, VolumeUnit.CubicYards, 93312)]
    [InlineData(2, VolumeUnit.UKPints, 69.3548582)]
    [InlineData(2, VolumeUnit.Liters, 122.0474882)]
    public void ConvertTo_CubicInches(decimal value, VolumeUnit fromUnit, decimal expectedResult)
    {
        var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.CubicInches);
        Assert.Equal(expectedResult, result, 7);
    }
    
    [Theory]
    [InlineData(1, VolumeUnit.Milliliters, 0.0000353147)]
    [InlineData(1, VolumeUnit.CubicMeters, 35.31466672148859)]
    [InlineData(1, VolumeUnit.CubicCentimeters, 0.0000353147)]
    [InlineData(1, VolumeUnit.CubicInches, 0.000578704)]
    [InlineData(1, VolumeUnit.CubicFeet, 1)]
    [InlineData(1, VolumeUnit.Gallons, 0.133681)]
    [InlineData(1, VolumeUnit.Quarts, 0.0334201)]
    [InlineData(1, VolumeUnit.USPints, 0.0167101)]
    [InlineData(1, VolumeUnit.FluidOunces, 0.00104438)]
    [InlineData(1, VolumeUnit.CubicYards, 27)]
    [InlineData(1, VolumeUnit.UKPints, 0.020068)]
    [InlineData(1, VolumeUnit.Liters, 0.0353147)]
    [InlineData(2, VolumeUnit.Milliliters, 0.0000706294)]
    [InlineData(2, VolumeUnit.CubicMeters, 70.629333442977)]
    [InlineData(2, VolumeUnit.CubicCentimeters, 0.0000706294)]
    [InlineData(2, VolumeUnit.CubicInches, 0.001157408)]
    [InlineData(2, VolumeUnit.CubicFeet, 2)]
    [InlineData(2, VolumeUnit.Gallons, 0.267362)]
    [InlineData(2, VolumeUnit.Quarts, 0.0668402)]
    [InlineData(2, VolumeUnit.USPints, 0.0334201)]
    [InlineData(2, VolumeUnit.FluidOunces, 0.00208876)]
    [InlineData(2, VolumeUnit.CubicYards, 54)]
    [InlineData(2, VolumeUnit.UKPints, 0.0401359)]
    [InlineData(2, VolumeUnit.Liters, 0.0706293)]
    public void ConvertTo_CubicFeet(decimal value, VolumeUnit fromUnit, decimal expectedResult)
    {
        var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.CubicFeet);
        Assert.Equal(expectedResult, result, 7);
    }
    
    [Theory]
[InlineData(1, VolumeUnit.Milliliters, 0.000264172)]
[InlineData(1, VolumeUnit.CubicMeters, 264.1720523581)]
[InlineData(1, VolumeUnit.CubicCentimeters, 0.000264172)]
[InlineData(1, VolumeUnit.CubicInches, 0.004329)]
[InlineData(1, VolumeUnit.CubicFeet, 7.480519)]
[InlineData(1, VolumeUnit.Gallons, 1)]
[InlineData(1, VolumeUnit.Quarts, 0.25)]
[InlineData(1, VolumeUnit.USPints, 0.125)]
[InlineData(1, VolumeUnit.FluidOunces, 0.0078125)]
[InlineData(1, VolumeUnit.CubicYards, 201.974026)]
[InlineData(1, VolumeUnit.UKPints, 0.150119)]
[InlineData(1, VolumeUnit.Liters, 0.264172)]
// Include more InlineData attributes for different input values if necessary.
public void ConvertTo_Gallons(decimal value, VolumeUnit fromUnit, decimal expectedResult)
{
    var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.Gallons);
    Assert.Equal(expectedResult, result, 6);
}

[Theory]
[InlineData(1, VolumeUnit.Milliliters, 0.001056688)]
[InlineData(1, VolumeUnit.CubicMeters, 1056.688209)]
[InlineData(1, VolumeUnit.CubicCentimeters, 0.001056688)]
[InlineData(1, VolumeUnit.CubicInches, 0.017316)]
[InlineData(1, VolumeUnit.CubicFeet, 29.922077922077920)]
[InlineData(1, VolumeUnit.Gallons, 4)]
[InlineData(1, VolumeUnit.Quarts, 1)]
[InlineData(1, VolumeUnit.USPints, 0.5)]
[InlineData(1, VolumeUnit.FluidOunces, 0.03125)]
[InlineData(1, VolumeUnit.CubicYards, 807.896104)]
[InlineData(1, VolumeUnit.UKPints, 0.600475)]
[InlineData(1, VolumeUnit.Liters, 1.056688)]
// Include more InlineData attributes for different input values if necessary.
public void ConvertTo_Quarts(decimal value, VolumeUnit fromUnit, decimal expectedResult)
{
    var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.Quarts);
    Assert.Equal(expectedResult, result, 6);
}

[Theory]
[InlineData(1, VolumeUnit.Milliliters, 0.002113376)]
[InlineData(1, VolumeUnit.CubicMeters, 2113.376419)]
[InlineData(1, VolumeUnit.CubicCentimeters, 0.002113376)]
[InlineData(1, VolumeUnit.CubicInches, 0.034632)]
[InlineData(1, VolumeUnit.CubicFeet, 59.844156)]
[InlineData(1, VolumeUnit.Gallons, 8)]
[InlineData(1, VolumeUnit.Quarts, 2)]
[InlineData(1, VolumeUnit.USPints, 1)]
[InlineData(1, VolumeUnit.FluidOunces, 0.0625)]
[InlineData(1, VolumeUnit.CubicYards, 1615.792233)]
[InlineData(1, VolumeUnit.UKPints, 1.20095)]
[InlineData(1, VolumeUnit.Liters, 2.113376)]
// Include more InlineData attributes for different input values if necessary.
public void ConvertTo_USPints(decimal value, VolumeUnit fromUnit, decimal expectedResult)
{
    var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.USPints);
    Assert.Equal(expectedResult, result, 6);
}

[Theory]
[InlineData(1, VolumeUnit.Milliliters, 0.033814)]
[InlineData(1, VolumeUnit.CubicMeters, 33814)]
[InlineData(1, VolumeUnit.CubicCentimeters, 0.033814)]
[InlineData(1, VolumeUnit.CubicInches, 0.554113)]
[InlineData(1, VolumeUnit.CubicFeet, 957.5064935064934)]
[InlineData(1, VolumeUnit.Gallons, 128)]
[InlineData(1, VolumeUnit.Quarts, 32)]
[InlineData(1, VolumeUnit.USPints, 16)]
[InlineData(1, VolumeUnit.FluidOunces, 1)]
[InlineData(1, VolumeUnit.CubicYards, 25852.7)]
[InlineData(1, VolumeUnit.UKPints, 19.215198)]
[InlineData(1, VolumeUnit.Liters, 33.814023)]
// Include more InlineData attributes for different input values if necessary.
public void ConvertTo_FluidOunces(decimal value, VolumeUnit fromUnit, decimal expectedResult)
{
    var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.FluidOunces);
    Assert.Equal(expectedResult, result, 6);
}

[Theory]
[InlineData(1, VolumeUnit.Milliliters, 0.00000130795)]
[InlineData(1, VolumeUnit.CubicMeters,  1.30795061931439225 )]
[InlineData(1, VolumeUnit.CubicCentimeters, 0.00000130795)]
[InlineData(1, VolumeUnit.CubicInches, 0.0000214335)]
[InlineData(1, VolumeUnit.CubicFeet, 0.03703704)]
[InlineData(1, VolumeUnit.Gallons, 0.00495113)]
[InlineData(1, VolumeUnit.Quarts, 0.00123778)]
[InlineData(1, VolumeUnit.USPints, 0.000618891)]
[InlineData(1, VolumeUnit.FluidOunces, 0.000038684719536)]
[InlineData(1, VolumeUnit.CubicYards, 1)]
[InlineData(1, VolumeUnit.UKPints, 0.00074349)]
[InlineData(1, VolumeUnit.Liters, 0.00130795)]
// Include more InlineData attributes for different input values if necessary.
public void ConvertTo_CubicYards(decimal value, VolumeUnit fromUnit, decimal expectedResult)
{
    var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.CubicYards);
    Assert.Equal(expectedResult, result, 8);
}

[Theory]
[InlineData(1, VolumeUnit.Milliliters, 0.00175975)]
[InlineData(1, VolumeUnit.CubicMeters, 1759.753986)]
[InlineData(1, VolumeUnit.CubicCentimeters, 0.00175975)]
[InlineData(1, VolumeUnit.CubicInches, 0.02883720119927234)]
[InlineData(1, VolumeUnit.CubicFeet, 49.8307)]
[InlineData(1, VolumeUnit.Gallons, 6.66139)]
[InlineData(1, VolumeUnit.Quarts, 1.66535)]
[InlineData(1, VolumeUnit.USPints, 0.832674)]
[InlineData(1, VolumeUnit.FluidOunces, 0.0520421)]
[InlineData(1, VolumeUnit.CubicYards, 1345.43)]
[InlineData(1, VolumeUnit.UKPints, 1)]
[InlineData(1, VolumeUnit.Liters, 1.7597539863927023616)]
// Include more InlineData attributes for different input values if necessary.
public void ConvertTo_UKPints(decimal value, VolumeUnit fromUnit, decimal expectedResult)
{
    var result = VolumeConversionService.ConvertVolume(value, fromUnit, VolumeUnit.UKPints);
    Assert.Equal(expectedResult, result, 6);
}


}
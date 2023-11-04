using Measurements;

namespace MeasurementTests.AreaTests;

public class AreaConversionServiceTests
{

    [Theory]
    [InlineData(1, AreaUnit.SquareMeters, 1_000_000)]
    [InlineData(1, AreaUnit.SquareCentimeters, 100)]  // Corrected
    [InlineData(1, AreaUnit.SquareKilometers, 1_000_000_000_000)]  // Corrected
    [InlineData(1, AreaUnit.SquareInches, 645.16)]  // Corrected
    [InlineData(1, AreaUnit.SquareFeet, 92_903.04)]  // Corrected
    [InlineData(1, AreaUnit.SquareYards, 836_127.36)]  // Corrected
    [InlineData(1, AreaUnit.Acres, 4_046_856_422.4)]  // Corrected
    [InlineData(1, AreaUnit.SquareMiles, 2_589_988_110_000)]  // Corrected
    [InlineData(2, AreaUnit.SquareMeters, 2_000_000)]
    [InlineData(2, AreaUnit.SquareCentimeters, 200)]  // Corrected
    [InlineData(2, AreaUnit.SquareKilometers, 2_000_000_000_000)]  // Corrected
    [InlineData(2, AreaUnit.SquareInches, 1_290.32)]  // Corrected
    [InlineData(2, AreaUnit.SquareFeet, 185_806.08)]  // Corrected
    [InlineData(2, AreaUnit.SquareYards, 1_672_254.72)]  // Corrected
    [InlineData(2, AreaUnit.Acres, 8_093_712_844.8)]  // Corrected
    [InlineData(2, AreaUnit.SquareMiles, 5_179_976_220_000)]  // Corrected
    public void ConvertTo_SquareMillimeters(decimal value, AreaUnit fromUnit, decimal expectedResult)
    {
        var result = AreaConversionService.ConvertArea(value, fromUnit, AreaUnit.SquareMillimeters);
        Assert.Equal(expectedResult, result, 6);
    }


    [Theory]
    [InlineData(1, AreaUnit.SquareMeters, 10_000)]
    [InlineData(1, AreaUnit.SquareMillimeters, 0.01)]
    [InlineData(1, AreaUnit.SquareKilometers, 10000000000)]
    [InlineData(1, AreaUnit.SquareInches, 6.4516)]
    [InlineData(1, AreaUnit.SquareFeet, 929.0304)]
    [InlineData(1, AreaUnit.SquareYards, 8361.2736)]
    [InlineData(1, AreaUnit.Acres, 40468564.224)]
    [InlineData(1, AreaUnit.SquareMiles, 25899881100)]
    [InlineData(2, AreaUnit.SquareMeters, 20_000)]
    [InlineData(2, AreaUnit.SquareMillimeters, 0.02)]
    [InlineData(2, AreaUnit.SquareKilometers, 20000000000)]
    [InlineData(2, AreaUnit.SquareInches, 12.9032)]
    [InlineData(2, AreaUnit.SquareFeet, 1858.0608)]
    [InlineData(2, AreaUnit.SquareYards, 16722.5472)]
    [InlineData(2, AreaUnit.Acres, 80937128.448)]
    [InlineData(2, AreaUnit.SquareMiles, 51_799_762_200)]
    public void ConvertTo_SquareCentimeters(decimal value, AreaUnit fromUnit, decimal expectedResult)
    {
        var result = AreaConversionService.ConvertArea(value, fromUnit, AreaUnit.SquareCentimeters);
        Assert.Equal(expectedResult, result, 6);
    }

    [Theory]
    [InlineData(1, AreaUnit.SquareMeters, 1)]
    [InlineData(1, AreaUnit.SquareMillimeters, 0.000001)]
    [InlineData(1, AreaUnit.SquareCentimeters, 0.0001)]
    [InlineData(1, AreaUnit.SquareKilometers, 1_000_000)]
    [InlineData(1, AreaUnit.SquareInches, 0.00064516)]
    [InlineData(1, AreaUnit.SquareFeet, 0.09290304)]
    [InlineData(1, AreaUnit.SquareYards, 0.83612736)]
    [InlineData(1, AreaUnit.Acres, 4046.85642)]  
    [InlineData(1, AreaUnit.SquareMiles, 2_589_988.11)]  
    [InlineData(2, AreaUnit.SquareMeters, 2)]
    [InlineData(2, AreaUnit.SquareMillimeters, 0.000002)]
    [InlineData(2, AreaUnit.SquareCentimeters, 0.0002)]
    [InlineData(2, AreaUnit.SquareKilometers, 2_000_000)]
    [InlineData(2, AreaUnit.SquareInches, 0.00129032)]
    [InlineData(2, AreaUnit.SquareFeet, 0.18580608)]
    [InlineData(2, AreaUnit.SquareYards, 1.67225472)]
    [InlineData(2, AreaUnit.Acres, 8093.71284)]  
    [InlineData(2, AreaUnit.SquareMiles, 5_179_976.22)] 
    public void ConvertTo_SquareMeters(decimal value, AreaUnit fromUnit, decimal expectedResult)
    {
        var result = AreaConversionService.ConvertArea(value, fromUnit, AreaUnit.SquareMeters);
        Assert.Equal(expectedResult, result, 3);
    }
    
    [Theory]
    [InlineData(1, AreaUnit.SquareMeters, 1)]
    [InlineData(1, AreaUnit.SquareMillimeters, 1_000_000)]
    [InlineData(1, AreaUnit.SquareCentimeters, 10_000)]
    [InlineData(1, AreaUnit.SquareKilometers, 0.000001)]
    [InlineData(1, AreaUnit.SquareInches, 1550)]
    [InlineData(1, AreaUnit.SquareFeet, 10.76391)]
    [InlineData(1, AreaUnit.SquareYards, 1.19599005)]
    [InlineData(1, AreaUnit.Acres, 0.000247105)]
    [InlineData(1, AreaUnit.SquareMiles, 0.00000038610215855)]
    [InlineData(2, AreaUnit.SquareMeters, 2)]
    [InlineData(2, AreaUnit.SquareMillimeters, 2_000_000)]
    [InlineData(2, AreaUnit.SquareCentimeters, 20_000)]
    [InlineData(2, AreaUnit.SquareKilometers, 0.000002)]
    [InlineData(2, AreaUnit.SquareInches, 3100)]
    [InlineData(2, AreaUnit.SquareFeet, 21.527821)]
    [InlineData(2, AreaUnit.SquareYards, 2.3919801)]
    [InlineData(2, AreaUnit.Acres, 0.000494210)]
    [InlineData(2, AreaUnit.SquareMiles, 0.0000007722043171)]
    public void ConvertFrom_SquareMeters(decimal value, AreaUnit toUnit, decimal expectedResult)
    {
        var result = AreaConversionService.ConvertArea(value, AreaUnit.SquareMeters, toUnit);
        Assert.Equal(expectedResult, result, 6);
    }


    [Theory]
    [InlineData(1, AreaUnit.SquareMeters, 0.000001)]
    [InlineData(1, AreaUnit.SquareMillimeters, 0.000000000001)]
    [InlineData(1, AreaUnit.SquareCentimeters, 0.0000000001)]
    [InlineData(1, AreaUnit.SquareKilometers, 1)]
    [InlineData(1, AreaUnit.SquareInches, 0.00000000064516)]
    [InlineData(1, AreaUnit.SquareFeet, 0.00000009290304)]
    [InlineData(1, AreaUnit.SquareYards, 0.00000083612736)]
    [InlineData(1, AreaUnit.Acres, 0.004047)]
    [InlineData(1, AreaUnit.SquareMiles, 2.589988110336)]
    [InlineData(2, AreaUnit.SquareMeters, 0.000002)]
    [InlineData(2, AreaUnit.SquareMillimeters, 0.000000000002)]
    [InlineData(2, AreaUnit.SquareCentimeters, 0.0000000002)]
    [InlineData(2, AreaUnit.SquareKilometers, 2)]
    [InlineData(2, AreaUnit.SquareInches, 0.00000000129032)]
    [InlineData(2, AreaUnit.SquareFeet, 0.00000018580608)]
    [InlineData(2, AreaUnit.SquareYards, 0.00000167225472)]
    [InlineData(2, AreaUnit.Acres, 0.008094)]
    [InlineData(2, AreaUnit.SquareMiles, 5.179976220672)]
    public void ConvertTo_SquareKilometers(decimal value, AreaUnit fromUnit, decimal expectedResult)
    {
        var result = AreaConversionService.ConvertArea(value, fromUnit, AreaUnit.SquareKilometers);
        Assert.Equal(expectedResult, result, 6);
    }



    [Theory]
    [InlineData(1, AreaUnit.SquareMeters, 1550.0031)]  // 1 Square Meter = 1550.0031 Square Inches
    [InlineData(1, AreaUnit.SquareMillimeters, 0.0015500031)]  // 1 Square Millimeter = 0.0015500031 Square Inches
    [InlineData(1, AreaUnit.SquareCentimeters, 0.15500031)]  // 1 Square Centimeter = 0.15500031 Square Inches
    [InlineData(1, AreaUnit.SquareKilometers, 1_550_003_100.0062)]  // corrected, 1 Square Kilometer = 1,550,003,100 Square Inches
    [InlineData(1, AreaUnit.SquareFeet, 144)]  // 1 Square Foot = 144 Square Inches
    [InlineData(1, AreaUnit.SquareYards, 1296)]  // 1 Square Yard = 1296 Square Inches
    [InlineData(1, AreaUnit.Acres, 6_272_640)]  // corrected, 1 Acre = 6,272,640 Square Inches
    [InlineData(1, AreaUnit.SquareMiles, 4_014_489_599.4792)]  // corrected, 1 Square Mile = 4,014,489,600 Square Inches
    [InlineData(2, AreaUnit.SquareMeters, 3100.0062)]  // 2 Square Meters = 3100.0062 Square Inches
    [InlineData(2, AreaUnit.SquareMillimeters, 0.0031000062)]  // 2 Square Millimeters = 0.0031000062 Square Inches
    [InlineData(2, AreaUnit.SquareCentimeters, 0.31000062)]  // 2 Square Centimeters = 0.31000062 Square Inches
    [InlineData(2, AreaUnit.SquareKilometers, 3_100_006_200.0124)]  // corrected, 2 Square Kilometers = 3,100,006,200 Square Inches
    [InlineData(2, AreaUnit.SquareFeet, 288)]  // 2 Square Feet = 288 Square Inches
    [InlineData(2, AreaUnit.SquareYards, 2592)]  // 2 Square Yards = 2592 Square Inches
    [InlineData(2, AreaUnit.Acres, 12_545_280)]  // corrected, 2 Acres = 12,545,280 Square Inches
    [InlineData(2, AreaUnit.SquareMiles, 8_028_979_198.9584)]  // corrected, 2 Square Miles = 8,028,979,200 Square Inches
    public void ConvertTo_SquareInches(decimal value, AreaUnit fromUnit, decimal expectedResult)
    {
        var result = AreaConversionService.ConvertArea(value, fromUnit, AreaUnit.SquareInches);
        Assert.Equal(expectedResult, result, 0);
    }


    [Theory]
    [InlineData(1, AreaUnit.SquareMeters, 10.763910417)]
    [InlineData(1, AreaUnit.SquareMillimeters, 0.000010763910417)]
    [InlineData(1, AreaUnit.SquareCentimeters, 0.0010763910417)]
    [InlineData(1, AreaUnit.SquareKilometers, 10763910.41671)] // corrected
    [InlineData(1, AreaUnit.SquareInches, 0.00694444)] // corrected
    [InlineData(1, AreaUnit.SquareYards, 9)]
    [InlineData(1, AreaUnit.Acres, 43_560)] // corrected
    [InlineData(1, AreaUnit.SquareMiles, 27_878_400)] // corrected
    [InlineData(2, AreaUnit.SquareMeters, 21.527820834)]
    [InlineData(2, AreaUnit.SquareMillimeters, 0.000021527820834)]
    [InlineData(2, AreaUnit.SquareCentimeters, 0.0021527820834)]
    [InlineData(2, AreaUnit.SquareKilometers, 21527820.83342 )] // corrected
    [InlineData(2, AreaUnit.SquareInches, 0.01388888)] // corrected
    [InlineData(2, AreaUnit.SquareYards, 18)]
    [InlineData(2, AreaUnit.Acres, 87_120)] // corrected
    [InlineData(2, AreaUnit.SquareMiles, 55_756_800)] // corrected
    public void ConvertTo_SquareFeet(decimal value, AreaUnit fromUnit, decimal expectedResult)
    {
        var result = AreaConversionService.ConvertArea(value, fromUnit, AreaUnit.SquareFeet);
        Assert.Equal(expectedResult, result, 1);
    }

    [Theory]
    [InlineData(1, AreaUnit.SquareMeters, 1.1959900463)]
    [InlineData(1, AreaUnit.SquareMillimeters, 0.0000011959900463)]
    [InlineData(1, AreaUnit.SquareCentimeters, 0.00011959900463)]
    [InlineData(1, AreaUnit.SquareKilometers, 1_195_990.0463)]
    [InlineData(1, AreaUnit.SquareInches, 0.000772)]
    [InlineData(1, AreaUnit.SquareFeet, 0.11111111111)]
    [InlineData(1, AreaUnit.Acres, 4840)]
    [InlineData(1, AreaUnit.SquareMiles,  3_097_599.999598)]
    [InlineData(2, AreaUnit.SquareMeters, 2.3919800926)]
    [InlineData(2, AreaUnit.SquareMillimeters, 0.0000023919800926)]
    [InlineData(2, AreaUnit.SquareCentimeters, 0.00023919800926)]
    [InlineData(2, AreaUnit.SquareKilometers, 2_391_980.0926)]
    [InlineData(2, AreaUnit.SquareInches, 0.001543)]
    [InlineData(2, AreaUnit.SquareFeet, 0.22222222222)]
    [InlineData(2, AreaUnit.Acres, 9680)]
    [InlineData(2, AreaUnit.SquareMiles, 6_195_199.999196)]
    public void ConvertTo_SquareYards(decimal value, AreaUnit fromUnit, decimal expectedResult)
    {
        var result = AreaConversionService.ConvertArea(value, fromUnit, AreaUnit.SquareYards);
        Assert.Equal(expectedResult, result, 5);
    }

    [Theory]
    [InlineData(1, AreaUnit.SquareMeters, 0.00024710538147)]
    [InlineData(1, AreaUnit.SquareMillimeters, 0.00000000024710538147)]
    [InlineData(1, AreaUnit.SquareCentimeters, 0.000000024710538147)]
    [InlineData(1, AreaUnit.SquareKilometers, 247.10538147)]
    [InlineData(1, AreaUnit.SquareInches, 0.000000159422517)]
    [InlineData(1, AreaUnit.SquareFeet, 0.000022956841138)]
    [InlineData(1, AreaUnit.SquareYards, 0.00020661157025)]
    [InlineData(1, AreaUnit.SquareMiles, 640)]
    [InlineData(2, AreaUnit.SquareMeters, 0.00049421076294)]
    [InlineData(2, AreaUnit.SquareMillimeters, 0.00000000049421076294)]
    [InlineData(2, AreaUnit.SquareCentimeters, 0.000000049421076294)]
    [InlineData(2, AreaUnit.SquareKilometers, 494.211)]
    [InlineData(2, AreaUnit.SquareInches, 0.000000318845034)]
    [InlineData(2, AreaUnit.SquareFeet, 0.000045913682276)]
    [InlineData(2, AreaUnit.SquareYards, 0.00041322314049)]
    [InlineData(2, AreaUnit.SquareMiles, 1280)]
    public void ConvertTo_Acres(decimal value, AreaUnit fromUnit, decimal expectedResult)
    {
        var result = AreaConversionService.ConvertArea(value, fromUnit, AreaUnit.Acres);
        Assert.Equal(expectedResult, result, 3);
    }

    [Theory]
    [InlineData(1, AreaUnit.SquareMeters, 0.00000038610215855)]
    [InlineData(1, AreaUnit.SquareMillimeters, 0.00000000000038610215855)]
    [InlineData(1, AreaUnit.SquareCentimeters, 0.000000000038610215855)]
    [InlineData(1, AreaUnit.SquareKilometers, 0.38610215855)]  // Corrected
    [InlineData(1, AreaUnit.SquareInches, 0.00000000024909773625)]
    [InlineData(1, AreaUnit.SquareFeet, 0.000000035870064329)]
    [InlineData(1, AreaUnit.SquareYards, 0.00000032283057996)]
    [InlineData(1, AreaUnit.Acres, 0.0015625)]  // Corrected
    [InlineData(2, AreaUnit.SquareMeters, 0.00000077220431711)]
    [InlineData(2, AreaUnit.SquareMillimeters, 0.00000000000077220431711)]
    [InlineData(2, AreaUnit.SquareCentimeters, 0.000000000077220431711)]
    [InlineData(2, AreaUnit.SquareKilometers, 0.77220431711)]  // Corrected
    [InlineData(2, AreaUnit.SquareInches, 0.0000000004981954725)]
    [InlineData(2, AreaUnit.SquareFeet, 0.000000071740128658)]
    [InlineData(2, AreaUnit.SquareYards, 0.00000064566115992)]
    [InlineData(2, AreaUnit.Acres, 0.003125)]  // Corrected
    public void ConvertTo_SquareMiles(decimal value, AreaUnit fromUnit, decimal expectedResult)
    {
        var result = AreaConversionService.ConvertArea(value, fromUnit, AreaUnit.SquareMiles);
        Assert.Equal(expectedResult, result, 6);
    }

}
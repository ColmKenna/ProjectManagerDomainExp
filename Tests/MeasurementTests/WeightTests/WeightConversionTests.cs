using Measurements;

namespace MeasurementTests.WeightTests;

    public class WeightConversionServiceTests
    {
        [Theory]
        [InlineData(1, WeightUnit.Kilograms, 1000000)]
        [InlineData(1, WeightUnit.Grams, 1000)]
        [InlineData(1, WeightUnit.Pounds, 453592.37)]
        [InlineData(1, WeightUnit.Ounces, 28349.5)]
        [InlineData(1, WeightUnit.Stones, 6350293.18)]
        [InlineData(1, WeightUnit.MetricTons, 1000000000)]
        [InlineData(1, WeightUnit.USTons, 907185000)]
        [InlineData(2, WeightUnit.Kilograms, 2000000)]
        [InlineData(2, WeightUnit.Grams, 2000)]
        [InlineData(2, WeightUnit.Pounds, 907184.74)]
        [InlineData(2, WeightUnit.Ounces, 56699)]
        [InlineData(2, WeightUnit.Stones, 12700586.36)]
        [InlineData(2, WeightUnit.MetricTons, 2000000000)]
        [InlineData(2, WeightUnit.USTons, 1814370000)]
        public void ConvertTo_Milligrams(decimal value, WeightUnit fromUnit, decimal expectedResult)
        {
            var result = WeightConversionService.ConvertWeight(value, fromUnit, WeightUnit.Milligrams);
            Assert.Equal(expectedResult, result, 2); // Adjust the precision as needed
        }
        
        [Theory]
        [InlineData(1, WeightUnit.Kilograms, 1000)]
        [InlineData(1, WeightUnit.Milligrams, 0.001)]
        [InlineData(1, WeightUnit.Pounds, 453.59237)]
        [InlineData(1, WeightUnit.Ounces, 28.3495)]
        [InlineData(1, WeightUnit.Stones, 6350.29318)]
        [InlineData(1, WeightUnit.MetricTons, 1000000)]
        [InlineData(1, WeightUnit.USTons, 907185)]
        [InlineData(2, WeightUnit.Kilograms, 2000)]
        [InlineData(2, WeightUnit.Milligrams, 0.002)]
        [InlineData(2, WeightUnit.Grams, 2)]
        [InlineData(2, WeightUnit.Pounds, 907.18474)]
        [InlineData(2, WeightUnit.Ounces, 56.699)]
        [InlineData(2, WeightUnit.Stones, 12700.58636)]
        [InlineData(2, WeightUnit.MetricTons, 2000000)]
        [InlineData(2, WeightUnit.USTons, 1814370)]
        public void ConvertTo_Grams(decimal value, WeightUnit fromUnit, decimal expectedResult)
        {
            var result = WeightConversionService.ConvertWeight(value, fromUnit, WeightUnit.Grams);
            Assert.Equal(expectedResult, result, 5);
        }
        
        [Theory]
        [InlineData(1, WeightUnit.Kilograms, 1)]
        [InlineData(1, WeightUnit.Milligrams, 0.000001)]
        [InlineData(1, WeightUnit.Grams, 0.001)]
        [InlineData(1, WeightUnit.Pounds, 0.45359)]
        [InlineData(1, WeightUnit.Ounces, 0.02835)]
        [InlineData(1, WeightUnit.Stones, 6.35029)]
        [InlineData(1, WeightUnit.MetricTons, 1000)]
        [InlineData(1, WeightUnit.USTons, 907.185)]
        [InlineData(2, WeightUnit.Kilograms, 2)]
        [InlineData(2, WeightUnit.Milligrams, 0.000002)]
        [InlineData(2, WeightUnit.Grams, 0.002)]
        [InlineData(2, WeightUnit.Pounds, 0.90718)]
        [InlineData(2, WeightUnit.Ounces, 0.0567)]
        [InlineData(2, WeightUnit.Stones, 12.70059)]
        [InlineData(2, WeightUnit.MetricTons, 2000)]
        [InlineData(2, WeightUnit.USTons, 1814.37)]
        public void ConvertTo_Kilograms(decimal value, WeightUnit fromUnit, decimal expectedResult)
        {
            var result = WeightConversionService.ConvertWeight(value, fromUnit, WeightUnit.Kilograms);
            Assert.Equal(expectedResult, result, 5);
        }
        
        [Theory]
        [InlineData(1, WeightUnit.Kilograms, 2.20462)]
        [InlineData(1, WeightUnit.Grams, 0.00220462)]
        [InlineData(1, WeightUnit.Milligrams, 0.00000220462)]
        [InlineData(1, WeightUnit.Ounces, 0.0625)]
        [InlineData(1, WeightUnit.Stones, 14)]
        [InlineData(1, WeightUnit.MetricTons, 2204.6226218487756)]
        [InlineData(1, WeightUnit.USTons, 2000)]
        [InlineData(2, WeightUnit.Kilograms, 4.409245243)]
        [InlineData(2, WeightUnit.Grams, 0.004409245243)]
        [InlineData(2, WeightUnit.Milligrams, 0.000004409245243)]
        [InlineData(2, WeightUnit.Ounces, 0.125)]
        [InlineData(2, WeightUnit.Stones, 28)]
        [InlineData(2, WeightUnit.MetricTons, 4409.245243)]
        [InlineData(2, WeightUnit.USTons, 4000)]
        public void ConvertTo_Pounds(decimal value, WeightUnit fromUnit, decimal expectedResult)
        {
            var result = WeightConversionService.ConvertWeight(value, fromUnit, WeightUnit.Pounds);
            Assert.Equal(expectedResult, result, 5);
        }
        
        [Theory]
        [InlineData(1, WeightUnit.Kilograms, 35.274)]
        [InlineData(1, WeightUnit.Grams, 0.035274)]
        [InlineData(1, WeightUnit.Milligrams, 0.000035274)]
        [InlineData(1, WeightUnit.Pounds, 16)]
        [InlineData(1, WeightUnit.Stones, 224)]
        [InlineData(1, WeightUnit.MetricTons, 35274)]
        [InlineData(1, WeightUnit.USTons, 32000)]
        [InlineData(2, WeightUnit.Kilograms, 70.548)]
        [InlineData(2, WeightUnit.Grams, 0.070548)]
        [InlineData(2, WeightUnit.Milligrams, 0.000070548)]
        [InlineData(2, WeightUnit.Pounds, 32)]
        [InlineData(2, WeightUnit.Stones, 448)]
        [InlineData(2, WeightUnit.MetricTons, 70548)]
        [InlineData(2, WeightUnit.USTons, 64000)]
        public void ConvertTo_Ounces(decimal value, WeightUnit fromUnit, decimal expectedResult)
        {
            var result = WeightConversionService.ConvertWeight(value, fromUnit, WeightUnit.Ounces);
            Assert.Equal(expectedResult, result, 5);
        }
        
        [Theory]
        [InlineData(1, WeightUnit.Kilograms, 0.001)]
        [InlineData(1, WeightUnit.Grams, 0.000001)]
        [InlineData(1, WeightUnit.Milligrams, 0.000000001)]
        [InlineData(1, WeightUnit.Ounces, 0.0000283495)]
        [InlineData(1, WeightUnit.Pounds, 0.00045359237)]
        [InlineData(1, WeightUnit.Stones, 0.00635029318)]
        [InlineData(1, WeightUnit.USTons, 0.907185)]
        [InlineData(2, WeightUnit.Kilograms, 0.002)]
        [InlineData(2, WeightUnit.Grams, 0.000002)]
        [InlineData(2, WeightUnit.Milligrams, 0.000000002)]
        [InlineData(2, WeightUnit.Ounces, 0.000056699)]
        [InlineData(2, WeightUnit.Pounds, 0.00090718474)]
        [InlineData(2, WeightUnit.Stones, 0.01270058636)]
        [InlineData(2, WeightUnit.USTons, 1.81437)]
        public void ConvertTo_MetricTons(decimal value, WeightUnit fromUnit, decimal expectedResult)
        {
            var result = WeightConversionService.ConvertWeight(value, fromUnit, WeightUnit.MetricTons);
            Assert.Equal(expectedResult, result, 5);
        }
        
        [Theory]
        [InlineData(1, WeightUnit.Kilograms, 0.157473044)]
        [InlineData(1, WeightUnit.Grams, 0.000157473044)]
        [InlineData(1, WeightUnit.Milligrams, 0.000000157473044)]
        [InlineData(1, WeightUnit.Ounces, 0.004464285714)]
        [InlineData(1, WeightUnit.Pounds, 0.07142857143)]
        [InlineData(1, WeightUnit.MetricTons, 157.473044)]
        [InlineData(1, WeightUnit.USTons, 142.8571429)]
        [InlineData(2, WeightUnit.Kilograms, 0.314946088)]
        [InlineData(2, WeightUnit.Grams, 0.000314946088)]
        [InlineData(2, WeightUnit.Milligrams, 0.000000314946088)]
        [InlineData(2, WeightUnit.Ounces, 0.008928571428)]
        [InlineData(2, WeightUnit.Pounds, 0.14285714286)]
        [InlineData(2, WeightUnit.MetricTons, 314.946088)]
        [InlineData(2, WeightUnit.USTons, 285.7142857)]
        public void ConvertTo_Stones(decimal value, WeightUnit fromUnit, decimal expectedResult)
        {
            var result = WeightConversionService.ConvertWeight(value, fromUnit, WeightUnit.Stones);
            Assert.Equal(expectedResult, result, 5);
        }
        
        [Theory]
        [InlineData(1, WeightUnit.Kilograms, 0.00110231)]
        [InlineData(1, WeightUnit.Grams, 0.00000110231)]
        [InlineData(1, WeightUnit.Milligrams, 0.00000000110231)]
        [InlineData(1, WeightUnit.Ounces, 0.00003125)]
        [InlineData(1, WeightUnit.Pounds, 0.0005)]
        [InlineData(1, WeightUnit.Stones, 0.007)]
        [InlineData(1, WeightUnit.MetricTons, 1.10231)]
        [InlineData(2, WeightUnit.Kilograms, 0.00220462)]
        [InlineData(2, WeightUnit.Grams, 0.00000220462)]
        [InlineData(2, WeightUnit.Milligrams, 0.00000000220462)]
        [InlineData(2, WeightUnit.Ounces, 0.0000625)]
        [InlineData(2, WeightUnit.Pounds, 0.001)]
        [InlineData(2, WeightUnit.Stones, 0.014)]
        [InlineData(2, WeightUnit.MetricTons, 2.20462)]
        public void ConvertTo_Tons(decimal value, WeightUnit fromUnit, decimal expectedResult)
        {
            var result = WeightConversionService.ConvertWeight(value, fromUnit, WeightUnit.USTons);
            Assert.Equal(expectedResult, result, 5);
        }
    }
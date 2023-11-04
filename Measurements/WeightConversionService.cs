namespace Measurements;

public class WeightConversionService
{
    public static Dictionary<(WeightUnit, WeightUnit), Func<decimal, decimal>> conversionFunctions =
        new Dictionary<(WeightUnit, WeightUnit), Func<decimal, decimal>>
        {
 // Conversions from Kilograms
{ (WeightUnit.Kilograms, WeightUnit.Grams), kg => kg * 1000m },
{ (WeightUnit.Kilograms, WeightUnit.Milligrams), kg => kg * 1000000m },
{ (WeightUnit.Kilograms, WeightUnit.Pounds), kg => kg * 2.2046226218487758848m },
{ (WeightUnit.Kilograms, WeightUnit.Ounces), kg => kg * 35.274m },
{ (WeightUnit.Kilograms, WeightUnit.Stones), kg => kg * 0.157473m },
{ (WeightUnit.Kilograms, WeightUnit.MetricTons), kg => kg / 1000m },
{ (WeightUnit.Kilograms, WeightUnit.USTons), kg => kg / 907.185m },

// Conversions from Grams
{ (WeightUnit.Grams, WeightUnit.Kilograms), g => g / 1000m },
{ (WeightUnit.Grams, WeightUnit.Milligrams), g => g * 1000m },
{ (WeightUnit.Grams, WeightUnit.Pounds), g => g * 0.00220462m },
{ (WeightUnit.Grams, WeightUnit.Ounces), g => g * 0.035274m },
{ (WeightUnit.Grams, WeightUnit.Stones), g => g * 0.000157473m },
{ (WeightUnit.Grams, WeightUnit.MetricTons), g => g / 1_000_000m },
{ (WeightUnit.Grams, WeightUnit.USTons), g => g / 907_185m },


// Conversions from Milligrams
{ (WeightUnit.Milligrams, WeightUnit.Kilograms), mg => mg / 1000000m },
{ (WeightUnit.Milligrams, WeightUnit.Grams), mg => mg / 1000m },
{ (WeightUnit.Milligrams, WeightUnit.Pounds), mg => mg * 0.00000220462m },
{ (WeightUnit.Milligrams, WeightUnit.Ounces), mg => mg * 0.000035274m },
{ (WeightUnit.Milligrams, WeightUnit.Stones), mg => mg * 0.000000157473m },
{ (WeightUnit.Milligrams, WeightUnit.MetricTons), mg => mg / 1_000_000_000m },
{ (WeightUnit.Milligrams, WeightUnit.USTons), mg => mg / 907_185_000m },

// Conversions from Pounds
{ (WeightUnit.Pounds, WeightUnit.Kilograms), lb => lb * 0.45359237m },
{ (WeightUnit.Pounds, WeightUnit.Grams), lb => lb * 453.59237m },
{ (WeightUnit.Pounds, WeightUnit.Milligrams), lb => lb * 453592.37m },
{ (WeightUnit.Pounds, WeightUnit.Ounces), lb => lb * 16m },
{ (WeightUnit.Pounds, WeightUnit.Stones), lb => lb /14m },
{ (WeightUnit.Pounds, WeightUnit.MetricTons), lb => lb / 2204.62m },
{ (WeightUnit.Pounds, WeightUnit.USTons), lb => lb / 2000m },

// Conversions from Ounces
{ (WeightUnit.Ounces, WeightUnit.Kilograms), oz => oz / 35.274m },
{ (WeightUnit.Ounces, WeightUnit.Grams), oz => oz * 28.3495m },
{ (WeightUnit.Ounces, WeightUnit.Milligrams), oz => oz * 28349.5m },
{ (WeightUnit.Ounces, WeightUnit.Pounds), oz => oz / 16m },
{ (WeightUnit.Ounces, WeightUnit.Stones), oz => oz * 0.00446429m },
{ (WeightUnit.Ounces, WeightUnit.MetricTons), oz => oz / 35274m },
{ (WeightUnit.Ounces, WeightUnit.USTons), oz => oz / 32000m },

// Conversions from Stones
{ (WeightUnit.Stones, WeightUnit.Kilograms), st => st * 6.35029318m },
{ (WeightUnit.Stones, WeightUnit.Grams), st => st * 6350.29318m },
{ (WeightUnit.Stones, WeightUnit.Milligrams), st => st * 6350293.18m },
{ (WeightUnit.Stones, WeightUnit.Pounds), st => st * 14m },
{ (WeightUnit.Stones, WeightUnit.Ounces), st => st * 224m },
{ (WeightUnit.Stones, WeightUnit.MetricTons), st => st / 157.473m },
{ (WeightUnit.Stones, WeightUnit.USTons), st => st / 142.857m },

// Conversions from MetricTons,
{ (WeightUnit.MetricTons, WeightUnit.Kilograms), mt => mt * 1000m },
{ (WeightUnit.MetricTons, WeightUnit.Grams), mt => mt * 1_000_000m },
{ (WeightUnit.MetricTons, WeightUnit.Milligrams), mt => mt * 1_000_000_000m },
{ (WeightUnit.MetricTons, WeightUnit.Pounds), mt => mt * 2204.6226218487756m },
{ (WeightUnit.MetricTons, WeightUnit.Ounces), mt => mt * 35274m },
{ (WeightUnit.MetricTons, WeightUnit.Stones), mt => mt * 157.4730444178m },
{ (WeightUnit.MetricTons, WeightUnit.USTons), mt => mt * 1.10231m },

// Conversion from Tons
{ (WeightUnit.USTons, WeightUnit.Kilograms), t => t * 907.185m },
{ (WeightUnit.USTons, WeightUnit.Grams), t => t * 907185m },
{ (WeightUnit.USTons, WeightUnit.Milligrams), t => t * 907185000m },
{ (WeightUnit.USTons, WeightUnit.Pounds), t => t * 2000m },
{ (WeightUnit.USTons, WeightUnit.Ounces), t => t * 32000m },
{ (WeightUnit.USTons, WeightUnit.Stones), t => t * 142.85714285714284m },
{ (WeightUnit.USTons, WeightUnit.MetricTons), t => t * 0.907185m }

    
};
    public static decimal ConvertWeight(decimal value, WeightUnit fromUnit, WeightUnit toUnit)
    {
        if (fromUnit == toUnit)
        {
            return value;
        }
        var func = conversionFunctions[(fromUnit, toUnit)];
        return func(value);

    }

    private static double ConvertToGrams(double value, WeightUnit fromUnit)
    {
        switch (fromUnit)
        {
            case WeightUnit.Milligrams:
                return value / 1000;
            case WeightUnit.Grams:
                return value;
            case WeightUnit.Kilograms:
                return value * 1000;
            case WeightUnit.MetricTons:
                return value * 1_000_000;
            case WeightUnit.Ounces:
                return value * 28.3495;
            case WeightUnit.Pounds:
                return value * 453.592;
            case WeightUnit.Stones:
                return value * 6_350.29318;
            default:
                throw new ArgumentOutOfRangeException(nameof(fromUnit), fromUnit, null);
        }
    }

    private static double ConvertFromGrams(double value, WeightUnit toUnit)
    {
        switch (toUnit)
        {
            case WeightUnit.Milligrams:
                return value * 1000;
            case WeightUnit.Grams:
                return value;
            case WeightUnit.Kilograms:
                return value / 1000;
            case WeightUnit.MetricTons:
                return value / 1_000_000;
            case WeightUnit.Ounces:
                return value / 28.3495;
            case WeightUnit.Pounds:
                return value / 453.592;
            case WeightUnit.Stones:
                return value/ 6_350.29318;
            default:
                throw new ArgumentOutOfRangeException(nameof(toUnit), toUnit, null);
        }
    }
}
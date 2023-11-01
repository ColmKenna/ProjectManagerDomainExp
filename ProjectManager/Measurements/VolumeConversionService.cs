namespace ProjectManager.Measurements;

public static class VolumeConversionService
{
    public static Dictionary<(VolumeUnit, VolumeUnit), Func<decimal, decimal>> conversionFuncs =
        new Dictionary<(VolumeUnit, VolumeUnit), Func<decimal, decimal>>
        {
            // Conversions from Liters
            { (VolumeUnit.Liters, VolumeUnit.Milliliters), l => l * 1000.0m },
            { (VolumeUnit.Liters, VolumeUnit.CubicMeters), l => l / 1000.0m},
            { (VolumeUnit.Liters, VolumeUnit.CubicCentimeters), l => l * 1000.0m },
            { (VolumeUnit.Liters, VolumeUnit.CubicInches), l => l * 61.0237440947323m },
            { (VolumeUnit.Liters, VolumeUnit.CubicFeet), l => l * 0.035314666721489m},
            { (VolumeUnit.Liters, VolumeUnit.Gallons), l => l * 0.264172052358148m },
            { (VolumeUnit.Liters, VolumeUnit.Quarts), l => l * 1.05668820943259m },
            { (VolumeUnit.Liters, VolumeUnit.USPints), l => l * 2.11337641886519m },
            { (VolumeUnit.Liters, VolumeUnit.FluidOunces), l => l * 33.814022701843m },
            { (VolumeUnit.Liters, VolumeUnit.CubicYards), l => l * 0.00130795061931439m },
            { (VolumeUnit.Liters, VolumeUnit.UKPints), l => l * 1.7597539863927023616m },

            // Conversions from Milliliters
            { (VolumeUnit.Milliliters, VolumeUnit.Liters), ml => ml / 1000.0m },
            { (VolumeUnit.Milliliters, VolumeUnit.CubicMeters), ml => ml / 1000000.0m },
            { (VolumeUnit.Milliliters, VolumeUnit.CubicCentimeters), ml => ml },
            { (VolumeUnit.Milliliters, VolumeUnit.CubicInches), ml => ml * 0.0610237440947323m },
            { (VolumeUnit.Milliliters, VolumeUnit.CubicFeet), ml => ml * 0.0000353154868024998m },
            { (VolumeUnit.Milliliters, VolumeUnit.Gallons), ml => ml * 0.000264172052358148m },
            { (VolumeUnit.Milliliters, VolumeUnit.Quarts), ml => ml * 0.00105668820943259m },
            { (VolumeUnit.Milliliters, VolumeUnit.USPints), ml => ml * 0.00211337641886519m },
            { (VolumeUnit.Milliliters, VolumeUnit.FluidOunces), ml => ml * 0.033814022701843m },
            { (VolumeUnit.Milliliters, VolumeUnit.CubicYards), ml => ml * 0.00000130795061931439m },
            { (VolumeUnit.Milliliters, VolumeUnit.UKPints), ml => ml * 0.0017597539863927m },

            // Conversions from CubicMeters
            { (VolumeUnit.CubicMeters, VolumeUnit.Liters), m3 => m3 * 1000.0m },
    { (VolumeUnit.CubicMeters, VolumeUnit.Milliliters), m3 => m3 * 1000000.0m },
    { (VolumeUnit.CubicMeters, VolumeUnit.CubicCentimeters), m3 => m3 * 1000000.0m },
    { (VolumeUnit.CubicMeters, VolumeUnit.CubicInches), m3 => m3 * 61023.744094732288000m },
    { (VolumeUnit.CubicMeters, VolumeUnit.CubicFeet), m3 => m3 * 35.31466672148859m },
    { (VolumeUnit.CubicMeters, VolumeUnit.Gallons), m3 => m3 * 264.1720523581m },
    { (VolumeUnit.CubicMeters, VolumeUnit.Quarts), m3 => m3 * 1056.688209432593664m },
    { (VolumeUnit.CubicMeters, VolumeUnit.USPints), m3 => m3 * 2113.376418865187109m },
    { (VolumeUnit.CubicMeters, VolumeUnit.FluidOunces), m3 => m3 * 33814m },
    { (VolumeUnit.CubicMeters, VolumeUnit.CubicYards), m3 => m3 * 1.30795061931439225m },
    { (VolumeUnit.CubicMeters, VolumeUnit.UKPints), m3 => m3 * 1759.7539863927023616m},

    // Conversions from Cubic Centimeters
    { (VolumeUnit.CubicCentimeters, VolumeUnit.Liters), cc => cc / 1000m },
    { (VolumeUnit.CubicCentimeters, VolumeUnit.Milliliters), cc => cc },
    { (VolumeUnit.CubicCentimeters, VolumeUnit.CubicMeters), cc => cc / 1000000m },
    { (VolumeUnit.CubicCentimeters, VolumeUnit.CubicInches), cc => cc * 0.061023744094732288m },
    { (VolumeUnit.CubicCentimeters, VolumeUnit.CubicFeet), cc => cc * 0.0000353147m },
    { (VolumeUnit.CubicCentimeters, VolumeUnit.Gallons), cc => cc * 0.000264172m },
    { (VolumeUnit.CubicCentimeters, VolumeUnit.Quarts), cc => cc * 0.001057m },
    { (VolumeUnit.CubicCentimeters, VolumeUnit.USPints), cc => cc * 0.00211338m },
    { (VolumeUnit.CubicCentimeters, VolumeUnit.FluidOunces), cc => cc * 0.033814m },
    { (VolumeUnit.CubicCentimeters, VolumeUnit.CubicYards), cc => cc * 0.000001308m },
    { (VolumeUnit.CubicCentimeters, VolumeUnit.UKPints), cc => cc * 0.00175975m },
// Conversions from Cubic Inches
            { (VolumeUnit.CubicInches, VolumeUnit.Liters), cuin => cuin * 0.016387064m },
            { (VolumeUnit.CubicInches, VolumeUnit.Milliliters), cuin => cuin * 16.387064m },
            { (VolumeUnit.CubicInches, VolumeUnit.CubicMeters), cuin => cuin * 0.000016387064m },
            { (VolumeUnit.CubicInches, VolumeUnit.CubicCentimeters), cuin => cuin * 16.387064m },
            { (VolumeUnit.CubicInches, VolumeUnit.CubicFeet), cuin => cuin * 0.000578703704m },
            { (VolumeUnit.CubicInches, VolumeUnit.Gallons), cuin => cuin * 0.004329004329m },
            { (VolumeUnit.CubicInches, VolumeUnit.Quarts), cuin => cuin * 0.017316017316m },
            { (VolumeUnit.CubicInches, VolumeUnit.USPints), cuin => cuin * 0.034632034632m },
            { (VolumeUnit.CubicInches, VolumeUnit.FluidOunces), cuin => cuin * 0.554112554113m },
            { (VolumeUnit.CubicInches, VolumeUnit.CubicYards), cuin => cuin * 0.0000214334705075m },
            { (VolumeUnit.CubicInches, VolumeUnit.UKPints), cuin => cuin * 0.028837201199272341504m },

// Conversions from Cubic Feet
    { (VolumeUnit.CubicFeet, VolumeUnit.Liters), cuft => cuft * 28.316846592m },
    { (VolumeUnit.CubicFeet, VolumeUnit.Milliliters), cuft => cuft * 28316.846592m },
    { (VolumeUnit.CubicFeet, VolumeUnit.CubicMeters), cuft => cuft * 0.028316846592m },
    { (VolumeUnit.CubicFeet, VolumeUnit.CubicCentimeters), cuft => cuft * 28316.846592m },
    { (VolumeUnit.CubicFeet, VolumeUnit.CubicInches), cuft => cuft * 1728.0m },
    { (VolumeUnit.CubicFeet, VolumeUnit.Gallons), cuft => cuft * 7.48051948051948m },
    { (VolumeUnit.CubicFeet, VolumeUnit.Quarts), cuft => cuft * 29.922077922077920m},
    { (VolumeUnit.CubicFeet, VolumeUnit.USPints), cuft => cuft * 59.844155844155842560m },
    { (VolumeUnit.CubicFeet, VolumeUnit.FluidOunces), cuft => cuft * 957.506493506494m },
    { (VolumeUnit.CubicFeet, VolumeUnit.CubicYards), cuft => cuft * 0.037037037037037m },
    { (VolumeUnit.CubicFeet, VolumeUnit.UKPints), cuft => cuft * 49.8307m }, 

    // Conversion From Gallons
    { (VolumeUnit.Gallons, VolumeUnit.Liters), gal => gal * 3.78541m },
    { (VolumeUnit.Gallons, VolumeUnit.Milliliters), gal => gal * 3785.41m },
    { (VolumeUnit.Gallons, VolumeUnit.CubicMeters), gal => gal * 0.00378541m },
    { (VolumeUnit.Gallons, VolumeUnit.CubicCentimeters), gal => gal * 3785.41m },
    { (VolumeUnit.Gallons, VolumeUnit.CubicInches), gal => gal * 231m },
    { (VolumeUnit.Gallons, VolumeUnit.CubicFeet), gal => gal * 0.133681m },
    { (VolumeUnit.Gallons, VolumeUnit.Quarts), gal => gal * 4m },
    { (VolumeUnit.Gallons, VolumeUnit.USPints), gal => gal * 8m },
    { (VolumeUnit.Gallons, VolumeUnit.FluidOunces), gal => gal * 128m },
    { (VolumeUnit.Gallons, VolumeUnit.CubicYards), gal => gal * 0.00495113m },
    { (VolumeUnit.Gallons, VolumeUnit.UKPints), gal => gal * 6.66139m },

    // Conversions from Quarts
    { (VolumeUnit.Quarts, VolumeUnit.Liters), qt => qt * 0.946352946m },
    { (VolumeUnit.Quarts, VolumeUnit.Milliliters), qt => qt * 946.353m },
    { (VolumeUnit.Quarts, VolumeUnit.CubicMeters), qt => qt * 0.000946353m },
    { (VolumeUnit.Quarts, VolumeUnit.CubicCentimeters), qt => qt * 946.353m },
    { (VolumeUnit.Quarts, VolumeUnit.CubicInches), qt => qt * 57.75m },
    { (VolumeUnit.Quarts, VolumeUnit.CubicFeet), qt => qt * 0.0334201m },
    { (VolumeUnit.Quarts, VolumeUnit.Gallons), qt => qt * 0.25m },
    { (VolumeUnit.Quarts, VolumeUnit.USPints), qt => qt * 2m },
    { (VolumeUnit.Quarts, VolumeUnit.FluidOunces), qt => qt * 32m },
    { (VolumeUnit.Quarts, VolumeUnit.CubicYards), qt => qt /807.9m },
    { (VolumeUnit.Quarts, VolumeUnit.UKPints), qt => qt * 1.66535m },

    // Conversions from Pints
    { (VolumeUnit.USPints, VolumeUnit.Liters), pt => pt * 0.473176473m },
    { (VolumeUnit.USPints, VolumeUnit.Milliliters), pt => pt * 473.176473m },
    { (VolumeUnit.USPints, VolumeUnit.CubicMeters), pt => pt * 0.000473176473m },
    { (VolumeUnit.USPints, VolumeUnit.CubicCentimeters), pt => pt * 473.176473m },
    { (VolumeUnit.USPints, VolumeUnit.CubicInches), pt => pt * 28.875m },
    { (VolumeUnit.USPints, VolumeUnit.CubicFeet), pt => pt * 0.0167100694444444m },
    { (VolumeUnit.USPints, VolumeUnit.Gallons), pt => pt * 0.125m },
    { (VolumeUnit.USPints, VolumeUnit.Quarts), pt => pt * 0.5m },
    { (VolumeUnit.USPints, VolumeUnit.FluidOunces), pt => pt * 16.0m },
    { (VolumeUnit.USPints, VolumeUnit.CubicYards), pt => pt * 0.00061889146090534976563m },
    { (VolumeUnit.USPints, VolumeUnit.UKPints), pt => pt * 0.832674184604601m },

    // Conversions from Fluid Ounces
    { (VolumeUnit.FluidOunces, VolumeUnit.Liters), flOz => flOz * 0.0295735295625m },
    { (VolumeUnit.FluidOunces, VolumeUnit.Milliliters), flOz => flOz * 29.5735295625m },
    { (VolumeUnit.FluidOunces, VolumeUnit.CubicMeters), flOz => flOz * 0.0000295735295625m },
    { (VolumeUnit.FluidOunces, VolumeUnit.CubicCentimeters), flOz => flOz * 29.5735295625m },
    { (VolumeUnit.FluidOunces, VolumeUnit.CubicInches), flOz => flOz * 1.8046875m },
    { (VolumeUnit.FluidOunces, VolumeUnit.CubicFeet), flOz => flOz * 0.00104437934027778m },
    { (VolumeUnit.FluidOunces, VolumeUnit.Gallons), flOz => flOz * 0.0078125m },
    { (VolumeUnit.FluidOunces, VolumeUnit.Quarts), flOz => flOz * 0.03125m },
    { (VolumeUnit.FluidOunces, VolumeUnit.USPints), flOz => flOz * 0.0625m },
    { (VolumeUnit.FluidOunces, VolumeUnit.CubicYards), flOz => flOz / 25850m },
    { (VolumeUnit.FluidOunces, VolumeUnit.UKPints), flOz => flOz * 0.052042126456m },

    // Conversion from Cubic Yards
    { (VolumeUnit.CubicYards, VolumeUnit.Liters), cy => cy * 764.554858m },
    { (VolumeUnit.CubicYards, VolumeUnit.Milliliters), cy => cy * 764554.858m },
    { (VolumeUnit.CubicYards, VolumeUnit.CubicMeters), cy => cy * 0.764554857984m },
    { (VolumeUnit.CubicYards, VolumeUnit.CubicCentimeters), cy => cy * 764554.858m },
    { (VolumeUnit.CubicYards, VolumeUnit.CubicInches), cy => cy * 46656m },
    { (VolumeUnit.CubicYards, VolumeUnit.CubicFeet), cy => cy * 27m },
    { (VolumeUnit.CubicYards, VolumeUnit.Gallons), cy => cy * 201.974026m },
    { (VolumeUnit.CubicYards, VolumeUnit.Quarts), cy => cy * 807.89610389615m },
    { (VolumeUnit.CubicYards, VolumeUnit.USPints), cy => cy * 1615.792233m },
    { (VolumeUnit.CubicYards, VolumeUnit.FluidOunces), cy => cy * 25852.7m },
    { (VolumeUnit.CubicYards, VolumeUnit.UKPints), cy => cy * 1345.43m },

    // Conversion from UKPints
    { (VolumeUnit.UKPints, VolumeUnit.Liters), ukpt => ukpt * 0.5682612500m },
    { (VolumeUnit.UKPints, VolumeUnit.Milliliters), ukpt => ukpt * 568.26125m },
    { (VolumeUnit.UKPints, VolumeUnit.CubicMeters), ukpt => ukpt * 0.00056826125m },
    { (VolumeUnit.UKPints, VolumeUnit.CubicCentimeters), ukpt => ukpt * 568.26125m },
    { (VolumeUnit.UKPints, VolumeUnit.CubicInches), ukpt => ukpt * 34.677429098955308007m },
    { (VolumeUnit.UKPints, VolumeUnit.CubicFeet), ukpt => ukpt * 0.020067957m },
    { (VolumeUnit.UKPints, VolumeUnit.Gallons), ukpt => ukpt * 0.15011874068810686m },
    { (VolumeUnit.UKPints, VolumeUnit.Quarts), ukpt => ukpt * 0.60047496275242742m },
    { (VolumeUnit.UKPints, VolumeUnit.USPints), ukpt => ukpt * 1.2009499255048548m },
    { (VolumeUnit.UKPints, VolumeUnit.FluidOunces), ukpt => ukpt * 19.215198m }, 
    { (VolumeUnit.UKPints, VolumeUnit.CubicYards), ukpt => ukpt /1345m }
        };

    public static decimal ConvertVolume(decimal value, VolumeUnit fromUnit, VolumeUnit toUnit)
    {
        if (fromUnit == toUnit)
        {
            return value;
        }

        var func = conversionFuncs[(fromUnit, toUnit)];
        return func(value);
    }

    private static double ConvertFromMilliliters(double value, VolumeUnit toUnit)
    {
        switch (toUnit)
        {
            case VolumeUnit.Liters:
                return value / 1000;
            case VolumeUnit.CubicMeters:
                return value / 1_000_000;
            case VolumeUnit.CubicCentimeters:
                return value;
            case VolumeUnit.CubicInches:
                return value / 16.3871;
            case VolumeUnit.CubicFeet:
                return value / 28_316.8466;
            case VolumeUnit.CubicYards:
                return value / 76455.4855;
            case VolumeUnit.Gallons:
                return value / 3785.41;
            default:
                throw new ArgumentOutOfRangeException(nameof(toUnit), toUnit, null);
        }
    }

    private static double ConvertFromLiters(double value, VolumeUnit toUnit)
    {
        switch (toUnit)
        {
            case VolumeUnit.Milliliters:
                return value * 1000;
            case VolumeUnit.CubicMeters:
                return value / 1000;
            case VolumeUnit.CubicCentimeters:
                return value * 1000;
            case VolumeUnit.CubicInches:
                return value / 0.0163871;
            case VolumeUnit.CubicFeet:
                return value / 28.3168466;
            case VolumeUnit.CubicYards:
                return value / 764.554858;
            case VolumeUnit.Gallons:
                return value * 0.264172052;
            default:
                throw new ArgumentOutOfRangeException(nameof(toUnit), toUnit, null);
        }
    }

    private static double ConvertFromCubicMeters(double value, VolumeUnit toUnit)
    {
        switch (toUnit)
        {
            case VolumeUnit.Milliliters:
                return value * 1_000_000;
            case VolumeUnit.Liters:
                return value * 1000;
            case VolumeUnit.CubicCentimeters:
                return value * 1_000_000;
            case VolumeUnit.CubicInches:
                return value * 61_023.7441;
            case VolumeUnit.CubicFeet:
                return value * 35.3146667;
            case VolumeUnit.CubicYards:
                return value * 1.30795062;
            case VolumeUnit.Gallons:
                return value * 264.172052;
            default:
                throw new ArgumentOutOfRangeException(nameof(toUnit), toUnit, null);
        }
    }

    private static double ConvertFromCubicCentimeters(double value, VolumeUnit toUnit)
    {
        switch (toUnit)
        {
            case VolumeUnit.Milliliters:
                return value;
            case VolumeUnit.Liters:
                return value / 1000;
            case VolumeUnit.CubicMeters:
                return value / 1_000_000;
            case VolumeUnit.CubicInches:
                return value / 16.387064;
            case VolumeUnit.CubicFeet:
                return value / 28_316.846592;
            case VolumeUnit.CubicYards:
                return value / 764554.858;
            case VolumeUnit.Gallons:
                return value / 3785.41178;
            default:
                throw new ArgumentOutOfRangeException(nameof(toUnit), toUnit, null);
        }
    }

    private static double ConvertFromCubicInches(double value, VolumeUnit toUnit)
    {
        switch (toUnit)
        {
            case VolumeUnit.Milliliters:
                return value * 16.387064;
            case VolumeUnit.Liters:
                return value / 61.0237441;
            case VolumeUnit.CubicMeters:
                return value / 61023.7441;
            case VolumeUnit.CubicCentimeters:
                return value * 16.387064;
            case VolumeUnit.CubicFeet:
                return value / 1728;
            case VolumeUnit.CubicYards:
                return value / 46656;
            case VolumeUnit.Gallons:
                return value / 231;
            default:
                throw new ArgumentOutOfRangeException(nameof(toUnit), toUnit, null);
        }
    }

    private static double ConvertFromCubicFeet(double value, VolumeUnit toUnit)
    {
        switch (toUnit)
        {
            case VolumeUnit.Milliliters:
                return value * 28_316.846592;
            case VolumeUnit.Liters:
                return value * 28.3168466;
            case VolumeUnit.CubicMeters:
                return value / 35.3146667;
            case VolumeUnit.CubicCentimeters:
                return value * 28_316.846592;
            case VolumeUnit.CubicInches:
                return value * 1728;
            case VolumeUnit.CubicYards:
                return value / 27;
            case VolumeUnit.Gallons:
                return value * 7.48051948;
            default:
                throw new ArgumentOutOfRangeException(nameof(toUnit), toUnit, null);
        }
    }

    private static double ConvertFromCubicYards(double value, VolumeUnit toUnit)
    {
        switch (toUnit)
        {
            case VolumeUnit.Milliliters:
                return value * 764554.858;
            case VolumeUnit.Liters:
                return value * 764.554858;
            case VolumeUnit.CubicMeters:
                return value / 1.30795062;
            case VolumeUnit.CubicCentimeters:
                return value * 764554.858;
            case VolumeUnit.CubicInches:
                return value * 46656;
            case VolumeUnit.CubicFeet:
                return value * 27;
            case VolumeUnit.Gallons:
                return value * 201.974026;
            default:
                throw new ArgumentOutOfRangeException(nameof(toUnit), toUnit, null);
        }
    }

    private static double ConvertFromGallons(double value, VolumeUnit toUnit)
    {
        switch (toUnit)
        {
            case VolumeUnit.Milliliters:
                return value * 3785.41178;
            case VolumeUnit.Liters:
                return value * 3.78541178;
            case VolumeUnit.CubicMeters:
                return value / 264.172052;
            case VolumeUnit.CubicCentimeters:
                return value * 3785.41178;
            case VolumeUnit.CubicInches:
                return value * 231;
            case VolumeUnit.CubicFeet:
                return value / 7.48051948;
            case VolumeUnit.CubicYards:
                return value / 201.974026;
            default:
                throw new ArgumentOutOfRangeException(nameof(toUnit), toUnit, null);
        }
    }
}
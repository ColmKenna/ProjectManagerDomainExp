namespace ProjectManager.Measurements;

public class AreaConversionService
{
    
    public static Dictionary<(AreaUnit, AreaUnit), Func<double, double>> conversionFuncs =
        new Dictionary<(AreaUnit, AreaUnit), Func<double, double>>
        {
            // Conversions from Square Meters
            { (AreaUnit.SquareMeters, AreaUnit.SquareCentimeters), m2 => m2 * 10000 },
            { (AreaUnit.SquareMeters, AreaUnit.SquareMillimeters), m2 => m2 * 1000000 },
            { (AreaUnit.SquareMeters, AreaUnit.SquareKilometers), m2 => m2 / 1000000 },
            { (AreaUnit.SquareMeters, AreaUnit.SquareInches), m2 => m2 * 1550 },
            { (AreaUnit.SquareMeters, AreaUnit.SquareFeet), m2 => m2 *  10.763910416709722112},
            { (AreaUnit.SquareMeters, AreaUnit.SquareYards), m2 => m2 * 1.19599 },
            { (AreaUnit.SquareMeters, AreaUnit.Acres), m2 => m2 * 0.000247105 },
            { (AreaUnit.SquareMeters, AreaUnit.Hectares), m2 => m2 * 0.0001 },
            { (AreaUnit.SquareMeters, AreaUnit.SquareMiles), m2 => m2 * 0.00000038610215855 },

            // Conversions from Square Centimeters
            { (AreaUnit.SquareCentimeters, AreaUnit.SquareMeters), cm2 => cm2 / 10000 },
            { (AreaUnit.SquareCentimeters, AreaUnit.SquareMillimeters), cm2 => cm2 * 100 },
            { (AreaUnit.SquareCentimeters, AreaUnit.SquareKilometers), cm2 => cm2 / 10000000000 },
            { (AreaUnit.SquareCentimeters, AreaUnit.SquareInches), cm2 => cm2 * 0.155 },
            { (AreaUnit.SquareCentimeters, AreaUnit.SquareFeet), cm2 => cm2 * 0.00107639 },
            { (AreaUnit.SquareCentimeters, AreaUnit.SquareYards), cm2 => cm2 * 0.000119599 },
            { (AreaUnit.SquareCentimeters, AreaUnit.Acres), cm2 => cm2 * 0.0000000247105 },
            { (AreaUnit.SquareCentimeters, AreaUnit.Hectares), cm2 => cm2 * 0.00000001 },
            { (AreaUnit.SquareCentimeters, AreaUnit.SquareMiles), cm2 => cm2 * 0.0000000000386102 },
            
            // Conversions from Square Millimeters
            { (AreaUnit.SquareMillimeters, AreaUnit.SquareMeters), mm2 => mm2 / 1000000 },
            { (AreaUnit.SquareMillimeters, AreaUnit.SquareCentimeters), mm2 => mm2 / 100 },
            { (AreaUnit.SquareMillimeters, AreaUnit.SquareKilometers), mm2 => mm2 / 1000000000000 },
            { (AreaUnit.SquareMillimeters, AreaUnit.SquareInches), mm2 => mm2 * 0.00155 },
            { (AreaUnit.SquareMillimeters, AreaUnit.SquareFeet), mm2 => mm2 * 0.000010764 },
            { (AreaUnit.SquareMillimeters, AreaUnit.SquareYards), mm2 => mm2 * 0.000001196 },
            { (AreaUnit.SquareMillimeters, AreaUnit.Acres), mm2 => mm2 * 0.000000000247105 },
            { (AreaUnit.SquareMillimeters, AreaUnit.Hectares), mm2 => mm2 * 0.0000000001 },
            { (AreaUnit.SquareMillimeters, AreaUnit.SquareMiles), mm2 => mm2 * 0.0000000000003861022 },
            
            // Conversions from Square Kilometers
            { (AreaUnit.SquareKilometers, AreaUnit.SquareMeters), km2 => km2 * 1000000 },
            { (AreaUnit.SquareKilometers, AreaUnit.SquareCentimeters), km2 => km2 * 10000000000 },
            { (AreaUnit.SquareKilometers, AreaUnit.SquareMillimeters), km2 => km2 * 1000000000000 },
            { (AreaUnit.SquareKilometers, AreaUnit.SquareInches), km2 => km2 * 1550003100 },
            { (AreaUnit.SquareKilometers, AreaUnit.SquareFeet), km2 => km2 * 10763910.416709722112 },
            { (AreaUnit.SquareKilometers, AreaUnit.SquareYards), km2 => km2 * 1195990.046301 },
            { (AreaUnit.SquareKilometers, AreaUnit.Acres), km2 => km2 * 247.105381467 },
            { (AreaUnit.SquareKilometers, AreaUnit.Hectares), km2 => km2 * 100 },
            { (AreaUnit.SquareKilometers, AreaUnit.SquareMiles), km2 => km2 * 0.386102 },
            
            // Conversions from Square Inches
            { (AreaUnit.SquareInches, AreaUnit.SquareMeters), in2 => in2 * 0.00064516 },
            { (AreaUnit.SquareInches, AreaUnit.SquareCentimeters), in2 => in2 * 6.4516 },
            { (AreaUnit.SquareInches, AreaUnit.SquareMillimeters), in2 => in2 * 645.16 },
            { (AreaUnit.SquareInches, AreaUnit.SquareKilometers), in2 => in2 * 0.00000000064516 },
            { (AreaUnit.SquareInches, AreaUnit.SquareFeet), in2 => in2 * 0.00694444 },
            { (AreaUnit.SquareInches, AreaUnit.SquareYards), in2 => in2 * 0.000771605 },
            { (AreaUnit.SquareInches, AreaUnit.Acres), in2 => in2 * 0.000000015625 },
            { (AreaUnit.SquareInches, AreaUnit.Hectares), in2 => in2 * 0.0000000064516 },
            { (AreaUnit.SquareInches, AreaUnit.SquareMiles), in2 => in2 * 0.0000000000000254221 },
            
            // Conversions from Square Feet
            { (AreaUnit.SquareFeet, AreaUnit.SquareMeters), ft2 => ft2 * 0.092903 },
            { (AreaUnit.SquareFeet, AreaUnit.SquareCentimeters), ft2 => ft2 *  929.0304 },
            { (AreaUnit.SquareFeet, AreaUnit.SquareMillimeters), ft2 => ft2 * 92903.04 },
            { (AreaUnit.SquareFeet, AreaUnit.SquareKilometers), ft2 => ft2 * 0.000000092903 },
            { (AreaUnit.SquareFeet, AreaUnit.SquareInches), ft2 => ft2 * 144 },
            { (AreaUnit.SquareFeet, AreaUnit.SquareYards), ft2 => ft2 * 0.111111 },
            { (AreaUnit.SquareFeet, AreaUnit.Acres), ft2 => ft2 * 0.0000229568 },
            { (AreaUnit.SquareFeet, AreaUnit.Hectares), ft2 => ft2 * 0.0000092903 },
            { (AreaUnit.SquareFeet, AreaUnit.SquareMiles), ft2 => ft2 * 0.00000003587006 },

            // Conversions from Square Yards
            { (AreaUnit.SquareYards, AreaUnit.SquareMeters), yd2 => yd2 * 0.83612736 },
            { (AreaUnit.SquareYards, AreaUnit.SquareCentimeters), yd2 => yd2 *  8361.2736 },
            { (AreaUnit.SquareYards, AreaUnit.SquareMillimeters), yd2 => yd2 * 836127.36 },
            { (AreaUnit.SquareYards, AreaUnit.SquareKilometers), yd2 => yd2 * 0.000000836127 },
            { (AreaUnit.SquareYards, AreaUnit.SquareInches), yd2 => yd2 * 1296 },
            { (AreaUnit.SquareYards, AreaUnit.SquareFeet), yd2 => yd2 * 9 },
            { (AreaUnit.SquareYards, AreaUnit.Acres), yd2 => yd2 * 0.000206612 },
            { (AreaUnit.SquareYards, AreaUnit.Hectares), yd2 => yd2 * 0.0000836127 },
            { (AreaUnit.SquareYards, AreaUnit.SquareMiles), yd2 => yd2 * 0.000000322831 },
            
            // Conversions from Acres
            { (AreaUnit.Acres, AreaUnit.SquareMeters), ac => ac * 4046.8564224 },
            { (AreaUnit.Acres, AreaUnit.SquareCentimeters), ac => ac * 40468564.224 },
            { (AreaUnit.Acres, AreaUnit.SquareMillimeters), ac => ac * 4046856422.4 },
            { (AreaUnit.Acres, AreaUnit.SquareKilometers), ac => ac * 0.0040468564224 },
            { (AreaUnit.Acres, AreaUnit.SquareInches), ac => ac * 6272640 },
            { (AreaUnit.Acres, AreaUnit.SquareFeet), ac => ac * 43560 },
            { (AreaUnit.Acres, AreaUnit.SquareYards), ac => ac * 4840 },
            { (AreaUnit.Acres, AreaUnit.Hectares), ac => ac * 0.404686 },
            { (AreaUnit.Acres, AreaUnit.SquareMiles), ac => ac * 0.0015625 },
            
            // Conversions from Hectares
            { (AreaUnit.Hectares, AreaUnit.SquareMeters), ha => ha * 10000 },
            { (AreaUnit.Hectares, AreaUnit.SquareCentimeters), ha => ha * 100000000 },
            { (AreaUnit.Hectares, AreaUnit.SquareMillimeters), ha => ha * 10000000000 },
            { (AreaUnit.Hectares, AreaUnit.SquareKilometers), ha => ha * 0.01 },
            { (AreaUnit.Hectares, AreaUnit.SquareInches), ha => ha * 15500031 },
            { (AreaUnit.Hectares, AreaUnit.SquareFeet), ha => ha * 107639 },
            { (AreaUnit.Hectares, AreaUnit.SquareYards), ha => ha * 11960 },
            { (AreaUnit.Hectares, AreaUnit.Acres), ha => ha * 2.47105 },
            { (AreaUnit.Hectares, AreaUnit.SquareMiles), ha => ha * 0.00386102 },
            
            // Conversions from Square Miles
            { (AreaUnit.SquareMiles, AreaUnit.SquareMeters), mi2 => mi2 * 2589988.11 },
            { (AreaUnit.SquareMiles, AreaUnit.SquareCentimeters), mi2 => mi2 * 25899881100 },
            { (AreaUnit.SquareMiles, AreaUnit.SquareMillimeters), mi2 => mi2 * 2589988110000 },
            { (AreaUnit.SquareMiles, AreaUnit.SquareKilometers), mi2 => mi2 * 2.589988110336 },
            { (AreaUnit.SquareMiles, AreaUnit.SquareInches), mi2 => mi2 * 4014489599.4792 },
            { (AreaUnit.SquareMiles, AreaUnit.SquareFeet), mi2 => mi2 * 27878400 },
            { (AreaUnit.SquareMiles, AreaUnit.SquareYards), mi2 => mi2 * 3097599.999598 },
            { (AreaUnit.SquareMiles, AreaUnit.Acres), mi2 => mi2 * 640 },
            { (AreaUnit.SquareMiles, AreaUnit.Hectares), mi2 => mi2 * 259 },
        };
    public static decimal ConvertArea(decimal value, AreaUnit fromUnit, AreaUnit toUnit)
    {
        if (fromUnit == toUnit)
        {
            return value;
        }
        else
        {
            var conversionFunc = conversionFuncs[(fromUnit, toUnit)];
            return (decimal)conversionFunc((double)value);
    }

    }
}
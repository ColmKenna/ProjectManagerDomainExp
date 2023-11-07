using PrimativeExtensions;

namespace Measurements;



public static class DurationConversionService
{
    public static Duration ConvertToTimeUnit(int Units, TimeUnit Time, TimeUnit toUnit, DateTime? date)
    {
        Func<int> getDaysInMonth = () => DateTime.DaysInMonth(date.Value.Year, date.Value.Month);
        Func<int> getDaysInYear = () => DateTime.IsLeapYear(date.Value.Year) ? 366 : 365;
        Func<int> getDaysInQuarter = () => (date.Value.AddMonths(3) - date.Value).Days;
        DateTime endDate;
        switch (toUnit)
        {
            case TimeUnit.Hours:
                switch (Time)
                {
                    case TimeUnit.Hours:
                        return new Duration(TimeUnit.Hours, Units);
                    case TimeUnit.Days:
                        return new Duration(TimeUnit.Hours, Units * 24);
                    case TimeUnit.Weeks:
                        return new Duration(TimeUnit.Hours, Units * 24 * 7);
                    case TimeUnit.Months:
                        endDate = date.Value.AddMonths(Units);
                        return Duration.Hours((endDate - date.Value).TotalHours.ToInt());
                    case TimeUnit.Years:
                        endDate = date.Value.AddYears(Units);
                        return Duration.Hours((endDate - date.Value).TotalHours.ToInt());
                    case TimeUnit.Quarters:
                        endDate = date.Value.AddMonths(Units * 3);
                        return Duration.Hours((endDate - date.Value).TotalHours.ToInt());
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case TimeUnit.Days:
                switch (Time)
                {
                    case TimeUnit.Hours:
                        var days = Units / 24;
                        var remainder = Units % 24;
                        if (remainder > 0)
                        {
                            return new Duration(TimeUnit.Days, days) + new Duration(TimeUnit.Hours, remainder);
                        }

                        return new Duration(TimeUnit.Days, Units / 24);
                    case TimeUnit.Days:
                        return new Duration(TimeUnit.Days, Units);
                    case TimeUnit.Weeks:
                        return new Duration(TimeUnit.Days, Units * 7);
                    case TimeUnit.Months:
                        endDate = date.Value.AddMonths(Units);

                        return Duration.Days((endDate - date.Value).TotalDays.ToInt());
                    case TimeUnit.Years:
                        endDate = date.Value.AddYears(Units);
                        var daysFromYears = (endDate.Year - date.Value.Year) * 12 + endDate.Month - date.Value.Month;
                        return Duration.Days((endDate - date.Value).TotalDays.ToInt());
                    case TimeUnit.Quarters:
                        endDate = date.Value.AddMonths(Units * 3);
                        var daysFromQuarters = (endDate.Year - date.Value.Year) * 12 + endDate.Month - date.Value.Month;
                        return Duration.Days((endDate - date.Value).TotalDays.ToInt());
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case TimeUnit.Weeks:
                switch (Time)
                {
                    case TimeUnit.Hours:
                        var weeks = Units / (24 * 7);
                        var remainder = Units % (24 * 7);
                        if (remainder > 0)
                        {
                            return new Duration(TimeUnit.Weeks, weeks) + new Duration(TimeUnit.Hours, remainder);
                        }

                        return new Duration(TimeUnit.Weeks, weeks);
                    case TimeUnit.Days:
                        var remainderDays = Units % 7;
                        if (remainderDays > 0)
                        {
                            return new Duration(TimeUnit.Weeks, Units / 7) + new Duration(TimeUnit.Days, remainderDays);
                        }

                        return new Duration(TimeUnit.Weeks, Units / 7);
                    case TimeUnit.Weeks:
                        return new Duration(TimeUnit.Weeks, Units);
                    case TimeUnit.Months:
                        endDate = date.Value.AddMonths(Units);
                        return Duration.Days((endDate - date.Value).TotalDays.ToInt()).ConvertTo(TimeUnit.Weeks, date);
                    case TimeUnit.Years:
                        endDate = date.Value.AddYears(Units);
                        return Duration.Days((endDate - date.Value).TotalDays.ToInt()).ConvertTo(TimeUnit.Weeks, date);
                    case TimeUnit.Quarters:
                        endDate = date.Value.AddMonths(Units * 3);
                        return Duration.Days((endDate - date.Value).TotalDays.ToInt()).ConvertTo(TimeUnit.Weeks, date);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case TimeUnit.Months:
                switch (Time)
                {
                    case TimeUnit.Hours:
                        endDate = date.Value.AddHours(Units);
                        var months = (endDate.Year - date.Value.Year) * 12 + endDate.Month - date.Value.Month;
                        var remainderHours = (endDate - date.Value.AddMonths(months)).TotalHours.ToInt();
                        if (remainderHours > 0)
                        {
                            return new Duration(TimeUnit.Months, months) + new Duration(TimeUnit.Hours, remainderHours);
                        }
                        return new Duration(TimeUnit.Months, months);
                    case TimeUnit.Days:
                        endDate = date.Value.AddDays(Units);
                        // get the number of months rounded between the start and end date
                        var monthsFromDays = (endDate.Year - date.Value.Year) * 12 + endDate.Month - date.Value.Month;
                        // get the remainder days
                        var remainderDays = (endDate - date.Value.AddMonths(monthsFromDays)).TotalDays.ToInt();
                        if (remainderDays > 0)
                        {
                            return new Duration(TimeUnit.Months, monthsFromDays) +
                                   new Duration(TimeUnit.Days, remainderDays);
                        }

                        return new Duration(TimeUnit.Months, monthsFromDays);
                    case TimeUnit.Weeks:
                        endDate = date.Value.AddDays(Units * 7);
                        // get the number of months rounded between the start and end date
                        var monthsFromWeeks = (endDate.Year - date.Value.Year) * 12 + endDate.Month - date.Value.Month;
                        var remainderDays1 = (endDate - date.Value.AddMonths(monthsFromWeeks)).TotalDays.ToInt();
                        var remainderWeeks = Duration.Days(remainderDays1).ConvertTo(TimeUnit.Weeks);


                        if (remainderDays1 > 0)
                        {
                            return new Duration(TimeUnit.Months, monthsFromWeeks) +  remainderWeeks;
                        }

                        return new Duration(TimeUnit.Months, monthsFromWeeks);
                    case TimeUnit.Months:
                        return new Duration(TimeUnit.Months, Units);
                    case TimeUnit.Years:
                        return new Duration(TimeUnit.Months, Units * 12);
                    case TimeUnit.Quarters:
                        return new Duration(TimeUnit.Months, Units * 3);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case TimeUnit.Years:
                switch (Time)
                {
                    case TimeUnit.Hours:
                        endDate = date.Value.AddHours(Units);
                        var yearsFromHours = (endDate.Year - date.Value.Year);
                        var remainderHours = (endDate - date.Value.AddYears(yearsFromHours)).TotalHours.ToInt();

                        if (remainderHours > 0)
                        {
                            return new Duration(TimeUnit.Years, yearsFromHours) +
                                   new Duration(TimeUnit.Hours, remainderHours);
                        }

                        return new Duration(TimeUnit.Years, yearsFromHours);
                    case TimeUnit.Days:
                        endDate = date.Value.AddDays(Units);
                        var yearsFromDays = (endDate.Year - date.Value.Year);
                        var remainderDays = (endDate - date.Value.AddYears(yearsFromDays)).TotalDays.ToInt();

                        if (remainderDays > 0)
                        {
                            return new Duration(TimeUnit.Years, yearsFromDays) +
                                   new Duration(TimeUnit.Days, remainderDays);
                        }

                        return new Duration(TimeUnit.Years, yearsFromDays);
                    case TimeUnit.Weeks:
                        endDate = date.Value.AddDays(Units * 7);
                        var yearsFromWeeks = (endDate.Year - date.Value.Year);
                        var endOfYears1 = date.Value.AddYears(yearsFromWeeks);
                        
                        var remainderWeeks = Duration.Days((endDate - endOfYears1).TotalDays.ToInt()).ConvertTo(TimeUnit.Weeks, endOfYears1);

                        if ((endDate - endOfYears1).TotalDays.ToInt() > 0)
                        {
                            return new Duration(TimeUnit.Years, yearsFromWeeks) + remainderWeeks;
                        }

                        return new Duration(TimeUnit.Years, yearsFromWeeks);
                    case TimeUnit.Months:
                        endDate = date.Value.AddMonths(Units);
                        
                        var yearsFromMonths = (endDate.Year - date.Value.Year);
                        var endOfYears = date.Value.AddYears(yearsFromMonths);
                        
                        
                        var remainderMonths = Duration.Days((endDate -  endOfYears).TotalDays.ToInt()).ConvertTo(TimeUnit.Months, endOfYears);

                        if ((endDate -  endOfYears).TotalDays.ToInt() > 0)
                        {
                            return new Duration(TimeUnit.Years, yearsFromMonths) + remainderMonths;
                        }

                        return new Duration(TimeUnit.Years, yearsFromMonths);
                    case TimeUnit.Quarters:
                        var yearsFromQuarters = Units / 4;
                        var remainderQuarters = Units % 4;
                        if (remainderQuarters > 0)
                        {
                            return new Duration(TimeUnit.Years, yearsFromQuarters) +
                                   new Duration(TimeUnit.Quarters, remainderQuarters);
                        }

                        return new Duration(TimeUnit.Years, yearsFromQuarters);
                    case TimeUnit.Years:
                        return new Duration(TimeUnit.Years, Units);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case TimeUnit.Quarters:
                switch (Time)
                {
                    case TimeUnit.Hours:
                        endDate = date.Value.AddHours(Units);
                        var quartersFromHours = (endDate.Year - date.Value.Year) * 4 +
                                                (endDate.Month - date.Value.Month) / 3;
                        var remainderHours = (endDate - date.Value.AddMonths(quartersFromHours * 3)).TotalHours.ToInt();

                        if (remainderHours > 0)
                        {
                            return new Duration(TimeUnit.Quarters, quartersFromHours) +
                                   new Duration(TimeUnit.Hours, remainderHours);
                        }

                        return new Duration(TimeUnit.Quarters, quartersFromHours);
                    case TimeUnit.Days:
                        endDate = date.Value.AddDays(Units);
                        var quartersFromDays = (endDate.Year - date.Value.Year) * 4 +
                                               (endDate.Month - date.Value.Month) / 3;
                        var remainderDays = (endDate - date.Value.AddMonths(quartersFromDays * 3)).TotalDays.ToInt();


                        if (remainderDays > 0)
                        {
                            return new Duration(TimeUnit.Quarters, quartersFromDays) +
                                   new Duration(TimeUnit.Days, remainderDays);
                        }

                        return new Duration(TimeUnit.Quarters, quartersFromDays);
                    case TimeUnit.Weeks:
                        endDate = date.Value.AddDays(Units * 7);
                        var quartersFromWeeks = (endDate.Year - date.Value.Year) * 4 +
                                                (endDate.Month - date.Value.Month) / 3;
                        var remainderWeeks = (endDate - date.Value.AddMonths(quartersFromWeeks * 3)).TotalDays.ToInt() /
                                             7;

                        if (remainderWeeks > 0)
                        {
                            return new Duration(TimeUnit.Quarters, quartersFromWeeks) +
                                   new Duration(TimeUnit.Weeks, remainderWeeks);
                        }

                        return new Duration(TimeUnit.Quarters, quartersFromWeeks);
                    case TimeUnit.Months:
                        endDate = date.Value.AddMonths(Units);
                        var quartersFromMonths = (endDate.Year - date.Value.Year) * 4 +
                                                 (endDate.Month - date.Value.Month) / 3;
                        
                        var endOfQuarter = date.Value.AddMonths(quartersFromMonths * 3);
                        var  remainderMonths  = Duration.Days((endDate - endOfQuarter).TotalDays.ToInt()).ConvertTo( TimeUnit.Months, endOfQuarter);
                        

                        if ((endDate - endOfQuarter).TotalDays.ToInt() > 0)
                        {
                            return new Duration(TimeUnit.Quarters, quartersFromMonths) + remainderMonths;
                        }

                        return new Duration(TimeUnit.Quarters, quartersFromMonths);
                    case TimeUnit.Years:
                        return new Duration(TimeUnit.Quarters, Units * 4);
                    case TimeUnit.Quarters:
                        return new Duration(TimeUnit.Quarters, Units);
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public class DistanceConversionService
{
    public static Dictionary<(DistanceUnit, DistanceUnit), Func<decimal, decimal>> conversionFunctions =
        new Dictionary<(DistanceUnit, DistanceUnit), Func<decimal, decimal>>
        {
// Conversions from Miles
            { (DistanceUnit.Miles, DistanceUnit.Kilometers), miles => miles * 1.609344m },
            { (DistanceUnit.Miles, DistanceUnit.Meters), miles => miles * 1609.344m },
            { (DistanceUnit.Miles, DistanceUnit.Centimeters), miles => miles * 160934.4m },
            { (DistanceUnit.Miles, DistanceUnit.Millimeters), miles => miles * 1609344m },
            { (DistanceUnit.Miles, DistanceUnit.Yards), miles => miles * 1760m },
            { (DistanceUnit.Miles, DistanceUnit.Feet), miles => miles * 5280m },
            { (DistanceUnit.Miles, DistanceUnit.Inches), miles => miles * 63360m },

// Conversions from Kilometers
            { (DistanceUnit.Kilometers, DistanceUnit.Miles), km => km / 1.60934m },
            { (DistanceUnit.Kilometers, DistanceUnit.Meters), km => km * 1000m },
            { (DistanceUnit.Kilometers, DistanceUnit.Centimeters), km => km * 100000m },
            { (DistanceUnit.Kilometers, DistanceUnit.Millimeters), km => km * 1000000m },
            { (DistanceUnit.Kilometers, DistanceUnit.Yards), km => km * 1093.61m },
            { (DistanceUnit.Kilometers, DistanceUnit.Feet), km => km * 3280.84m },
            { (DistanceUnit.Kilometers, DistanceUnit.Inches), km => km * 39370.07874016m },

// Conversions from Meters
            { (DistanceUnit.Meters, DistanceUnit.Miles), m => m / 1609.34m },
            { (DistanceUnit.Meters, DistanceUnit.Kilometers), m => m / 1000m },
            { (DistanceUnit.Meters, DistanceUnit.Centimeters), m => m * 100m },
            { (DistanceUnit.Meters, DistanceUnit.Millimeters), m => m * 1000m },
            { (DistanceUnit.Meters, DistanceUnit.Yards), m => m * 1.09361m },
            { (DistanceUnit.Meters, DistanceUnit.Feet), m => m * 3.28084m },
            { (DistanceUnit.Meters, DistanceUnit.Inches), m => m * 39.3701m },

// Conversions from Centimeters
            { (DistanceUnit.Centimeters, DistanceUnit.Miles), cm => cm / 160934m },
            { (DistanceUnit.Centimeters, DistanceUnit.Kilometers), cm => cm / 100000m },
            { (DistanceUnit.Centimeters, DistanceUnit.Meters), cm => cm / 100m },
            { (DistanceUnit.Centimeters, DistanceUnit.Millimeters), cm => cm * 10m },
            { (DistanceUnit.Centimeters, DistanceUnit.Yards), cm => cm * 0.0109361m },
            { (DistanceUnit.Centimeters, DistanceUnit.Feet), cm => cm * 0.0328084m },
            { (DistanceUnit.Centimeters, DistanceUnit.Inches), cm => cm * 0.393701m },

// Conversions from Millimeters
            { (DistanceUnit.Millimeters, DistanceUnit.Miles), mm => mm / 1609344m },
            { (DistanceUnit.Millimeters, DistanceUnit.Kilometers), mm => mm / 1000000m },
            { (DistanceUnit.Millimeters, DistanceUnit.Meters), mm => mm / 1000m },
            { (DistanceUnit.Millimeters, DistanceUnit.Centimeters), mm => mm / 10m },
            { (DistanceUnit.Millimeters, DistanceUnit.Yards), mm => mm * 0.00109361m },
            { (DistanceUnit.Millimeters, DistanceUnit.Feet), mm => mm * 0.00328084m },
            { (DistanceUnit.Millimeters, DistanceUnit.Inches), mm => mm * 0.0393701m },

// Conversions from Yards
            { (DistanceUnit.Yards, DistanceUnit.Miles), yd => yd / 1760m },
            { (DistanceUnit.Yards, DistanceUnit.Kilometers), yd => yd / 1093.61m },
            { (DistanceUnit.Yards, DistanceUnit.Meters), yd => yd / 1.09361m },
            { (DistanceUnit.Yards, DistanceUnit.Centimeters), yd => yd * 91.44m },
            { (DistanceUnit.Yards, DistanceUnit.Millimeters), yd => yd * 914.4m },
            { (DistanceUnit.Yards, DistanceUnit.Feet), yd => yd * 3m },
            { (DistanceUnit.Yards, DistanceUnit.Inches), yd => yd * 36m },

// Conversions from Feet
            { (DistanceUnit.Feet, DistanceUnit.Miles), ft => ft / 5280m },
            { (DistanceUnit.Feet, DistanceUnit.Kilometers), ft => ft / 3280.84m },
            { (DistanceUnit.Feet, DistanceUnit.Meters), ft => ft / 3.28084m },
            { (DistanceUnit.Feet, DistanceUnit.Centimeters), ft => ft * 30.48m },
            { (DistanceUnit.Feet, DistanceUnit.Millimeters), ft => ft * 304.8m },
            { (DistanceUnit.Feet, DistanceUnit.Yards), ft => ft / 3m },
            { (DistanceUnit.Feet, DistanceUnit.Inches), ft => ft * 12m },

// Conversions from Inches
            { (DistanceUnit.Inches, DistanceUnit.Miles), inch => inch / 63360m },
            { (DistanceUnit.Inches, DistanceUnit.Kilometers), inch => inch / 39370.1m },
            { (DistanceUnit.Inches, DistanceUnit.Meters), inch => inch / 39.3701m },
            { (DistanceUnit.Inches, DistanceUnit.Centimeters), inch => inch * 2.54m },
            { (DistanceUnit.Inches, DistanceUnit.Millimeters), inch => inch * 25.4m },
            { (DistanceUnit.Inches, DistanceUnit.Yards), inch => inch / 36m },
            { (DistanceUnit.Inches, DistanceUnit.Feet), inch => inch / 12m }
        };


    public static decimal ConvertDistance(decimal value, DistanceUnit fromUnit, DistanceUnit toUnit)
    {
        if (fromUnit == toUnit)
        {
            return value;
        }

        var conversionKey = (fromUnit, toUnit);
        if (!conversionFunctions.ContainsKey(conversionKey))
        {
            throw new ArgumentException($"Conversion from {fromUnit} to {toUnit} is not supported.");
        }

        var conversionFunction = conversionFunctions[conversionKey];
        return conversionFunction(value);
    }
}
using PrimativeExtensions;
namespace ProjectManager.Measurements;

public enum TimeUnit
{
    Hours,
    Days,
    Weeks,
    Months,
    Quarters,
    Years
}

public struct Duration
{
    private IList<Duration> otherDurations;
    public TimeUnit Time { get; set; }
    public int Units { get; set; }

    // equals
    public override bool Equals(object? obj)
    {
        if (obj is Duration duration)
        {
            return duration.Time == Time && duration.Units == Units;
        }

        return false;
    }
    
    public static bool operator ==(Duration left, Duration right)
    {
        if (ReferenceEquals(left, null))
        {
            return ReferenceEquals(right, null);
        }
        return left.Equals(right);
    }

    public static bool operator !=(Duration left, Duration right)
    {
        return !(left == right);
    }
    
    
    public IEnumerable<Duration> OtherDurations
    {
        get
        {
            var orderedTimeUnits = new List<TimeUnit>
            {
                TimeUnit.Years,
                TimeUnit.Quarters,
                TimeUnit.Months,
                TimeUnit.Weeks,
                TimeUnit.Days,
                TimeUnit.Hours
            };
            foreach (var timeUnit in orderedTimeUnits)
            {
                if (otherDurations.Any(x => x.Time == timeUnit))
                {
                    yield return otherDurations.First(x => x.Time == timeUnit);
                }
            }
            
        }
    }


    public IList<Duration> GetDurationsGrouped()
    {
        var durations = new List<Duration>();
        var orderedTimeUnits = new List<TimeUnit>
        {
            TimeUnit.Years,
            TimeUnit.Quarters,
            TimeUnit.Months,
            TimeUnit.Weeks,
            TimeUnit.Days,
            TimeUnit.Hours
        };
        
        var subAndThis = OtherDurations.Concat(this);
        
        //var subAndThis = SubDurations.Concat(this).ToList() ;
        foreach(var unit in orderedTimeUnits)
        {
            if (subAndThis.Any(x => x.Time == unit))
            {
                var unitSum = subAndThis.Where(x => x.Time == unit).Sum(x => x.Units);
                durations.Add(new Duration(unit, unitSum));
            }
        }
        
        
        return durations;
    }

    public Duration AddDuration (Duration duration)
    {

        if (duration.Time == Time)
        {
            this.Units += duration.Units;
        }

        foreach (var otherDuration in duration.OtherDurations)
        {
            AddDuration(otherDuration);
        }

        for (int i = 0; i < otherDurations.Count; i++)
        {
            if (otherDurations[i].Time == duration.Time)
            {
                otherDurations[i] = new Duration(duration.Time, otherDurations[i].Units + duration.Units);
            }
        }
        if (!otherDurations.Any(x => x.Time == duration.Time) && duration.Time != Time)
        {
            otherDurations.Add(new Duration(duration.Time,  duration.Units));
            return this;
        }

        return this;
    }
    
    public Duration(TimeUnit time, int units)
    {
        Time = time;
        Units = units;
        otherDurations = new List<Duration>();
    }

    public DateTime GetDate(DateTime startDate)
    {
        foreach (var subDuration in OtherDurations)
        {
            startDate = subDuration.GetDate(startDate);
        }
        
        switch (Time)
        {
            case TimeUnit.Hours:
                return startDate.AddHours(Units);
            case TimeUnit.Days:
                return startDate.AddDays(Units);
            case TimeUnit.Weeks:
                return startDate.AddDays(Units * 7);
            case TimeUnit.Months:
                return startDate.AddMonths(Units);
            case TimeUnit.Years:
                return startDate.AddYears(Units);
            case TimeUnit.Quarters:
                return startDate.AddMonths(Units * 3);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    
    
    public override string ToString()
    {
        var durations = GetDurationsGrouped();
        var result = "";
        foreach (var duration in durations)
        {
            string timeUnitText = duration.Time.ToString();
            if (duration.Units == 1)
            {
                // Remove the last "s" for singular form.
                timeUnitText = timeUnitText.TrimEnd('s');
            }
            result += $"{duration.Units} {timeUnitText} ";
        }

        return result.TrimEnd();
    }    
    // Days
    public static Duration Days(int days)
    {
        return new Duration(TimeUnit.Days, days);
    }

    public static Duration Days()
    {
        return new Duration(TimeUnit.Days, 1);
    }

    // Weeks
    public static Duration Weeks(int weeks)
    {
        return new Duration(TimeUnit.Weeks, weeks);
    }
    
    // Months
    public static Duration Months(int months)
    {
        return new Duration(TimeUnit.Months, months);
    }
    
    // Quarters
    public static Duration Quarters(int quarters)
    {
        return new Duration(TimeUnit.Quarters, quarters);
    }
    
    // Years
    public static Duration Years(int years)
    {
        return new Duration(TimeUnit.Years, years);
    }
    
    // Hours
    public static Duration Hours(int hours)
    {
        return new Duration(TimeUnit.Hours, hours);
    }

    public static Duration Hours()
    {
        return new Duration(TimeUnit.Hours, 1);
    }

    public static Duration operator +(Duration duration1, Duration duration2)
    {
        var result = new Duration(duration1.Time, duration1.Units);
        foreach (var otherDuration in duration1.OtherDurations)
        {
                result.AddDuration(otherDuration);
        }
        result.AddDuration(duration2);
        return result;
    }


    public Duration ConvertTo(TimeUnit valueTime, DateTime? date)
    {
        return DurationConversionService.ConvertToTimeUnit(this.Units, this.Time, valueTime, date);
    }
}

public static class DurationExtensions
{
    
    public static Duration AddDays(this Duration duration, int days)
    {
        duration.AddDuration(Duration.Days(days));
        return duration;
    }
    
    public static Duration AddWeeks(this Duration duration, int weeks)
    {
        duration.AddDuration(Duration.Weeks(weeks));
        return duration;
    }
    
    public static Duration AddMonths(this Duration duration, int months)
    {
        duration.AddDuration(Duration.Months(months));
        return duration;
    }
    
    public static Duration AddYears(this Duration duration, int years)
    {
        duration.AddDuration(Duration.Years(years));
        return duration;
    }
    
    public static Duration AddQuarters(this Duration duration, int quarters)
    {
        duration.AddDuration(Duration.Quarters(quarters));
        return duration;
    }
    
    public static Duration AddHours(this Duration duration, int hours)
    {
        duration.AddDuration(Duration.Hours(hours));
        return duration;
    }
    
    
    
}
namespace Measurements;

public struct DateApproximate
{
    public DateTime Earliest { get; set; }
    public DateTime Latest { get; set; }

    public DateApproximate(DateTime earliest, DateTime latest)
    {
        Earliest = earliest;
        Latest = latest;
    }

    public DateApproximate(DateTime date)
    {
        Earliest = date;
        Latest = date;
    }

    // Create an explicit conversion operator from DateTime to DateApproximate
    public static explicit operator DateApproximate(DateTime date) => new DateApproximate(date);
}
public struct DurationApproximate
{
    public Duration Minimum { get; set; }
    public Duration Maximum { get; set; }

    public DurationApproximate()
    {
        Minimum = Duration.Days(0);
        Maximum = Duration.Days(0);
    }

    public DurationApproximate(Duration exact)
    {
        Minimum = exact;
        Maximum = exact;
    }

    public DurationApproximate(Duration minimum, Duration maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }

    public DateApproximate GetDateApproximate(DateTime sourceDate)
    {
        var earliest = Minimum.GetDate(sourceDate);
        var latest = Maximum.GetDate(sourceDate);
        return new DateApproximate
        {
            Earliest = earliest,
            Latest = latest
        };
    }

    public static DurationApproximate From(Duration exact) => new DurationApproximate(exact);

    public static DurationApproximate From(Duration minimum, Duration maximum) =>
        new DurationApproximate(minimum, maximum);

    

    public static DurationApproximate operator +(DurationApproximate a, DurationApproximate b)
    {
        return new DurationApproximate(a.Minimum + b.Minimum, a.Maximum + b.Maximum);
    }

    // add an + operator overload which takes a Duration and returns a TaskDurationApproximate
    public static DurationApproximate operator +(DurationApproximate a, Duration b)
    {
        return new DurationApproximate(a.Minimum + b, a.Maximum + b);
    }
    

    // create an explicit conversion operator from Duration to TaskDurationApproximate
    public static implicit operator DurationApproximate(Duration duration) => new(duration);

    public override bool Equals(object? obj)
    {
        if (obj is DurationApproximate durationApproximate)
        {
            return durationApproximate.Minimum == Minimum && durationApproximate.Maximum == Maximum;
        }
        return base.Equals(obj);
    }
}
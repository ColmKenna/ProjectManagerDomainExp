namespace Measurements;

public struct Distance : IEqualityComparer<Distance>
{
    private readonly List<Distance> otherDistances;

    public DistanceUnit DistanceType { get; }
    public decimal Amount { get; private set; }

    public Distance(DistanceUnit distanceType, decimal amount)
    {
        DistanceType = distanceType;
        Amount = amount;
        otherDistances = new List<Distance>();
    }

    public IEnumerable<Distance> OtherDistances
    {
        get
        {
            var orderedDistanceUnits = new List<DistanceUnit>
            {
                DistanceUnit.Miles,
                DistanceUnit.Yards,
                DistanceUnit.Feet,
                DistanceUnit.Inches,
                DistanceUnit.Kilometers,
                DistanceUnit.Meters,
                DistanceUnit.Centimeters,
                DistanceUnit.Millimeters
            };
            foreach (var distanceUnit in orderedDistanceUnits)
            {
                if (otherDistances.Any(x => x.DistanceType == distanceUnit))
                {
                    yield return otherDistances.First(x => x.DistanceType == distanceUnit);
                }
            }
        }
    }

    public IList<Distance> GetDistancesGrouped()
    {
        var distances = new List<Distance>();
        var orderedDistanceUnits = new List<DistanceUnit>
        {
            DistanceUnit.Miles,
            DistanceUnit.Yards,
            DistanceUnit.Feet,
            DistanceUnit.Inches,
            DistanceUnit.Kilometers,
            DistanceUnit.Meters,
            DistanceUnit.Centimeters,
            DistanceUnit.Millimeters
        };

        var subAndThis = OtherDistances.Concat(new[] { this });

        foreach (var unit in orderedDistanceUnits)
        {
            if (subAndThis.Any(x => x.DistanceType == unit))
            {
                var unitSum = subAndThis.Where(x => x.DistanceType == unit).Sum(x => x.Amount);
                distances.Add(new Distance(unit, unitSum));
            }
        }

        return distances;
    }

    public Distance AddDistance(Distance distance)
    {
        if (distance.DistanceType == DistanceType)
        {
            Amount += distance.Amount;
        }
        
        

        foreach (var otherDistance in distance.OtherDistances)
        {
            AddDistance(otherDistance);
        }

        for (int i = 0; i < otherDistances.Count; i++)
        {
            if (otherDistances[i].DistanceType == distance.DistanceType)
            {
                otherDistances[i] = new Distance(distance.DistanceType, otherDistances[i].Amount + distance.Amount);
            }
        }
        if (!otherDistances.Any(x => x.DistanceType == distance.DistanceType) && distance.DistanceType != DistanceType)
        {
            otherDistances.Add(new Distance(distance.DistanceType, distance.Amount));
        }

        return this;
    }

    public decimal GetAs(DistanceUnit targetUnit, int rounded = 3)
    {
        var total = OtherDistances.Sum(x => x.GetAs(targetUnit));

        
        if (DistanceType == targetUnit)
        {
            return total + Amount;
        }
        return total + DistanceConversionService.ConvertDistance(Amount, DistanceType, targetUnit);
    }
    
    public Distance ConvertTo(DistanceUnit targetUnit)
    {
        return new Distance(targetUnit, GetAs(targetUnit));
    }

    public override string ToString()
    {
        var distances = GetDistancesGrouped();
        var result = "";
        foreach (var distance in distances)
        {
            string distanceUnitText = distance.DistanceType.ToString();
            if (distance.Amount == 1)
            {
                // Remove the last "s" for singular form.
                distanceUnitText = distanceUnitText.TrimEnd('s');
            }
            result += $"{distance.Amount} {distanceUnitText} ";
        }

        return result.TrimEnd();
    }

    public static Distance operator +(Distance distance1, Distance distance2)
    {
      
            var result = new Distance(distance1.DistanceType, distance1.Amount);
        foreach (var otherDistance in distance1.OtherDistances)
        {
            result.AddDistance(otherDistance);
        }
        result.AddDistance(distance2);
        return result;
    }

    public static Distance Millimeters(decimal amount)
    {
        return new Distance(DistanceUnit.Millimeters, amount);
    }

    public static Distance Centimeters(decimal amount)
    {
        return new Distance(DistanceUnit.Centimeters, amount);
    }

    public static Distance Meters(decimal amount)
    {
        return new Distance(DistanceUnit.Meters, amount);
    }

    public static Distance Kilometers(decimal amount)
    {
        return new Distance(DistanceUnit.Kilometers, amount);
    }

    public static Distance Inches(decimal amount)
    {
        return new Distance(DistanceUnit.Inches, amount);
    }

    public static Distance Feet(decimal amount)
    {
        return new Distance(DistanceUnit.Feet, amount);
    }

    public static Distance Yards(decimal amount)
    {
        return new Distance(DistanceUnit.Yards, amount);
    }

    public static Distance Miles(decimal amount)
    {
        return new Distance(DistanceUnit.Miles, amount);
    }

    public bool Equals(Distance x, Distance y)
    {
        return x.otherDistances.Equals(y.otherDistances) && x.DistanceType == y.DistanceType && x.Amount == y.Amount;
    }

    public int GetHashCode(Distance obj)
    {
        return HashCode.Combine(obj.otherDistances, (int)obj.DistanceType, obj.Amount);
    }
    
    public static bool operator ==(Distance? left, Distance right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Distance? left, Distance right)
    {
        return !(left == right);
    }
}

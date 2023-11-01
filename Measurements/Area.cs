namespace Measurements;

public struct Area : IEqualityComparer<Area>
{
    private IList<Area> otherAreas;
    public AreaUnit AreaType { get; set; }
    public decimal Amount { get; set; }

    // equals
    public override bool Equals(object? obj)
    {
        if (obj is Area area)
        {
            return area.AreaType == AreaType && area.Amount == Amount;
        }

        return false;
    }

    public IEnumerable<Area> OtherAreas
    {
        get
        {
            var orderedAreaUnits = new List<AreaUnit>
            {
                AreaUnit.SquareMiles,
                AreaUnit.Acres,
                AreaUnit.SquareYards,
                AreaUnit.SquareFeet,
                AreaUnit.SquareInches,
                AreaUnit.SquareKilometers,
                AreaUnit.SquareMeters,
                AreaUnit.SquareCentimeters,
                AreaUnit.SquareMillimeters
            };
            foreach (var areaUnit in orderedAreaUnits)
            {
                if (otherAreas.Any(x => x.AreaType == areaUnit))
                {
                    yield return otherAreas.First(x => x.AreaType == areaUnit);
                }
            }
        }
    }

    public IList<Area> GetAreasGrouped()
    {
        var areas = new List<Area>();
        var orderedAreaUnits = new List<AreaUnit>
        {
            AreaUnit.SquareMiles,
            AreaUnit.Acres,
            AreaUnit.SquareYards,
            AreaUnit.SquareFeet,
            AreaUnit.SquareInches,
            AreaUnit.SquareKilometers,
            AreaUnit.SquareMeters,
            AreaUnit.SquareCentimeters,
            AreaUnit.SquareMillimeters
        };

        var subAndThis = OtherAreas.Concat(new[] { this });

        foreach (var unit in orderedAreaUnits)
        {
            if (subAndThis.Any(x => x.AreaType == unit))
            {
                var unitSum = subAndThis.Where(x => x.AreaType == unit).Sum(x => x.Amount);
                areas.Add(new Area(unit, unitSum));
            }
        }

        return areas;
    }

    public Area AddArea(Area area)
    {
        if (area.AreaType == AreaType)
        {
            this.Amount += area.Amount;
        }

        foreach (var otherArea in area.OtherAreas)
        {
            AddArea(otherArea);
        }

        for (int i = 0; i < otherAreas.Count; i++)
        {
            if (otherAreas[i].AreaType == area.AreaType)
            {
                otherAreas[i] = new Area(area.AreaType, otherAreas[i].Amount + area.Amount);
            }
        }
        if (!otherAreas.Any(x => x.AreaType == area.AreaType) && area.AreaType != AreaType)
        {
            otherAreas.Add(new Area(area.AreaType, area.Amount));
        }

        return this;
    }

    public decimal GetAs(AreaUnit targetUnit, int rounded = 3)
    {
        var total = OtherAreas.Sum(x => x.GetAs(targetUnit));
        
        if (AreaType == targetUnit)
        {
            return total + Amount;
        }
        return total + AreaConversionService.ConvertArea(Amount, AreaType, targetUnit);
    }
    
    public Area ConvertTo(AreaUnit targetUnit)
    {
        return new Area(targetUnit, GetAs(targetUnit));
    }
    
    public override string ToString()
    {
        var areas = GetAreasGrouped();
        var result = "";
        foreach (var area in areas)
        {
            string areaUnitText = area.AreaType.ToString();
            if (area.Amount == 1)
            {
                // Remove the last "s" for singular form.
                areaUnitText = areaUnitText.TrimEnd('s');
            }
            result += $"{area.Amount} {areaUnitText} ";
        }

        return result.TrimEnd();
    }

    public static Area operator +(Area area1, Area area2)
    {
        var result = new Area(area1.AreaType, area1.Amount);
        foreach (var otherArea in area1.OtherAreas)
        {
            result.AddArea(otherArea);
        }
        result.AddArea(area2);
        return result;
    }

    public Area(AreaUnit areaType, decimal amount)
    {
        AreaType = areaType;
        Amount = amount;
        otherAreas = new List<Area>();
    }
    
    public static Area SquareMillimeters(decimal amount)
    {
        return new Area(AreaUnit.SquareMillimeters, amount);
    }

    public static Area SquareCentimeters(decimal amount)
    {
        return new Area(AreaUnit.SquareCentimeters, amount);
    }

    public static Area SquareMeters(decimal amount)
    {
        return new Area(AreaUnit.SquareMeters, amount);
    }

    public static Area SquareKilometers(decimal amount)
    {
        return new Area(AreaUnit.SquareKilometers, amount);
    }

    public static Area SquareInches(decimal amount)
    {
        return new Area(AreaUnit.SquareInches, amount);
    }

    public static Area SquareFeet(decimal amount)
    {
        return new Area(AreaUnit.SquareFeet, amount);
    }

    public static Area SquareYards(decimal amount)
    {
        return new Area(AreaUnit.SquareYards, amount);
    }

    public static Area Acres(decimal amount)
    {
        return new Area(AreaUnit.Acres, amount);
    }

    public static Area SquareMiles(decimal amount)
    {
        return new Area(AreaUnit.SquareMiles, amount);
    }

    public bool Equals(Area x, Area y)
    {
        return x.otherAreas.Equals(y.otherAreas) && x.AreaType == y.AreaType && x.Amount == y.Amount;
    }

    public int GetHashCode(Area obj)
    {
        return HashCode.Combine(obj.otherAreas, (int)obj.AreaType, obj.Amount);
    }
    
    public static bool operator ==(Area? left, Area right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(Area? left, Area right)
    {
        return !(left == right);
    }
}
namespace Measurements;

public struct Volume : IEqualityComparer<Volume>
{
    private IList<Volume> otherVolumes;
    public VolumeUnit VolumeType { get; set; }
    public decimal Amount { get; set; }

    // equals
    public override bool Equals(object? obj)
    {
        if (obj is Volume volume)
        {
            return volume.VolumeType == VolumeType && volume.Amount == Amount;
        }

        return false;
    }

    public IEnumerable<Volume> OtherVolumes
    {
        get
        {
            var orderedVolumeUnits = new List<VolumeUnit>
            {
                VolumeUnit.CubicMeters,
                VolumeUnit.CubicYards,
                VolumeUnit.CubicFeet,
                VolumeUnit.Gallons,
                VolumeUnit.Liters,
                VolumeUnit.Milliliters,
                VolumeUnit.CubicCentimeters,
                VolumeUnit.CubicInches
            };
            foreach (var volumeUnit in orderedVolumeUnits)
            {
                if (otherVolumes.Any(x => x.VolumeType == volumeUnit))
                {
                    yield return otherVolumes.First(x => x.VolumeType == volumeUnit);
                }
            }
        }
    }

    public IList<Volume> GetVolumesGrouped()
    {
        var volumes = new List<Volume>();
        var orderedVolumeUnits = new List<VolumeUnit>
        {
            VolumeUnit.CubicMeters,
            VolumeUnit.CubicYards,
            VolumeUnit.CubicFeet,
            VolumeUnit.Gallons,
            VolumeUnit.Liters,
            VolumeUnit.Milliliters,
            VolumeUnit.CubicCentimeters,
            VolumeUnit.CubicInches
        };

        var subAndThis = OtherVolumes.Concat(new[] { this });

        foreach (var unit in orderedVolumeUnits)
        {
            if (subAndThis.Any(x => x.VolumeType == unit))
            {
                var unitSum = subAndThis.Where(x => x.VolumeType == unit).Sum(x => x.Amount);
                volumes.Add(new Volume(unit, unitSum));
            }
        }

        return volumes;
    }

    public Volume AddVolume(Volume volume)
    {
        if (volume.VolumeType == VolumeType)
        {
            this.Amount += volume.Amount;
        }

        foreach (var otherVolume in volume.OtherVolumes)
        {
            AddVolume(otherVolume);
        }

        for (int i = 0; i < otherVolumes.Count; i++)
        {
            if (otherVolumes[i].VolumeType == volume.VolumeType)
            {
                otherVolumes[i] = new Volume(volume.VolumeType, otherVolumes[i].Amount + volume.Amount);
            }
        }
        if (!otherVolumes.Any(x => x.VolumeType == volume.VolumeType) && volume.VolumeType != VolumeType)
        {
            otherVolumes.Add(new Volume(volume.VolumeType, volume.Amount));
        }

        return this;
    }

    public decimal GetAs(VolumeUnit targetUnit, int rounded = 3)
    {
        var total = OtherVolumes.Sum(x => x.GetAs(targetUnit));

        if (VolumeType == targetUnit)
        {
            return total + Amount;
        }
        return total + VolumeConversionService.ConvertVolume(Amount, VolumeType, targetUnit);
    }
    
    public Volume ConvertTo(VolumeUnit targetUnit)
    {
        return new Volume(targetUnit, GetAs(targetUnit));
    }

    public override string ToString()
    {
        var volumes = GetVolumesGrouped();
        var result = "";
        foreach (var volume in volumes)
        {
            string volumeUnitText = volume.VolumeType.ToString();
            if (volume.Amount == 1)
            {
                // Remove the last "s" for singular form.
                volumeUnitText = volumeUnitText.TrimEnd('s');
            }
            result += $"{volume.Amount} {volumeUnitText} ";
        }

        return result.TrimEnd();
    }

    public static Volume operator +(Volume volume1, Volume volume2)
    {
        var result = new Volume(volume1.VolumeType, volume1.Amount);
        foreach (var otherVolume in volume1.OtherVolumes)
        {
            result.AddVolume(otherVolume);
        }
        result.AddVolume(volume2);
        return result;
    }

    public Volume(VolumeUnit volumeType, decimal amount)
    {
        VolumeType = volumeType;
        Amount = amount;
        otherVolumes = new List<Volume>();
    }

    public static Volume Milliliters(decimal amount)
    {
        return new Volume(VolumeUnit.Milliliters, amount);
    }

    public static Volume Liters(decimal amount)
    {
        return new Volume(VolumeUnit.Liters, amount);
    }

    public static Volume CubicMeters(decimal amount)
    {
        return new Volume(VolumeUnit.CubicMeters, amount);
    }

    public static Volume CubicCentimeters(decimal amount)
    {
        return new Volume(VolumeUnit.CubicCentimeters, amount);
    }

    public static Volume CubicInches(decimal amount)
    {
        return new Volume(VolumeUnit.CubicInches, amount);
    }

    public static Volume CubicFeet(decimal amount)
    {
        return new Volume(VolumeUnit.CubicFeet, amount);
    }

    public static Volume CubicYards(decimal amount)
    {
        return new Volume(VolumeUnit.CubicYards, amount);
    }

    public static Volume CubicYards()
    {
        return new Volume(VolumeUnit.CubicYards, 1);
    }

    public static Volume Gallons(decimal amount)
    {
        return new Volume(VolumeUnit.Gallons, amount);
    }

    public bool Equals(Volume x, Volume y)
    {
        return x.otherVolumes.Equals(y.otherVolumes) && x.VolumeType == y.VolumeType && x.Amount == y.Amount;
    }

    public int GetHashCode(Volume obj)
    {
        return HashCode.Combine(obj.otherVolumes, (int)obj.VolumeType, obj.Amount);
    }
    
    public static bool operator ==(Volume? left, Volume right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(Volume? left, Volume right)
    {
        return !(left == right);
    }
}
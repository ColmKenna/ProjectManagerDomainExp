namespace Measurements;

public struct Weight: IEqualityComparer<Weight>
{
    private IList<Weight> otherWeights;
    public WeightUnit WeightType { get; set; }
    public decimal Amount { get; set; }

   // equals
    public override bool Equals(object? obj)
    {
        if (obj is Weight weight)
        {
            return weight.WeightType == WeightType && weight.Amount == Amount;
        }

        return false;
    }

    public IEnumerable<Weight> OtherWeights
    {
        get
        {
            var orderedWeightUnits = new List<WeightUnit>
            {
                WeightUnit.MetricTons,
                WeightUnit.Stones,
                WeightUnit.Pounds,
                WeightUnit.Ounces,
                WeightUnit.Kilograms,
                WeightUnit.Grams,
                WeightUnit.Milligrams,
                WeightUnit.USTons
            };
            foreach (var weightUnit in orderedWeightUnits)
            {
                if (otherWeights.Any(x => x.WeightType == weightUnit))
                {
                    yield return otherWeights.First(x => x.WeightType == weightUnit);
                }
            }
        }
    }

    public IList<Weight> GetWeightsGrouped()
    {
        var weights = new List<Weight>();
        var orderedWeightUnits = new List<WeightUnit>
        {
            WeightUnit.MetricTons,
            WeightUnit.Stones,
            WeightUnit.Pounds,
            WeightUnit.Ounces,
            WeightUnit.Kilograms,
            WeightUnit.Grams,
            WeightUnit.Milligrams,
            WeightUnit.USTons
        };

        var subAndThis = OtherWeights.Concat(new[] { this });

        foreach (var unit in orderedWeightUnits)
        {
            if (subAndThis.Any(x => x.WeightType == unit))
            {
                var unitSum = subAndThis.Where(x => x.WeightType == unit).Sum(x => x.Amount);
                weights.Add(new Weight(unit, unitSum));
            }
        }

        return weights;
    }

    public Weight AddWeight(Weight weight)
    {
        if (weight.WeightType == WeightType)
        {
            this.Amount += weight.Amount;
        }

        foreach (var otherWeight in weight.OtherWeights)
        {
            AddWeight(otherWeight);
        }

        for (int i = 0; i < otherWeights.Count; i++)
        {
            if (otherWeights[i].WeightType == weight.WeightType)
            {
                otherWeights[i] = new Weight(weight.WeightType, otherWeights[i].Amount + weight.Amount);
            }
        }
        if (!otherWeights.Any(x => x.WeightType == weight.WeightType) && weight.WeightType != WeightType)
        {
            otherWeights.Add(new Weight(weight.WeightType, weight.Amount));
        }

        return this;
    }

    public decimal GetAsAmount(WeightUnit targetUnit, int rounded = 3)
    {
        var total = OtherWeights.Sum(x => x.GetAsAmount(targetUnit));
        
        if (WeightType == targetUnit)
        {
            return total + Amount;
        }
        return total+ WeightConversionService.ConvertWeight(Amount, WeightType, targetUnit);
    }

    public Weight ConvertTo(WeightUnit targetUnit)
    {
        return new Weight(targetUnit, GetAsAmount(targetUnit));
    }
    
    public override string ToString()
    {
        var weights = GetWeightsGrouped();
        var result = "";
        foreach (var weight in weights)
        {
            string weightUnitText = weight.WeightType.ToString();
            if (weight.Amount == 1)
            {
                // Remove the last "s" for singular form.
                weightUnitText = weightUnitText.TrimEnd('s');
            }
            result += $"{weight.Amount} {weightUnitText} ";
        }

        return result.TrimEnd();
    }

    public static Weight operator +(Weight weight1, Weight weight2)
    {
        var result = new Weight(weight1.WeightType, weight1.Amount);
        foreach (var otherWeight in weight1.OtherWeights)
        {
            result.AddWeight(otherWeight);
        }
        result.AddWeight(weight2);
        return result;
    }

    public Weight(WeightUnit weightType, decimal amount)
    {
        WeightType = weightType;
        Amount = amount;
        otherWeights = new List<Weight>();
    }
    
    public static Weight Milligrams(decimal amount)
    {
        return new Weight(WeightUnit.Milligrams, amount);
    }

    public static Weight Grams(decimal amount)
    {
        return new Weight(WeightUnit.Grams, amount);
    }

    public static Weight Kilograms(decimal amount)
    {
        return new Weight(WeightUnit.Kilograms, amount);
    }

    public static Weight MetricTons(decimal amount)
    {
        return new Weight(WeightUnit.MetricTons, amount);
    }

    public static Weight Ounces(decimal amount)
    {
        return new Weight(WeightUnit.Ounces, amount);
    }

    public static Weight Pounds(decimal amount)
    {
        return new Weight(WeightUnit.Pounds, amount);
    }

    public static Weight Stones(decimal amount)
    {
        return new Weight(WeightUnit.Stones, amount);
    }    
    
    public static Weight USTons(decimal amount)
    {
        return new Weight(WeightUnit.USTons, amount);
    }


    public bool Equals(Weight x, Weight y)
    {
        return x.otherWeights.Equals(y.otherWeights) && x.WeightType == y.WeightType && x.Amount == y.Amount;
    }

    public int GetHashCode(Weight obj)
    {
        return HashCode.Combine(obj.otherWeights, (int)obj.WeightType, obj.Amount);
    }
    
    public static bool operator ==(Weight? left, Weight right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Weight? left, Weight right)
    {
        return !(left == right);
    }
}
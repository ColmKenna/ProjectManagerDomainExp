using PrimativeExtensions;

namespace Measurements;

public class Measurement : Either<Distance, Weight, Duration, Volume, Area, int>,
    IEqualityComparer<Measurement>
{
    public class MeasurementType : Either<DistanceUnit, WeightUnit, TimeUnit, VolumeUnit, AreaUnit, int>
    {
        public MeasurementType(DistanceUnit value1) : base(value1)
        {
        }

        public MeasurementType(WeightUnit value2) : base(value2)
        {
        }

        public MeasurementType(TimeUnit value3) : base(value3)
        {
        }

        public MeasurementType(VolumeUnit value4) : base(value4)
        {
        }

        public MeasurementType(AreaUnit value5) : base(value5)
        {
        }

        public MeasurementType(int value6) : base(value6)
        {
        }

        public bool IsDistanceUnit => base.IsType1();
        public bool IsWeightUnit => base.IsType2();
        public bool IsTimeUnit => base.IsType3();
        public bool IsVolumeUnit => base.IsType4();
        public bool IsAreaUnit => base.IsType5();
        public bool IsInt => base.IsType6();

        public override bool Equals(object? obj)
        {
            if (obj is MeasurementType other)
            {
                var current = this;
                return other.Match<bool>(
                    distance => current.IsDistanceUnit && distance == current.Type1,
                    weight => current.IsWeightUnit && weight == current.Type2,
                    duration => current.IsTimeUnit && duration == current.Type3,
                    volume => current.IsVolumeUnit && volume == current.Type4,
                    area => current.IsAreaUnit && area == current.Type5,
                    qty => current.IsType6()
                );
            }

            return false;
        }


        public override int GetHashCode()
        {
            return Match<int>(
                distance => distance.GetHashCode(),
                weight => weight.GetHashCode(),
                duration => duration.GetHashCode(),
                volume => volume.GetHashCode(),
                area => area.GetHashCode(),
                qty => qty.GetHashCode()
            );
        }

        public static bool operator ==(MeasurementType left, MeasurementType right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(MeasurementType left, MeasurementType right)
        {
            return !(left == right);
        }

        public static implicit operator MeasurementType(DistanceUnit value) => new MeasurementType(value);
        public static implicit operator MeasurementType(WeightUnit value) => new MeasurementType(value);
        public static implicit operator MeasurementType(TimeUnit value) => new MeasurementType(value);
        public static implicit operator MeasurementType(VolumeUnit value) => new MeasurementType(value);
        public static implicit operator MeasurementType(AreaUnit value) => new MeasurementType(value);
        public static implicit operator MeasurementType(int value) => new MeasurementType(value);
    }

    public Measurement(Distance value) : base(value)
    {
    }

    public Measurement(Weight value) : base(value)
    {
    }

    public Measurement(Duration value) : base(value)
    {
    }

    public Measurement(Volume value) : base(value)
    {
    }

    public Measurement(Area value) : base(value)
    {
    }

    public Measurement(int value) : base(value)
    {
    }

    public bool IsDistance() => base.IsType1();
    public bool IsWeight() => base.IsType2();
    public bool IsTime() => base.IsType3();
    public bool IsVolume() => base.IsType4();
    public bool IsArea() => base.IsType5();
    public bool IsInt() => base.IsType6();

    public Maybe<Distance> DistanceValue
    {
        get => this;
    }

    public Maybe<Weight> WeightValue
    {
        get => this;
    }

    public Maybe<Duration> DurationValue
    {
        get => this;
    }

    public Maybe<Volume> VolumeValue
    {
        get => this;
    }

    public Maybe<Area> AreaValue
    {
        get => this;
    }

    public Maybe<int> IntValue
    {
        get => this;
    }

    public MeasurementType GetMeasurementType()
    {
        return Match<MeasurementType>(
            distance => new MeasurementType(distance.DistanceType),
            weight => new MeasurementType(weight.WeightType),
            duration => new MeasurementType(duration.Time),
            volume => new MeasurementType(volume.VolumeType),
            area => new MeasurementType(area.AreaType),
            qty => new MeasurementType(1)
        );
    }


    public IEnumerable<Measurement> GetSubMeasurements()
    {
        return Match<IEnumerable<Measurement>>(
            distance => distance.OtherDistances.Select(x => (Measurement)x),
            weight => weight.OtherWeights.Select(x => (Measurement)x),
            duration => duration.OtherDurations.Select(x => (Measurement)x),
            volume => volume.OtherVolumes.Select(x => (Measurement)x),
            area => area.OtherAreas.Select(x => (Measurement)x),
            qty => new List<Measurement>()
        );
    }

    public decimal GetQty()
    {
        return Match(
            distance => distance.Amount,
            weight => weight.Amount,
            duration => duration.Units.ToDecimal(),
            volume => volume.Amount,
            area => area.Amount,
            qty => qty.ToDecimal()
        );
    }

    public Measurement Map(Func<decimal, decimal> func)
    {
        var subMeasurements = GetSubMeasurements().Select(x => x.Map(func));
        var current = Match<Measurement>(
            distance => new Distance(distance.DistanceType, func(distance.Amount)),
            weight => new Weight(weight.WeightType, func(weight.Amount)),
            duration => new Duration(duration.Time, func(duration.Units.ToDecimal()).ToInt()),
            volume => new Volume(volume.VolumeType, func(volume.Amount)),
            area => new Area(area.AreaType, func(area.Amount)),
            qty => func(qty).ToInt()
        );
        return subMeasurements.Aggregate(current, (current1, subMeasurement) => current1 + subMeasurement);
    }

    public Validation<Measurement> GetAs(MeasurementType unitT, DateTime? date = null)
    {
        if (!this.HasSameMeasurementTypeAs(unitT))
        {
            return Validation<Measurement>.Fail($"Cannot convert {this.GetMeasurementType()} to {unitT}");
        }

        return Match<Measurement>(
            distance => distance.ConvertTo(((Maybe<DistanceUnit>)unitT).Value),
            weight => weight.ConvertTo(((Maybe<WeightUnit>)unitT).Value),
            duration =>
            {
                if (date == null && duration.Time.ContainedIn(TimeUnit.Quarters, TimeUnit.Months, TimeUnit.Years))
                {
                    throw new Exception("Date must be provided when converting a duration to a different time unit.");
                }

                var otherDurations = duration.OtherDurations.ToList();
                if (otherDurations.Any(x => x.Time == TimeUnit.Hours))
                {
                    return duration.ConvertTo(TimeUnit.Hours, date);
                }

                if (otherDurations.Any(x => x.Time == TimeUnit.Days))
                {
                    return duration.ConvertTo(TimeUnit.Days, date);
                }

                if (otherDurations.Any(x => x.Time == TimeUnit.Weeks))
                {
                    return duration.ConvertTo(TimeUnit.Weeks, date);
                }

                if (otherDurations.Any(x => x.Time == TimeUnit.Months))
                {
                    return duration.ConvertTo(TimeUnit.Months, date);
                }

                if (otherDurations.Any(x => x.Time == TimeUnit.Quarters))
                {
                    return duration.ConvertTo(TimeUnit.Quarters, date);
                }

                if (otherDurations.Any(x => x.Time == TimeUnit.Years))
                {
                    return duration.ConvertTo(TimeUnit.Years, date);
                }

                return duration.ConvertTo(((Maybe<TimeUnit>)unitT).Value, date);
            },
            volume => volume.ConvertTo(((Maybe<VolumeUnit>)unitT).Value),
            area => area.ConvertTo(((Maybe<AreaUnit>)unitT).Value),
            qty => qty
        );
    }

    public static implicit operator Measurement(Distance value) => new Measurement(value);
    public static implicit operator Measurement(Weight value) => new Measurement(value);
    public static implicit operator Measurement(Duration value) => new Measurement(value);
    public static implicit operator Measurement(Volume value) => new Measurement(value);
    public static implicit operator Measurement(Area value) => new Measurement(value);
    public static implicit operator Measurement(int value) => new Measurement(value);

    // overload operator +
    public static Measurement operator +(Measurement left, Measurement right)
    {
        if (left.GetEitherType() != right.GetEitherType())
        {
            if (right.IsInt())
            {
                return left.Match<Measurement>(
                    distance => new Distance(distance.DistanceType, distance.Amount + right.IntValue.Value),
                    weight => new Weight(weight.WeightType, weight.Amount + right.IntValue.Value),
                    duration => new Duration(duration.Time, duration.Units + right.IntValue.Value),
                    volume => new Volume(volume.VolumeType, volume.Amount + right.IntValue.Value),
                    area => new Area(area.AreaType, area.Amount + right.IntValue.Value),
                    qty => qty + right.IntValue.Value
                );
            }

            if (left.IsInt())
            {
                return right.Match<Measurement>(
                    distance => new Distance(distance.DistanceType, distance.Amount + left.IntValue.Value),
                    weight => new Weight(weight.WeightType, weight.Amount + left.IntValue.Value),
                    duration => new Duration(duration.Time, duration.Units + left.IntValue.Value),
                    volume => new Volume(volume.VolumeType, volume.Amount + left.IntValue.Value),
                    area => new Area(area.AreaType, area.Amount + left.IntValue.Value),
                    qty => qty + left.IntValue.Value
                );
            }

            throw new Exception("Cannot add two different types of measurements.");
        }

        return left.Match<Measurement>(
            distance => distance + right.DistanceValue.Value,
            weight => weight + right.WeightValue.Value,
            duration => duration + right.DurationValue.Value,
            volume => volume + right.VolumeValue.Value,
            area => area + right.AreaValue.Value,
            qty => qty + right.IntValue.Value
        );
    }


    public static Measurement Create(MeasurementType measurementType, decimal Qty)
    {
        return measurementType.Match<Measurement>(
            distance => new Distance(distance, Qty),
            weight => new Weight(weight, Qty),
            duration => new Duration(duration, Qty.ToInt()),
            volume => new Volume(volume, Qty),
            area => new Area(area, Qty),
            qty => new Measurement(Qty.ToInt())
        );
    }


    public static bool operator ==(Measurement? left, Measurement? right)
    {
        if (ReferenceEquals(left, null))
        {
            return ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(Measurement? left, Measurement? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        return Equals((Measurement)this, (Measurement)obj);
    }


    public bool ShallowEquals(Measurement other)
    {
        return ShallowEquals(this, other);
    }

    private static bool ShallowEquals(Measurement x, Measurement y)
    {
        return x.Match(
            distance => distance.DistanceType == y.DistanceValue.Value.DistanceType &&
                        distance.Amount == y.DistanceValue.Value.Amount,
            weight => weight.WeightType == y.WeightValue.Value.WeightType &&
                      weight.Amount == y.WeightValue.Value.Amount,
            duration => duration.Time == y.DurationValue.Value.Time && duration.Units == y.DurationValue.Value.Units,
            volume => volume.VolumeType == y.VolumeValue.Value.VolumeType &&
                      volume.Amount == y.VolumeValue.Value.Amount,
            area => area.AreaType == y.AreaValue.Value.AreaType && area.Amount == y.AreaValue.Value.Amount,
            qty => qty.Equals(y.IntValue.Value)
        );
    }

    public bool Equals(Measurement x, Measurement y)
    {
        var allMeasurementsOnLeft = BreadthFirstMethods.BreadthFirst(x, x => x.GetSubMeasurements()).ToList();
        var allMeasurementsOnRight = BreadthFirstMethods.BreadthFirst(y, y => y.GetSubMeasurements()).ToList();
        if (allMeasurementsOnLeft.Count != allMeasurementsOnRight.Count)
        {
            return false;
        }

        var containsTheSameMeasurements = allMeasurementsOnLeft.All(left =>
            allMeasurementsOnRight.Count(right => right.ShallowEquals(left)) == 1);
        return containsTheSameMeasurements;
    }

    public override int GetHashCode()
    {
        return GetHashCode(this);
    }

    public int GetHashCode(Measurement obj)
    {
        return obj.Match<int>(
            distance => distance.GetHashCode(),
            weight => weight.GetHashCode(),
            duration => duration.GetHashCode(),
            volume => volume.GetHashCode(),
            area => area.GetHashCode(),
            qty => qty.GetHashCode()
        );
    }

    public static Measurement Zero(MeasurementType measurementType)
    {
        return measurementType.Match<Measurement>(
            distance => new Distance(distance, 0),
            weight => new Weight(weight, 0),
            duration => new Duration(duration, 0),
            volume => new Volume(volume, 0),
            area => new Area(area, 0),
            qty => new Measurement(0)
        );
    }

    public Validation<Measurement> GetAsCurrentType()
    {
        return this.GetAs(this.GetMeasurementType());
    }

    public Measurement GetAsCurrentTypeWhenNonTimeType()
    {
        if (this.GetMeasurementType().IsTimeUnit)
        {
            throw new Exception("Cannot get as current type when measurement type is a time type.");
        }

        return this.GetAs(this.GetMeasurementType());
    }


    public Measurement GetAsCurrentType(DateTime date)
    {
        return this.GetAs(this.GetMeasurementType(), date);
    }

    public bool HasSameMeasurementTypeAs(MeasurementType measurementType)
    {
        return measurementType.IsAreaUnit == this.GetMeasurementType().IsAreaUnit
               && measurementType.IsDistanceUnit == this.GetMeasurementType().IsDistanceUnit
               && measurementType.IsTimeUnit == this.GetMeasurementType().IsTimeUnit
               && measurementType.IsVolumeUnit == this.GetMeasurementType().IsVolumeUnit
               && measurementType.IsWeightUnit == this.GetMeasurementType().IsWeightUnit
               && measurementType.IsInt == this.GetMeasurementType().IsInt;
    }

    public bool HasSameMeasurementTypeAs(Measurement measurement)
    {
        return HasSameMeasurementTypeAs(measurement.GetMeasurementType());
    }
}

public static class MeasurementExtensions
{

    public static Validation<IEnumerable<Measurement>> GetInOrderAscending
    (this IEnumerable<Measurement> measurements,
        DateTime? date = null)
    {
        return measurements.GetInOrder(true, date); 
    }

    public static Validation<IEnumerable<Measurement>> GetInOrderDescending
    (this IEnumerable<Measurement> measurements,
        DateTime? date = null)
    {
        return measurements.GetInOrder(false, date);
    }

    public static Validation<IEnumerable<Measurement>> GetInOrder
    (this IEnumerable<Measurement> measurements,
        bool ascending = true,
        DateTime? date = null)
    {
        var enumerable = measurements.ToList();
        if (!enumerable.AllSameMeasurementType())
        {
            return Validation<IEnumerable<Measurement>>.Fail("All measurements must be of the same type.");
        }

        if (enumerable.Count() <= 1)
            return Validation<IEnumerable<Measurement>>.Success(enumerable);

        var first = enumerable.First();

        return first.Match(
            distance =>
            {
                var list = enumerable.Select(x => x.DistanceValue.Value).ToList();
                return  (ascending? list.GetInOrder(): list.GetInOrderDescending()) .Select(x => new Measurement(x)).ToList();
            },
            weight =>
            {
                var list = enumerable.Select(x => x.WeightValue.Value).ToList();
                return  (ascending? list.GetInOrder(): list.GetInOrderDescending()) .Select(x => new Measurement(x)).ToList();
            },
            duration =>
            {
                var list = enumerable.Select(x => x.DurationValue.Value).ToList();
                var timeUnits = list.SelectMany(x => x.GetTimeUnits());
                if (!date.HasValue && timeUnits.Any(x=> x == TimeUnit.Months || x == TimeUnit.Quarters || x == TimeUnit.Years ))
                {
                    return Validation<IEnumerable<Measurement>>.Fail("Date must be provided when sorting durations.");
                }
                return (ascending? list.GetInOrder(date): list.GetInOrderDescending(date)) .Select(x => new Measurement(x)).ToList();
            },
            volume =>
            {
                var list = enumerable.Select(x => x.VolumeValue.Value).ToList();
                return (ascending? list.GetInOrder(): list.GetInOrderDescending()) .Select(x => new Measurement(x)).ToList();
            }, 
            area => { 
                List<Area> list = enumerable.Select(x => x.AreaValue.Value).ToList();
                return (ascending? list.GetInOrder(): list.GetInOrderDescending()) .Select(x => new Measurement(x)).ToList();
            },
            qty =>
            {
                List<int> list = enumerable.Select(x => x.IntValue.Value).ToList();
                return (ascending? list.OrderBy(x => x): list.OrderByDescending(x => x) ) .Select(x => new Measurement(x)).ToList();
            }
        );
    }


    public static bool AllSameMeasurementType(this IEnumerable<Measurement> measurements)
    {
        var enumerable = measurements.ToList();
        if (enumerable.Count() < 2)
        {
            return true;
        }

        var first = enumerable.First();
        return enumerable.Skip(1).All(current => first.HasSameMeasurementTypeAs(current));
    }
}
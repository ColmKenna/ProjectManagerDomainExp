using PrimativeExtensions;

namespace Measurements;

public class Measurement : Either<Distance, Weight, Duration, Volume, Area, int>, IEqualityComparer<Measurement>
{
    public class MeasurementType: Either<DistanceUnit, WeightUnit, TimeUnit, VolumeUnit, AreaUnit, int>
    {
        public MeasurementType(DistanceUnit value1) : base(value1) { }

        public MeasurementType(WeightUnit value2) : base(value2) { }

        public MeasurementType(TimeUnit value3) : base(value3) { }

        public MeasurementType(VolumeUnit value4) : base(value4) { }

        public MeasurementType(AreaUnit value5) : base(value5) { }

        public MeasurementType(int value6) : base(value6) { }

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
                return other.Match<bool>(
                    distance => this.IsType1() && this.Type1 == distance,
                    weight => this.IsType2() && this.Type2 == weight,
                    duration => this.IsType3() && this.Type3 == duration,
                    volume => this.IsType4() && this.Type4 == volume,
                    area => this.IsType5() && this.Type5 == area,
                    qty => this.IsType6() && this.Type6 == qty
                );
            }

            return false;
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
    public Measurement(Distance value) : base(value) { }
    public Measurement(Weight value) : base(value) { }
    public Measurement(Duration value) : base(value) { }
    public Measurement(Volume value) : base(value) { }
    public Measurement(Area value) : base(value) { }
    public Measurement(int value) : base(value) { }

    public bool IsDistance() => base.IsType1();
    public bool IsWeight() => base.IsType2();
    public bool IsTime() =>  base.IsType3();
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
            qty => new MeasurementType(qty)
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
        return Match<Measurement>(
            distance => new Distance(distance.DistanceType, func(distance.Amount)),
            weight => new Weight(weight.WeightType, func(weight.Amount)),
            duration => new Duration(duration.Time, func(duration.Units.ToDecimal()).ToInt()),
            volume => new Volume(volume.VolumeType, func(volume.Amount)),
            area => new Area(area.AreaType, func(area.Amount)),
            qty => func(qty).ToInt()
        );
    }

    public Validation<Measurement> GetAs(MeasurementType unitT, DateTime? date = null)
    {
        if(this.GetMeasurementType() != unitT)
        {
            return Validation<Measurement>.Fail($"Cannot convert {this.GetMeasurementType()} to {unitT}");
        }
        
        return Match<Measurement>(
            distance => distance.ConvertTo(((Maybe<DistanceUnit>)unitT).Value),
            weight => weight.ConvertTo(((Maybe<WeightUnit>)unitT).Value),
            duration => duration.ConvertTo(((Maybe<TimeUnit>)unitT).Value, date),
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
            if(left.IsInt())
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
            qty => new Measurement(qty)
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
    

    public bool Equals(Measurement x, Measurement y)
    {
        return x.Match<bool>(
            distance => distance.Equals(y.DistanceValue.Value),
            weight => weight.Equals(y.WeightValue.Value),
            duration => duration.Equals(y.DurationValue.Value),
            volume => volume.Equals(y.VolumeValue.Value),
            area => area.Equals(y.AreaValue.Value),
            qty => qty.Equals(y.IntValue.Value)
        ); 
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
}
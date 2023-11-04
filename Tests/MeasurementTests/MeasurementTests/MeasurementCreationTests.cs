namespace MeasurementTests.MeasurementTests;

public class MeasurementCreationTests
{
    [Fact]
    public void CanCreateMeasurementFromDuration()
    {
        var measurement = new Measurement(Duration.Days(5));
        var measurementType = measurement.GetMeasurementType();
        Assert.True(measurementType.IsTimeUnit);
        var asMaybe = measurement.DurationValue;
        Assert.True(asMaybe.HasValue);
        Assert.Equal(5, asMaybe.Value.Units);
        Assert.Equal(TimeUnit.Days, asMaybe.Value.Time);
    }
    
    [Fact]
    public void CanCreateMeasurementFromVolume()
    {
        var measurement = new Measurement(Volume.Liters(5));
        var measurementType = measurement.GetMeasurementType();
        Assert.True(measurementType.IsVolumeUnit);
        var asMaybe = measurement.VolumeValue;
        Assert.True(asMaybe.HasValue);
        Assert.Equal(5, asMaybe.Value.Amount);
        Assert.Equal(VolumeUnit.Liters, asMaybe.Value.VolumeType);
    }
    
    [Fact]
    public void CanCreateMeasurementFromArea()
    {
        var measurement = new Measurement(Area.SquareMeters(5));
        var measurementType = measurement.GetMeasurementType();
        Assert.True(measurementType.IsAreaUnit);
        var asMaybe = measurement.AreaValue;
        Assert.True(asMaybe.HasValue);
        Assert.Equal(5, asMaybe.Value.Amount);
        Assert.Equal(AreaUnit.SquareMeters, asMaybe.Value.AreaType);
    }
    
    [Fact]
    public void CanCreateMeasurementFromDistance()
    {
        var measurement = new Measurement(Distance.Meters(5));
        var measurementType = measurement.GetMeasurementType();
        Assert.True(measurementType.IsDistanceUnit);
        var asMaybe = measurement.DistanceValue;
        Assert.True(asMaybe.HasValue);
        Assert.Equal(5, asMaybe.Value.Amount);
        Assert.Equal(DistanceUnit.Meters, asMaybe.Value.DistanceType);
    }
    
    [Fact]
    public void CanCreateMeasurementFromWeight()
    {
        var measurement = new Measurement(Weight.Kilograms(5));
        
        var measurementType = measurement.GetMeasurementType();
        Assert.True(measurementType.IsWeightUnit);
        var asMaybe = measurement.WeightValue;
        Assert.True(asMaybe.HasValue);
        Assert.Equal(5, asMaybe.Value.Amount);
        Assert.Equal(WeightUnit.Kilograms, asMaybe.Value.WeightType);
    }
    
    [Fact]
    public void CanCreateMeasurementFromInt()
    {
        var measurement = new Measurement(5);
        
        var measurementType = measurement.GetMeasurementType();
        Assert.True(measurementType.IsInt);
        var asMaybe = measurement.IntValue;
        Assert.True(asMaybe.HasValue);
        Assert.Equal(5, asMaybe.Value);
    }
    
    
    [Fact]
    public void CanAddTwoMeasurementsOfTheSameType()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var measurement2 = new Measurement(Duration.Days(5));
        var result = measurement1 + measurement2;
        Assert.Equal(10, result.DurationValue.Value.Units);
    }

    
    [Fact]
    public void CannotAddTwoDifferentTypesOfMeasurements()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var measurement2 = new Measurement(Weight.Kilograms(5));
        Assert.Throws<Exception>(() => measurement1 + measurement2);
    }

    [Fact]
    public void CanAddIntToMeasurement()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var result = measurement1 + 5;
        Assert.Equal(10, result.DurationValue.Value.Units);
    }
    
    // can add a measurement to an int
    [Fact]
    public void CanAddMeasurementToInt()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var result = 5 + measurement1;
        Assert.Equal(10, result.DurationValue.Value.Units);
    }
    
    [Fact]
    public void CanGetMeasurementAsSpecificUnit()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var result = measurement1.GetAs(TimeUnit.Hours);
        Assert.True(result.IsValid);
        result.ActionOnSuccess(hours =>
        {
            Assert.Equal(120, hours.DurationValue.Value.Units);
        });
    }

    [Fact]
    public void CannotGetMeasurementAsDifferentType()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var result = measurement1.GetAs(WeightUnit.Kilograms);
        Assert.False(result.IsValid);
        Assert.Equal("Cannot convert from TimeUnit to WeightUnit", result.ErrorMessage);
    }
    
    
}
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
        result.ActionOnSuccess(hours => { Assert.Equal(120, hours.DurationValue.Value.Units); });
    }

    [Fact]
    public void CannotGetMeasurementAsDifferentType()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var result = measurement1.GetAs(WeightUnit.Kilograms);
        Assert.False(result.IsValid);
        Assert.Equal("Cannot convert Days to Kilograms", result.ErrorMessage);
    }
}

public class MeasurementExtensionsTest
{
    // Cant sort array of Measurements when they are different types
    [Fact]
    public void CannotSortMeasurementsWhenDifferentTypes()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var measurement2 = new Measurement(Weight.Kilograms(5));
        var measurement3 = new Measurement(Volume.Liters(5));
        var measurement4 = new Measurement(Area.SquareMeters(5));
        var measurement5 = new Measurement(Distance.Meters(5));
        var measurements = new List<Measurement>
            { measurement1, measurement2, measurement3, measurement4, measurement5 };
        var result = measurements.GetInOrder();

        Assert.False(result.IsValid);
        Assert.Equal("All measurements must be of the same type.", result.ErrorMessage);
    }

    [Fact]
    public void CanSortMeasurementsWhenSameTypes()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var measurement2 = new Measurement(Duration.Days(4));
        var measurement3 = new Measurement(Duration.Days(3));
        var measurement4 = new Measurement(Duration.Days(2));
        var measurement5 = new Measurement(Duration.Days(1));
        var measurements = new List<Measurement>
            { measurement1, measurement2, measurement3, measurement4, measurement5 };
        var result = measurements.GetInOrder();

        Assert.True(result.IsValid);
        result.ActionOnSuccess(measurementsInOrder =>
        {
            var measurementsAsList = measurementsInOrder.ToList();
            Assert.Equal(measurement5, measurementsAsList[0]);
            Assert.Equal(measurement4, measurementsAsList[1]);
            Assert.Equal(measurement3, measurementsAsList[2]);
            Assert.Equal(measurement2, measurementsAsList[3]);
            Assert.Equal(measurement1, measurementsAsList[4]);
        });
    }

    // can sort when same measurement type but different units
    [Fact]
    public void CanSortMeasurementsWhenSameTypeButDifferentUnits()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var measurement2 = new Measurement(Duration.Hours(4));
        var measurement3 = new Measurement(Duration.Months(3));
        var measurement4 = new Measurement(Duration.Days(2));
        var measurement5 = new Measurement(Duration.Months(1));
        var measurements = new List<Measurement>
            { measurement1, measurement2, measurement3, measurement4, measurement5 };
        var result = measurements.GetInOrderAscending(new DateTime(2023, 1, 1));

        Assert.True(result.IsValid);
        result.ActionOnSuccess(measurementsInOrder =>
        {
            var measurementsAsList = measurementsInOrder.ToList();
            Assert.Equal(measurement2, measurementsAsList[0]);
            Assert.Equal(measurement4, measurementsAsList[1]);
            Assert.Equal(measurement1, measurementsAsList[2]);
            Assert.Equal(measurement5, measurementsAsList[3]);
            Assert.Equal(measurement3, measurementsAsList[4]);
        });
    }


    [Fact]
    public void CannotSortMeasurementsWhenMonthsWithoutDate()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var measurement2 = new Measurement(Duration.Hours(4));
        var measurement3 = new Measurement(Duration.Months(3));
        var measurement4 = new Measurement(Duration.Days(2));
        var measurement5 = new Measurement(Duration.Months(1));
        var measurements = new List<Measurement>
            { measurement1, measurement2, measurement3, measurement4, measurement5 };
        var result = measurements.GetInOrderAscending();

        Assert.False(result.IsValid);
        Assert.Equal("Date must be provided when sorting durations.", result.ErrorMessage);
    }

    // as above but for quarter
    [Fact]
    public void CannotSortMeasurementsWhenQuartersWithoutDate()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var measurement2 = new Measurement(Duration.Hours(4));
        var measurement3 = new Measurement(Duration.Quarters(3));
        var measurement4 = new Measurement(Duration.Days(2));
        var measurement5 = new Measurement(Duration.Quarters(1));
        var measurements = new List<Measurement>
            { measurement1, measurement2, measurement3, measurement4, measurement5 };
        var result = measurements.GetInOrderAscending();

        Assert.False(result.IsValid);
        Assert.Equal("Date must be provided when sorting durations.", result.ErrorMessage);
    }

    [Fact]
    public void CannotSortMeasurementsWhenYearsWithoutDate()
    {
        var measurement1 = new Measurement(Duration.Days(5));
        var measurement2 = new Measurement(Duration.Hours(4));
        var measurement3 = new Measurement(Duration.Years(3));
        var measurement4 = new Measurement(Duration.Days(2));
        var measurement5 = new Measurement(Duration.Years(1));
        var measurements = new List<Measurement>
            { measurement1, measurement2, measurement3, measurement4, measurement5 };
        var result = measurements.GetInOrderAscending();

        Assert.False(result.IsValid);
        Assert.Equal("Date must be provided when sorting durations.", result.ErrorMessage);
    }
}
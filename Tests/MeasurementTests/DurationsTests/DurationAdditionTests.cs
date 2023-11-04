namespace MeasurementTests.DurationsTests;

public class DurationAdditionTests
{
    [Fact]
    public void CreateDays()
    {
        var duration1 = Duration.Days(5);

        Assert.Equal(TimeUnit.Days, duration1.Time);
        Assert.Equal(5, duration1.Units);
    }

    [Fact]
    public void CreateWeeks()
    {
        var duration1 = Duration.Weeks(5);

        Assert.Equal(TimeUnit.Weeks, duration1.Time);
        Assert.Equal(5, duration1.Units);
    }

    [Fact]
    public void CreateMonths()
    {
        var duration1 = Duration.Months(5);

        Assert.Equal(TimeUnit.Months, duration1.Time);
        Assert.Equal(5, duration1.Units);
    }

    [Fact]
    public void CreateQuarters()
    {
        var duration1 = Duration.Quarters(5);

        Assert.Equal(TimeUnit.Quarters, duration1.Time);
        Assert.Equal(5, duration1.Units);
    }

    [Fact]
    public void CreateYears()
    {
        var duration1 = Duration.Years(5);

        Assert.Equal(TimeUnit.Years, duration1.Time);
        Assert.Equal(5, duration1.Units);
    }

    [Fact]
    public void CreateHours()
    {
        var duration1 = Duration.Hours(5);

        Assert.Equal(TimeUnit.Hours, duration1.Time);
        Assert.Equal(5, duration1.Units);
    }

    [Fact]
    public void AddDuration_SameUnit()
    {
        var duration1 = Duration.Days(5);
        var duration2 = Duration.Days(10);
        duration1.AddDuration(duration2);
        Assert.Equal(15, duration1.Units);
    }

    [Fact]
    public void AddDuration_DifferentUnits()
    {
        var duration1 = Duration.Hours(1);
        var duration2 = Duration.Days(1);
        duration1.AddDuration(duration2);


        Assert.Equal(25, duration1.ConvertTo(TimeUnit.Hours).Units);
    }

    [Fact]
    public void AddDuration_WithOperator_CreatesNewDuration_AndLeavesOriginalUntouched()
    {
        var duration1 = Duration.Days(5);
        var duration2 = Duration.Days(10);
        var result = duration1 + duration2;
        Assert.Equal(15, result.Units);
        Assert.Equal(5, duration1.Units);
        Assert.Equal(10, duration2.Units);
    }

    [Fact]
    public void ConvertTo_DaysFromMonthsWorksForEachMonth()
    {
        var duration1 = Duration.Months(1);
        var resultJanuary = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 1, 1));
        var resultFebruary = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 2, 1));
        var resultMarch = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 3, 1));
        var resultApril = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 4, 1));
        var resultMay = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 5, 1));
        var resultJune = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 6, 1));
        var resultJuly = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 7, 1));
        var resultAugust = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 8, 1));
        var resultSeptember = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 9, 1));
        var resultOctober = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 10, 1));
        var resultNovember = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 11, 1));
        var resultDecember = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 12, 1));
        var resultFebruaryLeapYear = duration1.ConvertTo(TimeUnit.Days, new DateTime(2020, 2, 1));

        Assert.Equal(31, resultJanuary.Units);
        Assert.Equal(28, resultFebruary.Units);
        Assert.Equal(31, resultMarch.Units);
        Assert.Equal(30, resultApril.Units);
        Assert.Equal(31, resultMay.Units);
        Assert.Equal(30, resultJune.Units);
        Assert.Equal(31, resultJuly.Units);
        Assert.Equal(31, resultAugust.Units);
        Assert.Equal(30, resultSeptember.Units);
        Assert.Equal(31, resultOctober.Units);
        Assert.Equal(30, resultNovember.Units);
        Assert.Equal(31, resultDecember.Units);
        Assert.Equal(29, resultFebruaryLeapYear.Units);
    }


    [Fact]
    public void ConvertTo_DaysFromQuartersWorksForEachMonth()
    {
        var duration1 = Duration.Quarters(1);
        var resultJanuary = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 1, 1));
        var resultFebruary = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 2, 1));
        var resultMarch = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 3, 1));
        var resultApril = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 4, 1));
        var resultMay = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 5, 1));
        var resultJune = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 6, 1));
        var resultJuly = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 7, 1));
        var resultAugust = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 8, 1));
        var resultSeptember = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 9, 1));
        var resultOctober = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 10, 1));
        var resultNovember = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 11, 1));
        var resultDecember = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 12, 1));
        var resultFebruaryLeapYear = duration1.ConvertTo(TimeUnit.Days, new DateTime(2020, 2, 1));

        Assert.Equal(90, resultJanuary.Units);
        Assert.Equal(89, resultFebruary.Units);
        Assert.Equal(92, resultMarch.Units);
        Assert.Equal(91, resultApril.Units);
        Assert.Equal(92, resultMay.Units);
        Assert.Equal(92, resultJune.Units);
        Assert.Equal(92, resultJuly.Units);
        Assert.Equal(92, resultAugust.Units);
        Assert.Equal(91, resultSeptember.Units);
        Assert.Equal(92, resultOctober.Units);
        Assert.Equal(92, resultNovember.Units);
        Assert.Equal(90, resultDecember.Units);
        Assert.Equal(90, resultFebruaryLeapYear.Units);
    }

    [Fact]
    public void ConvertTo_DaysFromYearsWorksForEachMonth()
    {
        var duration1 = Duration.Years(1);
        var resultJanuary = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 1, 1));
        var resultFebruary = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 2, 1));
        var resultMarch = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 3, 1));
        var resultApril = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 4, 1));
        var resultMay = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 5, 1));
        var resultJune = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 6, 1));
        var resultJuly = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 7, 1));
        var resultAugust = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 8, 1));
        var resultSeptember = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 9, 1));
        var resultOctober = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 10, 1));
        var resultNovember = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 11, 1));
        var resultDecember = duration1.ConvertTo(TimeUnit.Days, new DateTime(2021, 12, 1));
        var resultFebruaryLeapYear = duration1.ConvertTo(TimeUnit.Days, new DateTime(2020, 2, 1));

        Assert.Equal(365, resultJanuary.Units);
        Assert.Equal(365, resultFebruary.Units);
        Assert.Equal(365, resultMarch.Units);
        Assert.Equal(365, resultApril.Units);
        Assert.Equal(365, resultMay.Units);
        Assert.Equal(365, resultJune.Units);
        Assert.Equal(365, resultJuly.Units);
        Assert.Equal(365, resultAugust.Units);
        Assert.Equal(365, resultSeptember.Units);
        Assert.Equal(365, resultOctober.Units);
        Assert.Equal(365, resultNovember.Units);
        Assert.Equal(365, resultDecember.Units);
        Assert.Equal(366, resultFebruaryLeapYear.Units);
    }

        [Fact]
    public void ConvertTo_HoursToDays_WithRemainder()
    {
        // Arrange
        var hours = 25; // 1 day and 1 hour
        var duration = Duration.Hours(hours);
        // Act
        var result = duration.ConvertTo(TimeUnit.Days);
        // Assert
        var expected = Duration.Days(1) + Duration.Hours(1);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_HoursToWeeks_WithRemainder()
    {
        // Arrange
        var hours = 169; // 1 week and 1 hour
        var duration = Duration.Hours(hours);
        // Act
        var result = duration.ConvertTo(TimeUnit.Weeks);
        // Assert
        var expected = Duration.Weeks(1) + Duration.Hours(1);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_HoursToMonths_WithRemainderLessThanAMonth()
    {
        // Arrange
        var hours = 700; // Variable, depends on the month
        var duration = Duration.Hours(hours);
        var startDate = new DateTime(2023, 1, 1); // January
        // Act
        var result = duration.ConvertTo(TimeUnit.Months, startDate);
        // Assert
        var expected = Duration.Months(0) + Duration.Hours(700);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_HoursToMonths_WithRemainder()
    {
        // Arrange
        var hours = 700; // Variable, depends on the month
        var duration = Duration.Hours(hours);
        var startDate = new DateTime(2023, 2, 1); 
        // Act
        var result = duration.ConvertTo(TimeUnit.Months, startDate);
        // Assert
        var expected = Duration.Months(1) + Duration.Hours(28); 
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_HoursToYears_WithRemainder()
    {
        // Arrange
        var hours = 9000; // More than 1 year
        var duration = Duration.Hours(hours);
        var startDate = new DateTime(2023, 1, 1);
        // Act
        var result = duration.ConvertTo(TimeUnit.Years, startDate);
        // Assert
        var endDate = startDate.AddHours(hours);
        var expectedYears = endDate.Year - startDate.Year;
        var remainderHours = (endDate - startDate.AddYears(expectedYears)).TotalHours;
        var expected = Duration.Years(expectedYears) + Duration.Hours((int)remainderHours);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_HoursToQuarters_WithRemainder()
    {
        // Arrange
        var hours = 2200; // More than a quarter
        var duration = Duration.Hours(hours);
        var startDate = new DateTime(2023, 1, 1); // Starting from January
        // Act
        var result = duration.ConvertTo(TimeUnit.Quarters, startDate);
        // Assert
        var endDate = startDate.AddHours(hours);
        var expectedQuarters = ((endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month) / 3;
        var remainderHours = (endDate - startDate.AddMonths(expectedQuarters * 3)).TotalHours;
        var expected = Duration.Quarters(expectedQuarters) + Duration.Hours((int)remainderHours);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_DaysToWeeks_WithRemainder()
    {
        // Arrange
        var days = 10; // 1 week and 3 days
        var duration = Duration.Days(days);
        // Act
        var result = duration.ConvertTo(TimeUnit.Weeks);
        // Assert
        var expected = Duration.Weeks(1) + Duration.Days(3);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_DaysToMonths_WithRemainder()
    {
        // Arrange
        var days = 45; // Depends on the starting month
        var duration = Duration.Days(days);
        var startDate = new DateTime(2023, 1, 1); // January has 31 days
        // Act
        var result = duration.ConvertTo(TimeUnit.Months, startDate);
        // Assert
        var expected = Duration.Months(1) + Duration.Days(14); // Remaining days after one month
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_DaysToYears_WithRemainder()
    {
        // Arrange
        var days = 400; // More than 1 year
        var duration = Duration.Days(days);
        var startDate = new DateTime(2023, 1, 1);
        // Act
        var result = duration.ConvertTo(TimeUnit.Years, startDate);
        // Assert
        var expected = Duration.Years(1) + Duration.Days(35); // Remaining days after one year (not a leap year)
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_DaysToQuarters_WithRemainder()
    {
        // Arrange
        var days = 95; // More than a quarter
        var duration = Duration.Days(days);
        var startDate = new DateTime(2023, 1, 1); // Starting from January
        // Act
        var result = duration.ConvertTo(TimeUnit.Quarters, startDate);
        // Assert
        var expected = Duration.Quarters(1) + Duration.Days(5); // Remaining days after one quarter (Jan-Mar has 90 days)
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void ConvertTo_WeeksToMonths_WithRemainder()
    {
        // Arrange
        var weeks = 6; // Variable, depends on the month
        var duration = Duration.Weeks(weeks);
        var startDate = new DateTime(2023, 1, 1); // January
        // Act
        var result = duration.ConvertTo(TimeUnit.Months, startDate);
        // Assert
        // Assuming January for simplicity, 6 weeks is more than a month
        var expectedMonths = 1;
        var expectedDays = (weeks * 7) - 31; // January has 31 days
        var expected = Duration.Months(expectedMonths) + Duration.Days(expectedDays);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_WeeksToYears_WithRemainder()
    {
        // Arrange
        var weeks = 54; // More than 1 year
        var duration = Duration.Weeks(weeks);
        var startDate = new DateTime(2023, 1, 1);
        // Act
        var result = duration.ConvertTo(TimeUnit.Years, startDate);
        // Assert
        // We have to calculate the expected value considering the weeks in a year
        var expectedYears = 1;
        var expectedWeeks = weeks - 52; // Assuming a non-leap year with 52 weeks
        var expected = Duration.Years(expectedYears) + Duration.Weeks(expectedWeeks);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_WeeksToQuarters_WithRemainder()
    {
        // Arrange
        var weeks = 14; // More than a quarter
        var duration = Duration.Weeks(weeks);
        var startDate = new DateTime(2023, 1, 1); // Starting from January
        // Act
        var result = duration.ConvertTo(TimeUnit.Quarters, startDate);
        // Assert
        // Calculate expected value
        var expectedQuarters = 1;
        var expectedWeeks = weeks - 13; // Assuming a quarter is roughly 13 weeks
        var expected = Duration.Quarters(expectedQuarters) + Duration.Weeks(expectedWeeks);
        Assert.Equal(expected, result);
    }
    
        [Fact]
    public void ConvertTo_MonthsToDays_WithVariableDays()
    {
        // Arrange
        var months = 1; // Using January for the test, which has 31 days
        var duration = Duration.Months(months);
        var startDate = new DateTime(2023, 1, 1); // January
        // Act
        var result = duration.ConvertTo(TimeUnit.Days, startDate);
        // Assert
        var expected = Duration.Days(31); // January has 31 days
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_MonthsToWeeks_WithRemainder()
    {
        // Arrange
        var months = 1; // Using January for the test
        var duration = Duration.Months(months);
        var startDate = new DateTime(2023, 1, 1); // January
        // Act
        var result = duration.ConvertTo(TimeUnit.Weeks, startDate);
        // Assert
        var expectedWeeks = 4;
        var expectedDays = 3; // January has 31 days, which is 4 weeks and 3 days
        var expected = Duration.Weeks(expectedWeeks) + Duration.Days(expectedDays);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_MonthsToQuarters_WithRemainder()
    {
        // Arrange
        var months = 5; // More than a quarter
        var duration = Duration.Months(months);
        // Act
        var result = duration.ConvertTo(TimeUnit.Quarters, new DateTime(2023,1,1));
        // Assert
        var expectedQuarters = 1;
        var expectedMonths = 2; // 5 months is 1 quarter and 2 months
        var expected = Duration.Quarters(expectedQuarters) + Duration.Months(expectedMonths);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_MonthsToYears_WithRemainder()
    {
        // Arrange
        var months = 14; // More than a year
        var duration = Duration.Months(months);
        // Act
        var result = duration.ConvertTo(TimeUnit.Years, new DateTime(2023,1,1));
        // Assert
        var expectedYears = 1;
        var expectedMonths = 2; // 14 months is 1 year and 2 months
        var expected = Duration.Years(expectedYears) + Duration.Months(expectedMonths);
        Assert.Equal(expected, result);
    }
    
        [Fact]
    public void ConvertTo_QuartersToDays_WithVariableDays()
    {
        // Arrange
        var quarters = 1; // One quarter can have around 90 to 92 days depending on the months
        var duration = Duration.Quarters(quarters);
        var startDate = new DateTime(2023, 1, 1); // Starting in January
        // Act
        var result = duration.ConvertTo(TimeUnit.Days, startDate);
        // Assert
        // January (31) + February (28) + March (31) = 90 days in Q1 of 2023
        var expected = Duration.Days(90);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_QuartersToWeeks_WithRemainder()
    {
        // Arrange
        var quarters = 1; // One quarter
        var duration = Duration.Quarters(quarters);
        var startDate = new DateTime(2023, 1, 1); // Starting in January
        // Act
        var result = duration.ConvertTo(TimeUnit.Weeks, startDate);
        // Assert
        // 90 days in a quarter gives us 12 weeks and 6 days as remainder
        var expectedWeeks = 12;
        var expectedDays = 6;
        var expected = Duration.Weeks(expectedWeeks) + Duration.Days(expectedDays);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_QuartersToMonths_WithNoRemainder()
    {
        // Arrange
        var quarters = 1; // One quarter is exactly 3 months
        var duration = Duration.Quarters(quarters);
        // Act
        var result = duration.ConvertTo(TimeUnit.Months);
        // Assert
        var expected = Duration.Months(3); // No remainder since a quarter is defined as 3 months
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_QuartersToYears_WithRemainder()
    {
        // Arrange
        var quarters = 5; // More than a year
        var duration = Duration.Quarters(quarters);
        // Act
        var result = duration.ConvertTo(TimeUnit.Years);
        // Assert
        var expectedYears = 1;
        var expectedQuarters = 1; // 5 quarters is 1 year and 1 quarter
        var expected = Duration.Years(expectedYears) + Duration.Quarters(expectedQuarters);
        Assert.Equal(expected, result);
    }
    
        [Fact]
    public void ConvertTo_YearsToDays_NonLeapYear()
    {
        // Arrange
        var years = 1;
        var duration = Duration.Years(years);
        var startDate = new DateTime(2023, 1, 1); // 2023 is not a leap year
        // Act
        var result = duration.ConvertTo(TimeUnit.Days, startDate);
        // Assert
        var expected = Duration.Days(365); // 2023 has 365 days
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_YearsToDays_LeapYear()
    {
        // Arrange
        var years = 1;
        var duration = Duration.Years(years);
        var startDate = new DateTime(2024, 1, 1); // 2024 is a leap year
        // Act
        var result = duration.ConvertTo(TimeUnit.Days, startDate);
        // Assert
        var expected = Duration.Days(366); // 2024 has 366 days
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_YearsToWeeks_WithRemainder()
    {
        // Arrange
        var years = 1; // One year
        var duration = Duration.Years(years);
        var startDate = new DateTime(2023, 1, 1); // Not a leap year
        // Act
        var result = duration.ConvertTo(TimeUnit.Weeks, startDate);
        // Assert
        // 365 days gives us 52 weeks and 1 day remainder
        var expectedWeeks = 52;
        var expectedDays = 1; // One day remaining
        var expected = Duration.Weeks(expectedWeeks) + Duration.Days(expectedDays);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_YearsToMonths_WithNoRemainder()
    {
        // Arrange
        var years = 1; // One year is exactly 12 months
        var duration = Duration.Years(years);
        // Act
        var result = duration.ConvertTo(TimeUnit.Months);
        // Assert
        var expected = Duration.Months(12); // No remainder since a year is defined as 12 months
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertTo_YearsToQuarters_WithNoRemainder()
    {
        // Arrange
        var years = 1; // One year is exactly 4 quarters
        var duration = Duration.Years(years);
        // Act
        var result = duration.ConvertTo(TimeUnit.Quarters);
        // Assert
        var expected = Duration.Quarters(4); // No remainder since a year is defined as 4 quarters
        Assert.Equal(expected, result);
    }


 
}
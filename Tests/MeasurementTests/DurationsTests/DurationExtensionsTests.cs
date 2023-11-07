namespace MeasurementTests.DurationsTests;

public class DurationExtensionsTests
{
    [Fact]
    public void CanOrderWhenAllTheSameDurationUnit()
    {
        var duration1 = Duration.Hours(1);
        var duration2 = Duration.Hours(4);
        var duration3 = Duration.Hours(3);
        var duration4 = Duration.Hours(2);
        var duration5 = Duration.Hours(5);
        

        var durations = new List<Duration> {duration5, duration4, duration3, duration2, duration1};
        var durationsInOrder =  durations.GetInOrder(new DateTime(2023,1,1)).ToList();
        Assert.Equal(duration1, durationsInOrder[0]);
        Assert.Equal(duration4, durationsInOrder[1]);
        Assert.Equal(duration3, durationsInOrder[2]);
        Assert.Equal(duration2, durationsInOrder[3]);
        Assert.Equal(duration5, durationsInOrder[4]);
    }


    [Fact]
    public void CanOrderWhenAllDifferentTimeUnit()
    {
        // TimeUnit.Hours to years
        var duration1 = Duration.Hours(25);
        var duration2 = Duration.Days(1);
        var duration3 = Duration.Weeks(3);
        var duration4 = Duration.Months(2);
        var duration5 = Duration.Quarters(5);
        var duration6 = Duration.Years(2);


        var durations = new List<Duration> { duration5, duration4, duration3, duration2, duration1, duration6 };
        var durationsInorder = durations.GetInOrder(new DateTime(2023, 1, 1)).ToList();
        Assert.Equal(duration2, durationsInorder[0]);
        Assert.Equal(duration1, durationsInorder[1]);
        Assert.Equal(duration3, durationsInorder[2]);
        Assert.Equal(duration4, durationsInorder[3]);
        Assert.Equal(duration5, durationsInorder[4]);
        Assert.Equal(duration6, durationsInorder[5]);
    }
    
    // Same tests as above, but descending
    [Fact]
    public void CanOrderDescendingWhenAllTheSameDurationUnit()
    {
        var duration1 = Duration.Hours(1);
        var duration2 = Duration.Hours(4);
        var duration3 = Duration.Hours(3);
        var duration4 = Duration.Hours(2);
        var duration5 = Duration.Hours(5);

        var durations = new List<Duration> {duration5, duration4, duration3, duration2, duration1};
        var durationsInOrder =  durations.GetInOrderDescending(new DateTime(2023,1,1)).ToList();
        Assert.Equal(duration5, durationsInOrder[0]);
        Assert.Equal(duration2, durationsInOrder[1]);
        Assert.Equal(duration3, durationsInOrder[2]);
        Assert.Equal(duration4, durationsInOrder[3]);
        Assert.Equal(duration1, durationsInOrder[4]);
    }
    
    [Fact]
    public void CanOrderDescendingWhenAllDifferentTimeUnit()
    {
        // TimeUnit.Hours to years
        var duration1 = Duration.Hours(25);
        var duration2 = Duration.Days(1);
        var duration3 = Duration.Weeks(3);
        var duration4 = Duration.Months(2);
        var duration5 = Duration.Quarters(5);
        var duration6 = Duration.Years(2);

        var durations = new List<Duration> { duration5, duration4, duration3, duration2, duration1, duration6 };
        var durationsInorder = durations.GetInOrderDescending(new DateTime(2023, 1, 1)).ToList();
        Assert.Equal(duration6, durationsInorder[0]);
        Assert.Equal(duration5, durationsInorder[1]);
        Assert.Equal(duration4, durationsInorder[2]);
        Assert.Equal(duration3, durationsInorder[3]);
        Assert.Equal(duration1, durationsInorder[4]);
        Assert.Equal(duration2, durationsInorder[5]);
    }

}
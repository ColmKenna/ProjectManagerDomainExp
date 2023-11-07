namespace MeasurementTests.WeightTests;

public class WeightExtensionsTests
{
    [Fact]
    public void CanOrderWhenAllTheSameWeightUnit()
    {
        var weight1 = new Weight(WeightUnit.Kilograms, 1);
        var weight2 = new Weight(WeightUnit.Kilograms, 4);
        var weight3 = new Weight(WeightUnit.Kilograms, 3);
        var weight4 = new Weight(WeightUnit.Kilograms, 2);
        var weight5 = new Weight(WeightUnit.Kilograms, 5);

        var weights = new List<Weight> {weight5, weight4, weight3, weight2, weight1};
        var weightsInOrder =  weights.GetInOrder().ToList();
        Assert.Equal(weight1, weightsInOrder[0]);
        Assert.Equal(weight4, weightsInOrder[1]);
        Assert.Equal(weight3, weightsInOrder[2]);
        Assert.Equal(weight2, weightsInOrder[3]);
        Assert.Equal(weight5, weightsInOrder[4]);
    }
    

    [Fact]
    public void CanOrderWhenAllDifferentWeightUnit()
    {
        var weight1 = new Weight(WeightUnit.Kilograms, 1);
        var weight2 = new Weight(WeightUnit.Grams, 4);
        var weight3 = new Weight(WeightUnit.Milligrams, 3);
        var weight4 = new Weight(WeightUnit.MetricTons, 2);
        var weight5 = new Weight(WeightUnit.Pounds, 5);
        var weight6 = new Weight(WeightUnit.Ounces, 2);

        var weights = new List<Weight> {weight5, weight4, weight3, weight2, weight1, weight6};
        var weightsInOrder = weights.GetInOrder().ToList(); // Assuming GetInOrder is the correct method to call for ascending order
        Assert.Equal(weight3, weightsInOrder[0]);
        Assert.Equal(weight2, weightsInOrder[1]);
        Assert.Equal(weight6, weightsInOrder[2]); // Corrected
        Assert.Equal(weight1, weightsInOrder[3]);
        Assert.Equal(weight5, weightsInOrder[4]); // Corrected
        Assert.Equal(weight4, weightsInOrder[5]);
    }

    
    [Fact]
    public void CanOrderDescendingWhenAllTheSameWeightUnit()
    {
        var weight1 = new Weight(WeightUnit.Kilograms, 1);
        var weight2 = new Weight(WeightUnit.Kilograms, 4);
        var weight3 = new Weight(WeightUnit.Kilograms, 3);
        var weight4 = new Weight(WeightUnit.Kilograms, 2);
        var weight5 = new Weight(WeightUnit.Kilograms, 5);

        var weights = new List<Weight> {weight5, weight4, weight3, weight2, weight1};
        var weightsInOrder =  weights.GetInOrderDescending().ToList();
        Assert.Equal(weight5, weightsInOrder[0]);
        Assert.Equal(weight2, weightsInOrder[1]);
        Assert.Equal(weight3, weightsInOrder[2]);
        Assert.Equal(weight4, weightsInOrder[3]);
        Assert.Equal(weight1, weightsInOrder[4]);
    }
    
    [Fact]
    public void CanOrderDescendingWhenAllDifferentWeightUnit()
    {
        var weight1 = new Weight(WeightUnit.Kilograms, 1);
        var weight2 = new Weight(WeightUnit.Grams, 4);
        var weight3 = new Weight(WeightUnit.Milligrams, 3);
        var weight4 = new Weight(WeightUnit.MetricTons, 2);
        var weight5 = new Weight(WeightUnit.Pounds, 5);
        var weight6 = new Weight(WeightUnit.Ounces, 2);

        var weights = new List<Weight> {weight5, weight4, weight3, weight2, weight1, weight6};
        var weightsInOrder =  weights.GetInOrderDescending().ToList();

        Assert.Equal(weight4, weightsInOrder[0]);
        Assert.Equal(weight5, weightsInOrder[1]);
        Assert.Equal(weight1, weightsInOrder[2]);
        Assert.Equal(weight6, weightsInOrder[3]);
        Assert.Equal(weight2, weightsInOrder[4]);
        Assert.Equal(weight3, weightsInOrder[5]);
    }
}
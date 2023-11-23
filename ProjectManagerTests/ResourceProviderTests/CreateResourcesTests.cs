using ProjectManager;

namespace ProjectManagerTests.ResourceProviderTests;

public class CreateResourcesTests
{
    
    [Fact]
    public void CanCreateResourceWithResourceProvider()
    {
        var resourceProvider = ResourceProvider.Create("Test Provider", "Test Provider Description");
        var resource = resourceProvider.CreateResource("Test Resource", "Test Resource Description");
        Assert.Equal(resourceProvider, resource.ResourceProvider);
    }
    
    [Fact]
    public void CreateResourceCostReturnValidationFailWhenResourceIsNotRelatedToCurrentResourceProvider()
    {
        var resourceProvider = ResourceProvider.Create("Test Provider", "Test Provider Description");
        var resource = Resource.Create("Test Resource", "Test Resource Description", resourceProvider);
        var resourceProvider2 = ResourceProvider.Create("Test Provider 2", "Test Provider Description 2");
        var resourceCost = resourceProvider2.CreateResourceCost(1, resource, 1);
        Assert.False(resourceCost.IsValid);
        Assert.Equal($"Resource is associated with {resourceProvider2.Name} as opposed to {resourceProvider.Name}", resourceCost.ErrorMessage);
    }
    

    
    
}
namespace ProjectManager;

public interface IResourceRepository
{
    IList<Resource> GetResources();
    IList<ResourceCost> GetResourceCosts();
    IList<ResourceProvider> GetResourceProviders();
    IList<Resource> GetResourcesByProvider(ResourceProvider resourceProvider);
    IList<ResourceCost> GetResourceCostsByResource(Resource resource);
    IList<ResourceCost> GetResourceCostsByResourceProvider(ResourceProvider resourceProvider);
    IList<ResourceCost> GetResourceCostsByResourceAndResourceProvider(Resource resource, ResourceProvider resourceProvider);
}
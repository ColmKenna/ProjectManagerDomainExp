using System.Collections;

namespace ProjectManager;

public interface IDealsRepository
{
    public IList<IDeals> GetDealsByResource(Resource resource);
    public IList<IDeals> GetDealsByResourceProvider(ResourceProvider resourceProvider);
    public IList<IDeals> GetDealsByKeyWords(string initialKeyWord, params string[] moreKeyWords);
    public IList<IDeals> GetDealsByResourceProvider(ProjectOwner resourceProvider, DateTime date);
}

public interface IResourceRepository
{
    IList<Resource> GetResources();
    
    IList<Resource> GetResourceByFilter(Func<Resource, bool> filter);
    IList<ResourceCost> GetResourceCosts();
    IList<ResourceProvider> GetResourceProviders();
    IList<Resource> GetResourcesByProvider(ResourceProvider resourceProvider);
    IList<ResourceCost> GetResourceCostsByResource(Resource resource);
    IList<ResourceCost> GetResourceCostsByResourceProvider(ResourceProvider resourceProvider);
}
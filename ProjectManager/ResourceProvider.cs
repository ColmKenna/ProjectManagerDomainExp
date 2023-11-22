using Measurements;
using PrimativeExtensions;

namespace ProjectManager;



public class ResourceProvider
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public static ResourceProvider Create(string name, string description)
    {
        return new ResourceProvider
        {
            Name = name,
            Description = description
        };
    }
    
    
    
    public Resource CreateResource(string resourceName, string resourceDescription)
    {
        return Resource.Create(resourceName, resourceDescription, this);
    }
    
    public Validation< ResourceCost> CreateResourceCost(Measurement quantity, Resource resource, decimal cost)
    {
        if (resource.ResourceProvider != this)
        {
            return Validation< ResourceCost>.Fail("Resource Provider does not match");
            
        }
        
        return ResourceCost.CreateInstance(quantity, resource, cost);
    }
}
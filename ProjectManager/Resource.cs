namespace ProjectManager;

public class Resource
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ResourceProvider ResourceProvider { get; private set; }

    private Resource()
    {
        
    }
    public static Resource Create(string resourceName, string resourceDescription, ResourceProvider resourceProvider)
    {
        return new Resource
        {
            Name = resourceName,
            Description = resourceDescription,
            ResourceProvider = resourceProvider
        };
    }
}
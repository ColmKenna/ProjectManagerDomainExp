namespace ProjectManager;

public class ResourceAssigned
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }

    public static ResourceAssigned Create(string resourceAssignedName, string resourceAssignedDescription, int resourceAssignedQuantity)
    {
        return new ResourceAssigned
        {
            Name = resourceAssignedName,
            Description = resourceAssignedDescription,
            Quantity = resourceAssignedQuantity
        };

    }
}
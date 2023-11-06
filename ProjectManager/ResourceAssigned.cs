using Measurements;

namespace ProjectManager;

public class ResourceAssigned
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Measurement Quantity { get; private set; }

    public static ResourceAssigned Create(string resourceAssignedName, string resourceAssignedDescription, Measurement resourceAssignedQuantity)
    {
        return new ResourceAssigned
        {
            Name = resourceAssignedName,
            Description = resourceAssignedDescription,
            Quantity = resourceAssignedQuantity
        };

    }
}
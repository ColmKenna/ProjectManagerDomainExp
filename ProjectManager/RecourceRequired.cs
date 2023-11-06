using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

public class RecourceRequired
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IList<ResourceAssigned> ResourcesAssigned { get; private set; } = new List<ResourceAssigned>();
    public Measurement Quantity { get; private set; }

    public Measurement GetTotalAssignedQty(Measurement.MeasurementType measurementType)
    {
        return ResourcesAssigned
            .Where(r => r.Quantity.HasSameMeasurementTypeAs(measurementType))
            .Aggregate(Measurement.Zero(measurementType), (current, resourceAssigned) => current + resourceAssigned.Quantity);
    }

    public Measurement GetTotalAssignedQty()
    {
        var measurementType = Quantity.GetMeasurementType();
        
        return GetTotalAssignedQty(measurementType);
    }

    public Measurement GetQuantityRequiredRemaining
    {
        get
        {
            var currentAssignedQty = GetTotalAssignedQty();
            var zero = Measurement.Zero(currentAssignedQty.GetMeasurementType());
            if (currentAssignedQty == zero)
            {
                return Quantity;
            }
            return Quantity + currentAssignedQty.Map((qty) => qty * -1);
        }
    }

    public static RecourceRequired Create(string resourceName, string resourceDescription, Measurement resourceQuantity)
    {
        return new RecourceRequired
        {
            Name = resourceName,
            Description = resourceDescription,
            Quantity = resourceQuantity
        }; 
    }

    public Validation<ResourceAssigned> AddResourceAssigned(ResourceAssigned resourceAssigned)
    {
        if (ResourcesAssigned.Any(r => r.Name.Equals(resourceAssigned.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return Validation<ResourceAssigned>.Fail($"The resource '{resourceAssigned.Name}' is already assigned to this resource required.");
        }
        if (!this.Quantity.HasSameMeasurementTypeAs(resourceAssigned.Quantity.GetMeasurementType()))
        {
            return Validation<ResourceAssigned>.Fail($"The resource '{resourceAssigned.Name}' is not of the same measurement type as the resource required.");
        }
        
        var resourcesAssigned = ResourcesAssigned.ToList();
        resourcesAssigned.Add(resourceAssigned);
        ResourcesAssigned = resourcesAssigned;
        return resourceAssigned;
    }
}
using System.Diagnostics.Metrics;
using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

// Class to represent the Resource Provider
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

}

// Resource
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

// Cost for a Resource
public class ResourceCost 
{
    private ResourceCost(Measurement quantity, Resource resource, decimal cost)
    {
        Quantity = quantity;
        Resource = resource;
        Cost = cost;
    }

    public static ResourceCost CreateInstance(Measurement quantity, Resource resource, decimal cost)
    {
        return new ResourceCost(quantity, resource, cost);
    }

    public int Id { get; private set; }
    public Measurement Quantity { get; private set; }
    public Resource Resource { get; private set; }
    public decimal Cost { get; private set; }
    public Measurement GetAs(Measurement.MeasurementType measurementType, DateTime? onDate = null)
    {
        return Quantity.GetAs(measurementType, onDate);
    }
    
    public decimal GetCostPer(Measurement measurement, DateTime? onDate = null)
    {
        
        return Cost / GetAs(measurement.GetMeasurementType() , onDate).GetQty();
    }

}

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
using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

public class ProjectTask
{
    private ProjectTask(string name, string description, DurationApproximate durationApproximate)
    {
        Name = name;
        Description = description;
        DurationApproximate = durationApproximate;
    }

    public static ProjectTask Create(string name, string description, DurationApproximate durationApproximate)
    {
        return new ProjectTask(name, description, durationApproximate);
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    public IReadOnlyCollection<RecourceRequired> ResourcesRequired { get; private set; } = new List<RecourceRequired>();

    public IReadOnlyCollection<ResourceAssigned> ResourcesAssigned
    {
        get
        {
            return ResourcesRequired
                .SelectMany(r => r.ResourcesAssigned)
                .ToList();
        }
    }

    public DurationApproximate DurationApproximate { get; set; }
    public IReadOnlyCollection<ProjectTask> Dependencies { get; private set; } = new List<ProjectTask>();

    public Validation<RecourceRequired> AddResourcesRequired(RecourceRequired resource)
    {
        if (ResourcesRequired.Any(r => r.Name.Equals(resource.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return Validation<RecourceRequired>.Fail($"The resource '{resource.Name}' is already a requirement for this task.");
        }
        var resourcesRequired = ResourcesRequired.ToList();
        resourcesRequired.Add(resource);
        ResourcesRequired = resourcesRequired;
        return resource; 
    }

    public Validation<ProjectTask> AssignResourceToResourceRequired(string resourceName, ResourceAssigned resourceAssigned)
    {
        // Check that the resource required exists
        if (!ResourcesRequired.Any(r => r.Name.Equals(resourceName, StringComparison.OrdinalIgnoreCase)))
        {
            return Validation<ProjectTask>.Fail($"A resource required with the name '{resourceName}' does not exist in this task.");
        }
        // Get the resource required
        var resourceRequired = ResourcesRequired.First(r => r.Name.Equals(resourceName, StringComparison.OrdinalIgnoreCase));
        // Check that the resource is not already assigned to the resource required
        if (resourceRequired.ResourcesAssigned.Any(r => r.Name.Equals(resourceAssigned.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return Validation<ProjectTask>.Fail($"The resource '{resourceAssigned.Name}' is already assigned to this resource required.");
        }
        // Add the resource to the resource required
        var addResult =  resourceRequired.AddResourceAssigned(resourceAssigned);
        return addResult.Map(x => this); 
    }

    public Validation<ProjectTask> AddDependency(ProjectTask projectTask)
    {
        if(projectTask == this)
        {
            return Validation<ProjectTask>.Fail($"A task cannot be dependent on itself.");
        }
        IEnumerable<ProjectTask> dependancies = PrimativeExtensions.BreadthFirstMethods.BreadthFirst(projectTask, x => x.Dependencies).Skip(1);
        if (dependancies.Any(d => d == this))
        {
            return Validation<ProjectTask>.Fail($"{projectTask.Name} is already a dependency of {this.Name}.");
        }
        
        Dependencies = Dependencies.Append(projectTask).ToList();
        return this;

    }
}

public class RecourceRequired
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IList<ResourceAssigned> ResourcesAssigned { get; private set; } = new List<ResourceAssigned>();
    public int Quantity { get; private set; }

    public int TotalAssignedQty => ResourcesAssigned.Sum(r => r.Quantity);

    public int QuantityRequiredRemaining => Quantity - TotalAssignedQty;

    public static RecourceRequired Create(string resourceName, string resourceDescription, int resourceQuantity)
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
        var resourcesAssigned = ResourcesAssigned.ToList();
        resourcesAssigned.Add(resourceAssigned);
        ResourcesAssigned = resourcesAssigned;
        return resourceAssigned;
    }
}

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
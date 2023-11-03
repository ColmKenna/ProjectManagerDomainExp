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
        InitialStartPoint = DurationApproximate.From(Duration.Days(0));
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

    public Validation<ProjectTask> SetStartPoint(Duration start)
    {
        return this.SetStartPoint(DurationApproximate.From(start));
    }

    public Validation<ProjectTask> SetStartPoint(DurationApproximate approximateStartPoint)
    {
        this.InitialStartPoint = approximateStartPoint;
        return this;
    }

    public DurationApproximate InitialStartPoint { get; private set; }

    public Duration GetEarliestStartPointBasedOnDependancies()
    {
        var currentEarliestStartPoint = Duration.Days(0); 
        foreach (var dependency in Dependencies)
        {
            var dependencyEarliestStartPoint = dependency.GetEarliestStartPointBasedOnDependancies();
            var dependencyDuration = dependency.DurationApproximate.Minimum;
            var dependencyEndPoint = dependencyEarliestStartPoint + dependencyDuration;
            var currentOfDays =  currentEarliestStartPoint.ConvertTo(TimeUnit.Days, new DateTime(2023,1,1)).Units;
            var dependencyOfDays = dependencyEndPoint.ConvertTo(TimeUnit.Days, new DateTime(2023,1,1)).Units;
            if (dependencyOfDays > currentOfDays)
            {
                currentEarliestStartPoint = dependencyEndPoint;
            } 
        }

        if (this.InitialStartPoint.Minimum.ConvertTo(TimeUnit.Days, new DateTime(2023,1,1)).Units > currentEarliestStartPoint.ConvertTo(TimeUnit.Days, new DateTime(2023,1,1)).Units)
        {
            currentEarliestStartPoint = this.InitialStartPoint.Minimum;
        }
        return currentEarliestStartPoint;
    }
     
    public Duration GetLatestStartPointBasedOnDependancies()
    {
        var currentLatestStartPoint = Duration.Days(0);
        foreach (var dependency in Dependencies)
        {
            var dependencyLatestStartPoint = dependency.GetLatestStartPointBasedOnDependancies();
            var dependencyDuration = dependency.DurationApproximate.Maximum;
            var dependencyEndPoint = dependencyLatestStartPoint + dependencyDuration;
            var currentOfDays =  currentLatestStartPoint.ConvertTo(TimeUnit.Days, new DateTime(2023,1,1)).Units;
            var dependencyOfDays = dependencyEndPoint.ConvertTo(TimeUnit.Days, new DateTime(2023,1,1)).Units;
            if (dependencyOfDays > currentOfDays)
            {
                currentLatestStartPoint = dependencyEndPoint;
            } 
        }
        if (this.InitialStartPoint.Maximum.ConvertTo(TimeUnit.Days, new DateTime(2023,1,1)).Units > currentLatestStartPoint.ConvertTo(TimeUnit.Days, new DateTime(2023,1,1)).Units)
        {
            currentLatestStartPoint = this.InitialStartPoint.Maximum;
        }

        return currentLatestStartPoint;
    }

    public DurationApproximate GetApproximateStartRangeBasedOnDependancies()
    {
        var earliestStartPoint = GetEarliestStartPointBasedOnDependancies();
        var latestStartPoint = GetLatestStartPointBasedOnDependancies();
        return DurationApproximate.From(earliestStartPoint, latestStartPoint); 
    }
    
    
    public DurationApproximate GetApproximateEndRangeBasedOnDependancies()
    {
        var earliestStartPoint = GetEarliestStartPointBasedOnDependancies();
        var latestStartPoint = GetLatestStartPointBasedOnDependancies();
        var earliestEndPoint = earliestStartPoint +  DurationApproximate.Minimum;
        var latestEndPoint =  latestStartPoint + DurationApproximate.Maximum;
        return DurationApproximate.From(earliestEndPoint, latestEndPoint); 
    }
    
}
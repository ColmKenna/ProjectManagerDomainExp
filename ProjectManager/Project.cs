using Measurements;
using PrimativeExtensions;

namespace ProjectManager;

public class Project
{
    private Project(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Project Create(string name, string description)
    {
        return new Project(name, description);
    }


    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public List<ProjectTask> Tasks { get; private set; } = new List<ProjectTask>();

    public Validation<ProjectTask> AddTask(string task, string details)
    {
        if (Tasks.Any(t => t.Name.Equals(task, StringComparison.OrdinalIgnoreCase)))
        {
            return Validation<ProjectTask>.Fail($"A task with the name '{task}' already exists in this project.");
        }

        var projectTask = ProjectTask.Create(task, details);
        Tasks.Add(projectTask);
        return projectTask;
    }

    // Method to add a required resource to a task
    public Validation<ProjectTask> AddResourceToTask(string taskName, RecourceRequired resource)
    {
        // Check that the task exists
        if (!Tasks.Any(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase)))
        {
            return Validation<ProjectTask>.Fail($"A task with the name '{taskName}' does not exist in this project.");
        }

        // Get the task
        var task = Tasks.First(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
        // Check that the resource is not already assigned to the task
        if (task.ResourcesRequired.Any(r => r.Name.Equals(resource.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return Validation<ProjectTask>.Fail($"The resource '{resource.Name}' is already assigned to this task.");
        }

        // Add the resource to the task
        var addResult = task.AddResourcesRequired(resource);
        return addResult.Map(x => task);
    }

    public Validation<Project> AssignResourceToResourceRequired(string taskName, string resourceName,
        ResourceAssigned resourceAssigned)
    {
        // Check that the task exists
        if (!Tasks.Any(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase)))
        {
            return Validation<Project>.Fail($"A task with the name '{taskName}' does not exist in this project.");
        }

        // Get the task
        var task = Tasks.First(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
        // Get the resource required
        Validation<ProjectTask> result = task.AssignResourceToResourceRequired(resourceName, resourceAssigned);
        return result.Map(x => this);
    }

    public Validation<ProjectTask> GetTaskByName(string taskName)
    {
        if (!Tasks.Any(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase)))
        {
            return Validation<ProjectTask>.Fail($"A task with the name '{taskName}' does not exist in this project.");
        }
        return Tasks.First(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
    }
}
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
    
    
}
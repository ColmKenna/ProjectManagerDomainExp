using Measurements;
using ProjectManager;

namespace ProjectManagerTests;

public class ProjectCreationTests
{
    [Fact]
    public void CanInitProject()
    {
        string name = "Test Project";
        string description = "This is a test project";
        
        // Call the Project.Create() method
        Project project = Project.Create(name, description);

        // Check that the project was created and has the correct properties
        Assert.NotNull(project);
        Assert.Equal(name, project.Name);
        Assert.Equal(description, project.Description);
        Assert.Empty(project.Tasks);
    }
    
    [Fact]
    public void CanAddTaskToProject()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";
        
        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        project.AddTask(taskName, taskDescription);

        // Check that the project was created and has the correct properties
        Assert.Single(project.Tasks);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
    }
    
    // Test for multiple tasks
    [Fact]
    public void CanAddMultipleTasksToProject()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";
        string taskName2 = "Test Task 2";
        string taskDescription2 = "This is a test task 2";
        
        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        project.AddTask(taskName, taskDescription);
        project.AddTask(taskName2, taskDescription2);

        // Check that the project was created and has the correct properties
        Assert.Equal(2, project.Tasks.Count);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.Equal(taskName2, project.Tasks[1].Name);
        Assert.Equal(taskDescription2, project.Tasks[1].Description);
    }
    
    // Cant add multiple tasks with the same name
    [Fact]
    public void CantAddMultipleTasksWithSameNameToProject()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";
        
        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        var result1 = project.AddTask(taskName, taskDescription);
        var result2 = project.AddTask(taskName, taskDescription);

        // Check that the project was created and has the correct properties
        Assert.Single(project.Tasks);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.True(result1.IsValid);
        Assert.False(result2.IsValid);
        Assert.Equal($"A task with the name '{taskName}' already exists in this project.", result2.ErrorMessage );
    }
    
    
}
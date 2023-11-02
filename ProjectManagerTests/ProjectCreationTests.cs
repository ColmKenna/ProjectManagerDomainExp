using Measurements;
using PrimativeExtensions;
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
        project.AddTask(taskName, taskDescription, Duration.Days(10));

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
        project.AddTask(taskName, taskDescription,Duration.Days(10));
        project.AddTask(taskName2, taskDescription2,Duration.Days(10));

        // Check that the project was created and has the correct properties
        Assert.Equal(2, project.Tasks.Count);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.Equal(taskName2, project.Tasks[1].Name);
        Assert.Equal(taskDescription2, project.Tasks[1].Description);
    }


    // Test for getting a Task by name
    [Fact]
    public void CanGetTaskByName()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";

        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        project.AddTask(taskName, taskDescription,Duration.Days(10));
        Validation<ProjectTask> task = project.GetTaskByName(taskName);

        // Check that the project was created and has the correct properties
        Assert.Single(project.Tasks);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.True(task.IsValid);
        task.ActionOnSuccess(t =>
        {
            Assert.Equal(taskName, t.Name);
            Assert.Equal(taskDescription, t.Description);
        });
    }


    [Fact]
    public void CantAddMultipleTasksWithSameNameToProject()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";

        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        var result1 = project.AddTask(taskName, taskDescription,Duration.Days(10));
        var result2 = project.AddTask(taskName, taskDescription,Duration.Days(10));

        // Check that the project was created and has the correct properties
        Assert.Single(project.Tasks);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.True(result1.IsValid);
        Assert.False(result2.IsValid);
        Assert.Equal($"A task with the name '{taskName}' already exists in this project.", result2.ErrorMessage);
    }
    
    // Task should have an approximate duration
    [Fact]
    public void TaskShouldHaveAnApproximateDuration()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";
        string resourceName = "Test Resource";
        string resourceDescription = "This is a test resource";
        int resourceQuantity = 1;
        DurationApproximate taskDuration = new DurationApproximate(Duration.Days(10), Duration.Days(12));



        Project project = Project.Create(name, description);
        var result =  project.AddTask(taskName, taskDescription, taskDuration);
        
        
        // check result is valid
        Assert.True(result.IsValid);
        // check task has duration
        result.ActionOnSuccess(t =>
        {
            Assert.Equal(taskDuration, t.DurationApproximate);
        });
        
        
        

    }
    
    // Task can have dependencies of other tasks been completed
    [Fact]
    public void TaskCanHaveDependenciesOfOtherTasksBeenCompleted()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";
        string taskName2 = "Test Task 2";
        string taskDescription2 = "This is a test task 2";

        Project project = Project.Create(name, description);
        var result1 = project.AddTask(taskName, taskDescription, Duration.Days(10));
        var result2 = project.AddTask(taskName2, taskDescription2, Duration.Days(10));

        Validation<ProjectTask> result =
            ValidationExtensions.Match(result1, result2,
                (task1, task2) =>
                {
                    return project.AddDependency(task1, task2);
                },
                (task1) => Validation<ProjectTask>.Fail("Task does not exist"),
                (task2) => Validation<ProjectTask>.Fail("Task does not exist"));

        Assert.True(result.IsValid);
        result.ActionOnSuccess(t =>
        {
            Assert.Equal(taskName, t.Name);
            Assert.Equal(taskDescription, t.Description);
            Assert.Equal(taskName2, t.Dependencies.First().Name);
            Assert.Equal(taskDescription2, t.Dependencies.First().Description);
        });

    }
    
    [Fact]
    public void TaskDependenciesCannotDependOnItself()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";

        Project project = Project.Create(name, description);
        var result1 = project.AddTask(taskName, "This is a test task", Duration.Days(10));
        var result2 = project.AddDependency(result1.Value, result1.Value);

        Assert.False(result2.IsValid);
        Assert.Equal("A task cannot be dependent on itself.", result2.ErrorMessage);
    }

    
    [Fact]
    public void TaskDependenciesCannotBeCircular()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskName2 = "Test Task 2";
        string taskName3 = "Test Task 3";

        Project project = Project.Create(name, description);
        var result1 = project.AddTask(taskName, "This is a test task", Duration.Days(10));
        var result2 = project.AddTask(taskName2, "This is a test task", Duration.Days(10));
        var result3 = project.AddTask(taskName3, "This is a test task", Duration.Days(10));

        var result4 = project.AddDependency(result2.Value, result1.Value);
        var result5 = project.AddDependency(result3.Value, result2.Value);
        var result6 = project.AddDependency(result1.Value, result3.Value);

        Assert.True(result4.IsValid);
        Assert.True(result5.IsValid);
        Assert.False(result6.IsValid);
        Assert.Equal($"{taskName3} is already a dependency of {taskName}.", result6.ErrorMessage);
    }
    

    //TODO: Subtasks?
}
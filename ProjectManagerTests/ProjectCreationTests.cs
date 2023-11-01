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
        project.AddTask(taskName, taskDescription);
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
    
    // Test for adding a resource to a task
    [Fact]
    public void CanAddResourceToTask()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";
        string resourceName = "Test Resource";
        string resourceDescription = "This is a test resource";
        int resourceQuantity = 1;
        
        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        project.AddTask(taskName, taskDescription);
        var resource = RecourceRequired.Create(resourceName, resourceDescription, resourceQuantity);
        var result = project.AddResourceToTask(taskName, resource);

        // Check that the project was created and has the correct properties
        Assert.Single(project.Tasks);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.Single(project.Tasks[0].ResourcesRequired);
        Assert.Equal(resourceName, project.Tasks[0].ResourcesRequired.First().Name);
        Assert.Equal(resourceDescription, project.Tasks[0].ResourcesRequired.First().Description);
        Assert.Equal(resourceQuantity, project.Tasks[0].ResourcesRequired.First().Quantity);
        Assert.True(result.IsValid);
        Assert.Equal(project.Tasks[0], result.Value);
    }
    
    // Cant add resource to a task that does not exist
    [Fact]
    public void CantAddResourceToTaskThatDoesNotExist()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string resourceName = "Test Resource";
        string resourceDescription = "This is a test resource";
        int resourceQuantity = 1;
        
        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        var resource = RecourceRequired.Create(resourceName, resourceDescription, resourceQuantity);
        var result = project.AddResourceToTask(taskName, resource);

        // Check that the project was created and has the correct properties
        Assert.Empty(project.Tasks);
        Assert.False(result.IsValid);
        Assert.Equal($"A task with the name '{taskName}' does not exist in this project.", result.ErrorMessage);
    }
    
    // Cant add resource to a task that already has the resource
    [Fact]
    public void CantAddResourceToTaskThatAlreadyHasResource()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";
        string resourceName = "Test Resource";
        string resourceDescription = "This is a test resource";
        int resourceQuantity = 1;
        
        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        project.AddTask(taskName, taskDescription);
        var resource = RecourceRequired.Create(resourceName, resourceDescription, resourceQuantity);
        var result1 = project.AddResourceToTask(taskName, resource);
        var result2 = project.AddResourceToTask(taskName, resource);

        // Check that the project was created and has the correct properties
        Assert.Single(project.Tasks);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.Single(project.Tasks[0].ResourcesRequired);
        Assert.Equal(resourceName, project.Tasks[0].ResourcesRequired.First().Name);
        Assert.Equal(resourceDescription, project.Tasks[0].ResourcesRequired.First().Description);
        Assert.Equal(resourceQuantity, project.Tasks[0].ResourcesRequired.First().Quantity);
        Assert.True(result1.IsValid);
        Assert.False(result2.IsValid);
        Assert.Equal($"The resource '{resource.Name}' is already assigned to this task.", result2.ErrorMessage);
    }
    
    
    // Assign a resource to a resource required
    [Fact]
    public void CanAssignResourceToResourceRequired()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";
        string resourceName = "Test Resource";
        string resourceDescription = "This is a test resource";
        int resourceQuantity = 1;
        string resourceAssignedName = "Test Resource Assigned";
        string resourceAssignedDescription = "This is a test resource assigned";
        int resourceAssignedQuantity = 1;

        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        project.AddTask(taskName, taskDescription);
        var resource = RecourceRequired.Create(resourceName, resourceDescription, resourceQuantity);
        var resourceAssigned = ResourceAssigned.Create(resourceAssignedName, resourceAssignedDescription, resourceAssignedQuantity);
        var result1 = project.AddResourceToTask(taskName, resource);
        var result2 = project.AssignResourceToResourceRequired(taskName, resourceName, resourceAssigned);

        // Check that the project was created and has the correct properties
        Assert.Single(project.Tasks);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.Single(project.Tasks[0].ResourcesRequired);
        Assert.Equal(resourceName, project.Tasks[0].ResourcesRequired.First().Name);
        Assert.Equal(resourceDescription, project.Tasks[0].ResourcesRequired.First().Description);
        Assert.Equal(resourceQuantity, project.Tasks[0].ResourcesRequired.First().Quantity);
        Assert.Single(project.Tasks[0].ResourcesRequired.First().ResourcesAssigned);
        Assert.Equal(resourceAssignedName, project.Tasks[0].ResourcesRequired.First().ResourcesAssigned.First().Name);
        Assert.Equal(resourceAssignedDescription,
            project.Tasks[0].ResourcesRequired.First().ResourcesAssigned.First().Description);
        Assert.Equal(resourceAssignedQuantity,
            project.Tasks[0].ResourcesRequired.First().ResourcesAssigned.First().Quantity);
    }
    
    // Cant assign a resource to a resource required that does not exist
    [Fact]
    public void CantAssignResourceToResourceRequiredThatDoesNotExist()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string resourceName = "Test Resource";
        string resourceAssignedName = "Test Resource Assigned";
        string resourceAssignedDescription = "This is a test resource assigned";
        int resourceAssignedQuantity = 1;

        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        var resourceAssigned = ResourceAssigned.Create(resourceAssignedName, resourceAssignedDescription, resourceAssignedQuantity);
        var result = project.AssignResourceToResourceRequired(taskName, resourceName, resourceAssigned);

        // Check that the project was created and has the correct properties
        Assert.Empty(project.Tasks);
        Assert.False(result.IsValid);
        Assert.Equal($"A task with the name '{taskName}' does not exist in this project.", result.ErrorMessage);
    }
    
    
    // cant assign to a non existent resource required
    [Fact]
    public void CantAssignResourceToNonExistentResourceRequired()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";
        string resourceName = "Test Resource";
        string resourceAssignedName = "Test Resource Assigned";
        string resourceAssignedDescription = "This is a test resource assigned";
        int resourceAssignedQuantity = 1;

        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        project.AddTask(taskName, taskDescription);
        var resourceAssigned = ResourceAssigned.Create(resourceAssignedName, resourceAssignedDescription, resourceAssignedQuantity);
        var result = project.AssignResourceToResourceRequired(taskName, resourceName, resourceAssigned);

        // Check that the project was created and has the correct properties
        Assert.Single(project.Tasks);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.Empty(project.Tasks[0].ResourcesRequired);
        Assert.False(result.IsValid);
        Assert.Equal($"A resource required with the name '{resourceName}' does not exist in this task.", result.ErrorMessage);
    }
    
    
    
}
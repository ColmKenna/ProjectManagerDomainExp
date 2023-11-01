using ProjectManager;

namespace ProjectManagerTests;

public class ResourceAssignmentTests{

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
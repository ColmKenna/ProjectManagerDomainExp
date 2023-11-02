using Measurements;
using ProjectManager;

namespace ProjectManagerTests;

public class ResourceAssignmentTests
{
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
        project.AddTask(taskName, taskDescription, Duration.Days(10));
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
        project.AddTask(taskName, taskDescription, Duration.Days(10));
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
        project.AddTask(taskName, taskDescription, Duration.Days(10));
        var resource = RecourceRequired.Create(resourceName, resourceDescription, resourceQuantity);
        var resourceAssigned =
            ResourceAssigned.Create(resourceAssignedName, resourceAssignedDescription, resourceAssignedQuantity);
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
        var resourceAssigned =
            ResourceAssigned.Create(resourceAssignedName, resourceAssignedDescription, resourceAssignedQuantity);
        var result = project.AssignResourceToResourceRequired(taskName, resourceName, resourceAssigned);

        // Check that the project was created and has the correct properties
        Assert.Empty(project.Tasks);
        Assert.False(result.IsValid);
        Assert.Equal($"A task with the name '{taskName}' does not exist in this project.", result.ErrorMessage);
    }


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
        project.AddTask(taskName, taskDescription, Duration.Days(10));
        var resourceAssigned =
            ResourceAssigned.Create(resourceAssignedName, resourceAssignedDescription, resourceAssignedQuantity);
        var result = project.AssignResourceToResourceRequired(taskName, resourceName, resourceAssigned);

        // Check that the project was created and has the correct properties
        Assert.Single(project.Tasks);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.Empty(project.Tasks[0].ResourcesRequired);
        Assert.False(result.IsValid);
        Assert.Equal($"A resource required with the name '{resourceName}' does not exist in this task.",
            result.ErrorMessage);
    }

    [Fact]
    public void CanAddMultipleResourcesRequiredToTask()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName = "Test Task";
        string taskDescription = "This is a test task";
        string resourceName1 = "Test Resource 1";
        string resourceDescription1 = "This is a test resource 1";
        int resourceQuantity1 = 1;
        string resourceName2 = "Test Resource 2";
        string resourceDescription2 = "This is a test resource 2";
        int resourceQuantity2 = 1;

        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        project.AddTask(taskName, taskDescription, Duration.Days(10));
        var resource1 = RecourceRequired.Create(resourceName1, resourceDescription1, resourceQuantity1);
        var resource2 = RecourceRequired.Create(resourceName2, resourceDescription2, resourceQuantity2);
        var result1 = project.AddResourceToTask(taskName, resource1);
        var result2 = project.AddResourceToTask(taskName, resource2);

        // Check that the project was created and has the correct properties
        Assert.Single(project.Tasks);
        Assert.Equal(taskName, project.Tasks[0].Name);
        Assert.Equal(taskDescription, project.Tasks[0].Description);
        Assert.Equal(2, project.Tasks[0].ResourcesRequired.Count);
        Assert.Equal(resourceName1, project.Tasks[0].ResourcesRequired.First().Name);
        Assert.Equal(resourceDescription1, project.Tasks[0].ResourcesRequired.First().Description);
        Assert.Equal(resourceQuantity1, project.Tasks[0].ResourcesRequired.First().Quantity);
        Assert.Equal(resourceName2, project.Tasks[0].ResourcesRequired.Last().Name);
        Assert.Equal(resourceDescription2, project.Tasks[0].ResourcesRequired.Last().Description);
        Assert.Equal(resourceQuantity2, project.Tasks[0].ResourcesRequired.Last().Quantity);
        Assert.True(result1.IsValid);
        Assert.True(result2.IsValid);
    }


    // Can get Total assigned qty for a resource Required
    [Fact]
    public void CanGetTotalAssignedQtyForResourceRequired()
    {
        string name = "Test Project";
        string description = "This is a test project";
        string taskName1 = "Test Task 1";
        string taskDescription1 = "This is a test task 1";
        string resourceName = "Test Resource";
        string resourceDescription = "This is a test resource";
        int resourceQuantity = 6;
        string resourceAssignedName1 = "Test Resource Assigned 1";
        string resourceAssignedDescription1 = "This is a test resource assigned 1";
        int resourceAssignedQuantity1 = 1;
        string resourceAssignedName2 = "Test Resource Assigned 2";
        string resourceAssignedDescription2 = "This is a test resource assigned 2";
        int resourceAssignedQuantity2 = 3;

        // Call the Project.Create() method
        Project project = Project.Create(name, description);
        project.AddTask(taskName1, taskDescription1, Duration.Days(10));
        var resource = RecourceRequired.Create(resourceName, resourceDescription, resourceQuantity);
        var resourceAssigned1 = ResourceAssigned.Create(resourceAssignedName1, resourceAssignedDescription1,
            resourceAssignedQuantity1);
        var resourceAssigned2 = ResourceAssigned.Create(resourceAssignedName2, resourceAssignedDescription2,
            resourceAssignedQuantity2);
        var result1 = project.AddResourceToTask(taskName1, resource);
        var result2 = project.AssignResourceToResourceRequired(taskName1, resourceName, resourceAssigned1);
        var result3 = project.AssignResourceToResourceRequired(taskName1, resourceName, resourceAssigned2);


        // Assert the total Assigned Qty is correct
        Assert.Equal(4, project.Tasks[0].ResourcesRequired.First().TotalAssignedQty);
        Assert.Equal(2, project.Tasks[0].ResourcesRequired.First().QuantityRequiredRemaining);
        
    }
}
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
}
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
}
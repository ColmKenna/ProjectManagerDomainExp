namespace ProjectManager;

public class ProjectTask
{
    private ProjectTask(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static ProjectTask Create(string name, string description)
    {
        return new ProjectTask(name, description);
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
}

public class RecourceRequired
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IList<ResourceAssigned> ResourcesAssigned { get; private set; } = new List<ResourceAssigned>();
    public int Quantity { get; private set; }
}

public class ResourceAssigned
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
}
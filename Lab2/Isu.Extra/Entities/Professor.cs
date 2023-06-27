namespace Isu.Extra.Entities;

public class Professor
{
    public Professor(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }

    public string Name { get; }
    public Guid Id { get; }
}
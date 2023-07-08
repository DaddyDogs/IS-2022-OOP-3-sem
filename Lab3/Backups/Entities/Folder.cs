using Backups.Interfaces;

namespace Backups.Entities;

public class Folder : IFolder
{
    public Folder(Func<IReadOnlyCollection<IRepositoryObject>> repositoryObjects, string name)
    {
        RepositoryObjects = repositoryObjects;
        Name = name;
    }

    public Func<IReadOnlyCollection<IRepositoryObject>> RepositoryObjects { get; }
    public string Name { get; }
    public string GetName()
    {
        return Name;
    }

    public void Accept(IRepositoryObjectVisitor visitor)
    {
        visitor.Visit(this);
    }

    public IReadOnlyCollection<IRepositoryObject> GetSubDirectories()
    {
        return RepositoryObjects.Invoke();
    }
}
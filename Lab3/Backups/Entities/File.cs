using Backups.Interfaces;

namespace Backups.Entities;

public class File : IFile
{
    public File(string name, Func<Stream> stream)
    {
        Name = name;
        Stream = stream;
    }

    public string Name { get; }
    public Func<Stream> Stream { get; }
    public string GetName()
    {
        return Name;
    }

    public Stream GetStream()
    {
        return Stream.Invoke();
    }

    public void Accept(IRepositoryObjectVisitor visitor)
    {
        visitor.Visit(this);
    }
}
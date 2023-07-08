namespace Backups.Interfaces;

public interface IFile : IRepositoryObject
{
    public Stream GetStream();
}
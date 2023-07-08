namespace Backups.Interfaces;

public interface IFolder : IRepositoryObject
{
    IReadOnlyCollection<IRepositoryObject> GetSubDirectories();
}
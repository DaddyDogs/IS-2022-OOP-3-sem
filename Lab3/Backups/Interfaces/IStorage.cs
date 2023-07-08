using Backups.Entities;

namespace Backups.Interfaces;

public interface IStorage
{
    IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects();
    IReadOnlyCollection<ZipFolder> GetZipObjects();
}
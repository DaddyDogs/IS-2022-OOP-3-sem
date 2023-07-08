using Backups.Interfaces;

namespace Backups.Algorithms;

public class SingleStorageAlgorithm : IStorageAlgorithm
{
    public IStorage CreateStorages(IEnumerable<IBackupObject> backupObjects, IRepository repository, IZipArchive zipArchive, string path)
    {
        var repositoryObjects = new List<IRepositoryObject>(0);
        repositoryObjects.AddRange(backupObjects.Select(backupObject => backupObject.GetRepository().CreateObject(backupObject.GetPath())));

        return zipArchive.CreateStorage(repositoryObjects, repository, path);
    }
}
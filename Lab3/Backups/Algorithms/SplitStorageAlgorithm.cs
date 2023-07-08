using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Algorithms;

public class SplitStorageAlgorithm : IStorageAlgorithm
{
    public IStorage CreateStorages(IEnumerable<IBackupObject> backupObjects, IRepository repository, IZipArchive zipArchive, string path)
    {
        var storages = new List<IStorage>(0);
        storages.AddRange(backupObjects.Select(backupObject => backupObject.GetRepository().CreateObject(backupObject.GetPath())).Select(repositoryObject => zipArchive.CreateStorage(new List<IRepositoryObject> { repositoryObject }, repository, path)));

        return new SplitStorageAdapter(repository, path, storages);
    }
}
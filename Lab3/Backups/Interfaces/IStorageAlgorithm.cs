namespace Backups.Interfaces;

public interface IStorageAlgorithm
{
    IStorage CreateStorages(IEnumerable<IBackupObject> backupObjects, IRepository repository, IZipArchive zipArchive, string path);
}
using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private readonly List<IBackupObject> _backupObjects;
    private readonly IBackup _backup;
    public BackupTask(string name, IStorageAlgorithm storageAlgorithm, IRepository repository, IZipArchive zipArchive, IBackup backup)
    {
        Name = name;
        Id = Guid.NewGuid();

        Repository = repository;
        ZipArchive = zipArchive;
        StorageAlgorithm = storageAlgorithm;
        _backupObjects = new List<IBackupObject>(0);
        _backup = backup;
    }

    public string Name { get; }
    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public IZipArchive ZipArchive { get; }
    public IReadOnlyList<IBackupObject> BackupObjects => _backupObjects;
    public Guid Id { get; }

    public void AddBackupObject(IBackupObject backupObject)
    {
        if (_backupObjects.Contains(backupObject))
        {
            throw BackupTaskException.BackupObjectAlreadyExists(backupObject.GetPath());
        }

        _backupObjects.Add(backupObject);
    }

    public void RemoveBackupObject(IBackupObject backupObject)
    {
        if (!_backupObjects.Contains(backupObject))
        {
            throw BackupTaskException.BackupObjectDoesNotExist(backupObject.GetPath());
        }

        _backupObjects.Remove(backupObject);
    }

    public void CreateRestorePoint(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException();
        }

        Repository.CreateSubDirectory(path, Name);
        path = $@"{path}{Name}" + Path.DirectorySeparatorChar;
        IStorage storage = StorageAlgorithm.CreateStorages(_backupObjects, Repository, ZipArchive, path);
        var restorePoint = new RestorePoint(_backupObjects, DateTime.Now, storage);
        _backup.AddRestorePoint(restorePoint);
    }

    public int GetRestorePointCount()
    {
        return _backup.GetRestorePoints().Count;
    }

    public int GetArchivesCount()
    {
        return _backup.GetRestorePoints().Sum(restorePoint => restorePoint.Storage.GetZipObjects().Count);
    }
}
using Backups.Interfaces;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<IBackupObject> _backupObjects;
    public RestorePoint(IEnumerable<IBackupObject> backupObjects, DateTime creationDate, IStorage storage)
    {
        _backupObjects = new List<IBackupObject>(backupObjects);
        CreationDate = creationDate;
        Storage = storage;
    }

    public DateTime CreationDate { get; }
    public IStorage Storage { get; }
    public IReadOnlyList<IBackupObject> BackupObject => _backupObjects;
}
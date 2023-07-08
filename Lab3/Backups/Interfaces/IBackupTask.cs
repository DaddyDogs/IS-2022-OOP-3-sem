namespace Backups.Interfaces;

public interface IBackupTask
{
    void AddBackupObject(IBackupObject backupObject);
    void RemoveBackupObject(IBackupObject backupObject);
    void CreateRestorePoint(string path);
    int GetRestorePointCount();
    int GetArchivesCount();
}
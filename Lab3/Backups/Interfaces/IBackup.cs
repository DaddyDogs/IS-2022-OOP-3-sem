using Backups.Entities;

namespace Backups.Interfaces;

public interface IBackup
{
    void AddRestorePoint(RestorePoint restorePoint);
    IReadOnlyList<RestorePoint> GetRestorePoints();
}
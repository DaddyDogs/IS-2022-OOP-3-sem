using Backups.Interfaces;

namespace Backups.Entities;

public class Backup : IBackup
{
    private readonly List<RestorePoint> _restorePoints;
    public Backup()
    {
        _restorePoints = new List<RestorePoint>(0);
        Id = Guid.NewGuid();
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;
    public Guid Id { get; }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        _restorePoints.Add(restorePoint);
    }

    public IReadOnlyList<RestorePoint> GetRestorePoints()
    {
        return _restorePoints;
    }
}
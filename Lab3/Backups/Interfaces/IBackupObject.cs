namespace Backups.Interfaces;

public interface IBackupObject
{
    IRepository GetRepository();
    string GetPath();
    IRepositoryObject GetRepositoryObject();
}
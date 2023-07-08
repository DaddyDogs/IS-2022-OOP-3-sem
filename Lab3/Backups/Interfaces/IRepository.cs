namespace Backups.Interfaces;

public interface IRepository
{
    IRepositoryObject CreateObject(string id);
    Stream GetStream(string path);
    public void CreateSubDirectory(string path, string name);
}
using Backups.Interfaces;

namespace Backups.Entities;

public class FileSystemObject : IBackupObject
{
    public FileSystemObject(string path, IRepository repository)
    {
        Id = path;
        Repository = repository;
    }

    public string Id { get; }
    public IRepository Repository { get; }
    public IRepository GetRepository()
    {
        return Repository;
    }

    public string GetPath()
    {
        return Id;
    }

    public IRepositoryObject GetRepositoryObject()
    {
        return Repository.CreateObject(Id);
    }
}
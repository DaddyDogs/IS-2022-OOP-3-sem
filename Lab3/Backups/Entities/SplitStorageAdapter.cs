using Backups.Interfaces;

namespace Backups.Entities;

public class SplitStorageAdapter : IStorage
{
    private IReadOnlyCollection<ZipFolder> _zipObjects;
    private IReadOnlyCollection<IStorage> _storages;
    public SplitStorageAdapter(IRepository repository, string path, IReadOnlyCollection<IStorage> storages)
    {
        Repository = repository;
        Path = path;
        var zipObjects = new List<ZipFolder>(0);
        zipObjects.AddRange(storages.SelectMany(storage => storage.GetZipObjects()));
        _zipObjects = zipObjects;
        _storages = storages;
    }

    public IRepository Repository { get; }
    public string Path { get; }
    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        var repositoryObjects = new List<IRepositoryObject>(0);
        repositoryObjects.AddRange(_storages.SelectMany(s => s.GetRepositoryObjects()));
        return repositoryObjects;
    }

    public IReadOnlyCollection<ZipFolder> GetZipObjects()
    {
        return _zipObjects;
    }
}
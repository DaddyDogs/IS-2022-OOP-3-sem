using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipStorage : IStorage
{
    private readonly List<ZipFolder> _zipFolders;
    public ZipStorage(IRepository repository, string path, List<ZipFolder> zipFolders)
    {
        Repository = repository;
        Path = path;
        _zipFolders = zipFolders;
    }

    public IRepository Repository { get; }
    public string Path { get; }
    public IReadOnlyCollection<IRepositoryObject> GetRepositoryObjects()
    {
        var repositoryObjects = new List<IRepositoryObject>(0);
        Stream stream = Repository.GetStream(Path);
        const string filePath = "C:/test/FUCK";
        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
        stream.CopyTo(fileStream);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read);
        repositoryObjects.AddRange(from entry in archive.Entries from zipObject in _zipFolders[0].ZipObjects where entry.Name == zipObject.GetName() select zipObject.GetRepositoryObject(entry));

        stream.Close();
        fileStream.Close();

        return repositoryObjects;
    }

    public IReadOnlyCollection<ZipFolder> GetZipObjects()
    {
        return _zipFolders;
    }
}
using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipFolder : IZipObject
{
    private readonly List<IZipObject> _zipObjects;

    public ZipFolder(string name, List<IZipObject> zipObjects)
    {
        Name = name;
        _zipObjects = zipObjects;
    }

    public IEnumerable<IZipObject> ZipObjects => _zipObjects;

    public string Name { get; }
    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry zipArchiveEntry)
    {
        var repositoryObjects = new List<IRepositoryObject>(0);
        var archive = new ZipArchive(zipArchiveEntry.Open(), ZipArchiveMode.Read);
        repositoryObjects.AddRange(from entry in archive.Entries from zipObject in _zipObjects where entry.Name == zipObject.GetName() select zipObject.GetRepositoryObject(entry));

        archive.Dispose();
        return new Folder(() => repositoryObjects, zipArchiveEntry.Name);
    }

    public string GetName()
    {
        return Name;
    }
}
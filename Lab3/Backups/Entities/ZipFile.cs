using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipFile : IZipObject
{
    public ZipFile(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public IRepositoryObject GetRepositoryObject(ZipArchiveEntry zipArchiveEntry)
    {
        return new File(Name, () => zipArchiveEntry.Open());
    }

    public string GetName()
    {
        return Name;
    }
}
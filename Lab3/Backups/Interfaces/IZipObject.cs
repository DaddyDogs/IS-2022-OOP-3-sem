using System.IO.Compression;

namespace Backups.Interfaces;

public interface IZipObject
{
    IRepositoryObject GetRepositoryObject(ZipArchiveEntry zipArchiveEntry);
    string GetName();
}
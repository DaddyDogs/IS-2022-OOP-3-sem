using System.IO.Compression;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class ZipArchiver : IZipArchive
{
    private readonly DateTimeParser _dateTimeParser = new ();
    public IStorage CreateStorage(List<IRepositoryObject> repositoryObjects, IRepository repository, string path)
    {
        var zipFolders = new List<ZipFolder>(0);
        string archivePath = path + DateTimeParser.GetDateTime() + ".zip";
        using Stream stream = repository.GetStream(archivePath);
        var zipObjects = new List<IZipObject>();
        using var archive = new ZipArchive(stream, ZipArchiveMode.Create);
        foreach (IRepositoryObject repositoryObject in repositoryObjects)
        {
            IRepositoryObjectVisitor visitor = new ZipVisitor(archive);
            repositoryObject.Accept(visitor);
            zipObjects.AddRange(visitor.GetZipObjects().ToList());
        }

        zipFolders.Add(new ZipFolder(archivePath, zipObjects));
        return new ZipStorage(repository, archivePath, zipFolders);
    }
}
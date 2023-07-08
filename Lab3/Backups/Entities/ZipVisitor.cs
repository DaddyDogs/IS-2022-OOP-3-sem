using System.IO.Compression;
using Backups.Interfaces;

namespace Backups.Entities;

public class ZipVisitor : IRepositoryObjectVisitor
{
    private readonly Stack<ZipArchive> _zipArchives = new (0);
    private readonly Stack<List<IZipObject>> _zipObjects = new (0);

    public ZipVisitor(ZipArchive zipArchive)
    {
        _zipArchives.Push(zipArchive);
        _zipObjects.Push(new List<IZipObject>(0));
    }

    public void Visit(IFile file)
    {
        ZipArchiveEntry archiveEntry = _zipArchives.Peek().CreateEntry(file.GetName());
        using Stream archiveStream = archiveEntry.Open();
        using Stream fileStream = file.GetStream();
        fileStream.CopyTo(archiveStream);
        var zipFile = new ZipFile(file.GetName());
        _zipObjects.Peek().Add(zipFile);
    }

    public void Visit(IFolder folder)
    {
        ZipArchiveEntry archiveEntry = _zipArchives.Peek().CreateEntry(folder.GetName() + ".zip");
        using Stream archiveStream = archiveEntry.Open();
        using var fileArchive = new ZipArchive(archiveStream, ZipArchiveMode.Create);
        _zipArchives.Push(fileArchive);
        _zipObjects.Push(new List<IZipObject>(0));
        foreach (IRepositoryObject file in folder.GetSubDirectories())
        {
            file.Accept(this);
        }

        var zipFolder = new ZipFolder(folder.GetName() + ".zip", _zipObjects.Peek());
        _zipObjects.Pop();
        _zipObjects.Peek().Add(zipFolder);
        _zipArchives.Pop();
    }

    public IReadOnlyList<IZipObject> GetZipObjects()
    {
        return _zipObjects.Peek();
    }
}
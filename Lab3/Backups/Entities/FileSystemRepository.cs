using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Entities;

public class FileSystemRepository : IRepository
{
    public IRepositoryObject CreateObject(string id)
    {
        if (System.IO.Directory.Exists(id))
        {
            var repositoryObjects = new List<IRepositoryObject>(0);
            IReadOnlyCollection<string> files = System.IO.Directory.GetFiles(id);
            repositoryObjects.AddRange(files.Select(CreateObject));

            var folder = new Folder(() => repositoryObjects, Path.GetFileName(id));
            return folder;
        }

        if (!System.IO.File.Exists(id)) throw RepositoryExceptions.InvalidPath(id);
        var file = new File(Path.GetFileName(id), () => new FileStream(id, FileMode.Open));
        return file;
    }

    public Stream GetStream(string path)
    {
        return System.IO.File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }

    public void CreateSubDirectory(string path, string name)
    {
        Directory.CreateDirectory($@"{path}" + Path.DirectorySeparatorChar + $"{name}");
    }
}
namespace Backups.Interfaces;

public interface IZipArchive
{
    IStorage CreateStorage(List<IRepositoryObject> repositoryObjects, IRepository repository, string path);
}
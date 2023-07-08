namespace Backups.Interfaces;

public interface IRepositoryObjectVisitor
{
    void Visit(IFile file);
    void Visit(IFolder folder);
    IReadOnlyList<IZipObject> GetZipObjects();
}
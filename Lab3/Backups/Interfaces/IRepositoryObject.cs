namespace Backups.Interfaces;

public interface IRepositoryObject
{
    string GetName();
    void Accept(IRepositoryObjectVisitor visitor);
}
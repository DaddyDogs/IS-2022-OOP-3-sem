namespace Backups.Exceptions;

public class RepositoryExceptions : BackupsExceptions
{
    private RepositoryExceptions(string message)
        : base(message) { }

    public static RepositoryExceptions InvalidPath(string path)
    {
        return new RepositoryExceptions($"Path {path} is invalid. Can't find this directory");
    }
}
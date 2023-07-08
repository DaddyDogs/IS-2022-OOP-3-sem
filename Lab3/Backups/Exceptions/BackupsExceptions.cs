namespace Backups.Exceptions;

public class BackupsExceptions : Exception
{
    public BackupsExceptions() { }
    public BackupsExceptions(string message)
        : base(message) { }
}
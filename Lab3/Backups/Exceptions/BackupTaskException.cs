namespace Backups.Exceptions;

public class BackupTaskException : BackupsExceptions
{
    private BackupTaskException(string message)
        : base(message) { }

    public static BackupTaskException BackupObjectAlreadyExists(string name)
    {
        return new BackupTaskException($"Backup object {name} already exists.");
    }

    public static BackupTaskException BackupObjectDoesNotExist(string name)
    {
        return new BackupTaskException($"Backup object {name} does not exist. Can't remove");
    }
}
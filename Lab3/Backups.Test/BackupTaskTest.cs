using Backups.Algorithms;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
using Xunit;
using Zio;
using Zio.FileSystems;
namespace Backups.Test;

public class InMemoryRepository : IRepository, IDisposable
{
    private readonly MemoryFileSystem _memoryFileSystem;

    public InMemoryRepository(MemoryFileSystem memoryFileSystem)
    {
        _memoryFileSystem = memoryFileSystem;
    }

    public IRepositoryObject CreateObject(string id)
    {
        if (_memoryFileSystem.DirectoryExists(id))
        {
            var repositoryObjects = new List<IRepositoryObject>(0);
            IEnumerable<FileSystemItem> subDirectories = _memoryFileSystem.EnumerateItems(id, SearchOption.AllDirectories);
            repositoryObjects.AddRange(subDirectories.Select(directory => CreateObject(directory.FullName)));

            var folder = new Folder(() => repositoryObjects, Path.DirectorySeparatorChar + Path.GetFileName(id));
            return folder;
        }

        if (!_memoryFileSystem.FileExists(id)) throw RepositoryExceptions.InvalidPath(id);
        var file = new Entities.File(id, () => _memoryFileSystem.OpenFile(Path.DirectorySeparatorChar + Path.GetFileName(id), FileMode.Open, FileAccess.ReadWrite));
        return file;
    }

    public Stream GetStream(string path)
    {
        return _memoryFileSystem.OpenFile(path, FileMode.Create, FileAccess.ReadWrite);
    }

    public void CreateSubDirectory(string path, string name)
    {
        _memoryFileSystem.CreateDirectory($@"{path}{name}");
    }

#pragma warning disable CA1816
    public void Dispose()
#pragma warning restore CA1816
    {
        _memoryFileSystem.Dispose();
    }
}

public class BackupTests
{
    [Fact]
    public void BackupTwoObjects_BackupOneObjectWithSplitAlgo_TwoRestorePointsThreeArchives()
    {
        var memoryFileSystem = new MemoryFileSystem();
        var repository = new InMemoryRepository(memoryFileSystem);
        var backupTask = new BackupTask("task1", new SplitStorageAlgorithm(), repository, new ZipArchiver(), new Backup());

        memoryFileSystem.WriteAllText(Path.DirectorySeparatorChar + "aboba.txt", "aboba");
        IBackupObject file1 = new FileSystemObject(Path.DirectorySeparatorChar + "aboba.txt", repository);
        memoryFileSystem.CreateDirectory(Path.DirectorySeparatorChar + "tests");
        IBackupObject folder1 = new FileSystemObject(Path.DirectorySeparatorChar + "tests", repository);

        backupTask.AddBackupObject(folder1);
        backupTask.AddBackupObject(file1);

        memoryFileSystem.CreateDirectory(Path.DirectorySeparatorChar + "test" + Path.DirectorySeparatorChar);
        backupTask.CreateRestorePoint(Path.DirectorySeparatorChar + "test" + Path.DirectorySeparatorChar);

        backupTask.RemoveBackupObject(folder1);

        backupTask.CreateRestorePoint(Path.DirectorySeparatorChar + "test" + Path.DirectorySeparatorChar);

        Assert.Equal(2, backupTask.GetRestorePointCount());
        Assert.Equal(3, backupTask.GetArchivesCount());
    }
}
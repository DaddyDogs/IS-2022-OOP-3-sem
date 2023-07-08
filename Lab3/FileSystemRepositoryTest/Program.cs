using Backups.Algorithms;
using Backups.Entities;
using Backups.Interfaces;

IRepository repository = new FileSystemRepository();
var backupTask = new BackupTask("task1", new SingleStorageAlgorithm(), repository, new ZipArchiver(), new Backup());

IBackupObject file1 = new FileSystemObject("C:/tests/boba", repository);
IBackupObject file2 = new FileSystemObject("C:/tests/сус.jpg", repository);
IBackupObject file3 = new FileSystemObject("C:/tests/tuesgai.txt", repository);

backupTask.AddBackupObject(file1);
backupTask.AddBackupObject(file2);

backupTask.CreateRestorePoint("C:/test/");
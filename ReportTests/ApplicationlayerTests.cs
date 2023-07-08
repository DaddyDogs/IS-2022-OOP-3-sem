using ApplicationLayer.Exceptions;
using ApplicationLayer.Extensions;
using ApplicationLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Extensions;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ReportTests;

public class Tests : IDisposable
{
    private static readonly IServiceCollection _serviceCollection = new ServiceCollection();
    private static ServiceProvider _serviceProvider = _serviceCollection.BuildServiceProvider();
    private readonly DatabaseContext _dbContext;
    private readonly ISupervisorService _supervisorService;
    private readonly IAccountService _accountService;
    private readonly IMessageService _messageService;
    private readonly IMessageSourceService _messageSourceService;
    public Tests()
    {
        _serviceCollection.AddApplication();
        _serviceCollection.AddDataAccess(options => options.UseInMemoryDatabase("DataSource=Test.db"));
        _serviceProvider = _serviceCollection.BuildServiceProvider();
        _supervisorService = _serviceProvider.GetRequiredService<ISupervisorService>();
        _accountService = _serviceProvider.GetRequiredService<IAccountService>();
        _messageService = _serviceProvider.GetRequiredService<IMessageService>();
        _messageSourceService = _serviceProvider.GetRequiredService<IMessageSourceService>();
        _dbContext = _serviceProvider.GetRequiredService<DatabaseContext>();
    }

    [Fact]
    public void AddNewEmployee_SubordinateBecomeSupervisor()
    {
        var supervisor = new Supervisor("Roman", "Makarevich", null);
        _dbContext.Supervisors.Add(supervisor);

        var privateAccount = new PrivateAccount(new List<Account>(0), supervisor, "fdfd", "fdfd");
        supervisor.PrivateAccount = privateAccount;
        _dbContext.PrivateAccounts.Add(privateAccount);

        var tempId = Guid.NewGuid();
        _dbContext.TemporaryIds.Add(new TemporaryId(supervisor.Identifier, tempId));

        _dbContext.SaveChanges();

        Task<Guid> id = _supervisorService.AddEmployee("Ivan", "Lololo", supervisor.Identifier, tempId, "fdfd", "fdfd", CancellationToken.None);
        Assert.Equal(typeof(Subordinate), _supervisorService.FindEmployee(id.Result)?.GetType());

        Task<Guid> id2 = _supervisorService.AddEmployee("Kiik", "Butovski", id.Result, tempId, "fdfdfd", "fdfdfdfd", CancellationToken.None);
        Assert.Equal(typeof(Supervisor), _supervisorService.FindEmployee(id2.Result)?.Supervisor?.GetType());
    }

    [Fact]
    public void MakeReport()
    {
        var supervisor = new Supervisor("Roman", "Makarevich", null);
        _dbContext.Supervisors.Add(supervisor);

        supervisor.PrivateAccount = new PrivateAccount(new List<Account>(0), supervisor, "fdfd", "fdfd");
        _dbContext.PrivateAccounts.Add(supervisor.PrivateAccount);

        _dbContext.SaveChanges();

        _accountService.Login(supervisor.PrivateAccount.Login, supervisor.PrivateAccount.Password, CancellationToken.None);

        _dbContext.SaveChanges();

        TemporaryId? temporaryId = _dbContext.TemporaryIds.Local.ToObservableCollection().FirstOrDefault(x => x.EmployeeId == supervisor.Identifier);
        if (temporaryId is null)
        {
            throw NotFoundException.EntityNotFoundException<Employee>(supervisor.Identifier);
        }

        Task<Guid> id = _supervisorService.AddEmployee("Ivan", "Lololo", supervisor.Identifier, temporaryId.TempId, "fd55fd", "f77dfd", CancellationToken.None);
        _accountService.GetEmployee(id.Result).PrivateAccount = new PrivateAccount(new List<Account>(0), _accountService.GetEmployee(id.Result), "fdqqfd", "wqqq");
        PrivateAccount? privateAccount = _accountService.GetEmployee(id.Result).PrivateAccount;
        if (privateAccount is null)
        {
            throw NotFoundException.EntityNotFoundException<PrivateAccount>(id.Result);
        }

        PrivateAccount? entity = _accountService.GetEmployee(id.Result).PrivateAccount;
        if (entity == null)
        {
            throw NotFoundException.EntityNotFoundException<PrivateAccount>(id.Result);
        }

        _dbContext.SaveChanges();

        Guid emailId = _messageSourceService.AddEmail("7777");
        Guid emailId2 = _messageSourceService.AddEmail("22222");

        MessageSource? email = _dbContext.MessageSource.Find(emailId);
        if (email is null)
        {
            throw NotFoundException.EntityNotFoundException<MessageSource>(emailId);
        }

        MessageSource? email2 = _dbContext.MessageSource.Find(emailId2);
        if (email2 is null)
        {
            throw NotFoundException.EntityNotFoundException<MessageSource>(emailId2);
        }

        _supervisorService.AddAccount(email.Id, email2.Id, temporaryId.EmployeeId, temporaryId.TempId);

        Guid messageId = _messageService.SendMessage("22222", "7777",   "fdfdfdfd", "fdfddf", temporaryId.TempId);

        _supervisorService.AddAccount(email2.Id, email.Id, temporaryId.EmployeeId, temporaryId.TempId);

        _messageService.MarkAsRead(messageId, temporaryId.TempId);

        Task<Report> report = _supervisorService.MakeReport(temporaryId.TempId, new TimeSpan(0, 5, 0, 0));

        Assert.Equal(1, report.Result.HandledMessagesCount);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
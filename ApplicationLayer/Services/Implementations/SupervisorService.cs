using ApplicationLayer.Exceptions;
using ApplicationLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Implementations;

public class SupervisorService : ISupervisorService
{
    private readonly IValidationService _validationService;
    private readonly DatabaseContext _context;

    public SupervisorService(DatabaseContext context)
    {
        _context = context;
        _validationService = new ValidationService(_context);
    }

    public Task<Guid> AddEmployee(string firstName, string secondName, Guid supervisorId, Guid requestingId, string login, string password, CancellationToken cancellationToken)
    {
        _validationService.CheckAccessForEmployee(requestingId, supervisorId);
        Employee? employee = _context.Employee.Find(supervisorId);
        employee = _validationService.CheckEntityForNull(employee, supervisorId);

        Subordinate subordinate;
        if (employee is Supervisor head)
        {
            subordinate = new Subordinate(firstName, secondName, head);
            AddSubordinate(head, subordinate);
        }
        else
        {
            Employee headOfSupervisor = _validationService.CheckEntityForNull(employee.Supervisor, supervisorId);
            RemoveSubordinate(headOfSupervisor, employee);

            var newSupervisor = new Supervisor(employee.FirstName, employee.LastName, employee.Supervisor);
            PrivateAccount account = _validationService.CheckEntityForNull(employee.PrivateAccount, employee.Identifier);

            string login2 = account.Login;
            string password2 = account.Password;
            newSupervisor.PrivateAccount =
                new PrivateAccount(account.Accounts, newSupervisor, login2, password2);

            subordinate = new Subordinate(firstName, secondName, newSupervisor);

            AddSubordinate(newSupervisor, subordinate);
            AddSubordinate(headOfSupervisor, newSupervisor);

            _context.Remove(employee);
        }

        subordinate.PrivateAccount = new PrivateAccount(new List<Account>(0), subordinate, login, password);

        _context.PrivateAccounts.Add(subordinate.PrivateAccount);
        _context.SaveChanges();
        return Task.FromResult(subordinate.Identifier);
    }

    public Task RemoveEmployee(Guid employeeId, Guid requestingId, CancellationToken cancellationToken)
    {
        _validationService.CheckAccessForEmployee(requestingId, employeeId);
        Employee? employee = _context.Employee.Find(employeeId);
        employee = _validationService.CheckEntityForNull(employee, employeeId);

        PrivateAccount account = _validationService.CheckEntityForNull(employee.PrivateAccount, employeeId);
        _context.PrivateAccounts.Remove(account);
        _context.Employee.Remove(employee);
        Employee head = _validationService.CheckEntityForNull(employee.Supervisor, employeeId);

        RemoveSubordinate(head, employee);
        if (employee is not Supervisor supervisor) return Task.CompletedTask;
        foreach (Employee worker in supervisor.Employees)
        {
            worker.Supervisor = employee.Supervisor;
        }

        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public Employee? FindEmployee(Guid employeeId)
    {
        return _context.Employee.Find(employeeId);
    }

    public Task<Report> MakeReport(Guid requestingId, TimeSpan timeSpan)
    {
        TemporaryId? temporaryId = _context.TemporaryIds.Find(requestingId);
        temporaryId = _validationService.CheckEntityForNull(temporaryId, requestingId);

        Employee? employee = FindEmployee(temporaryId.EmployeeId);
        employee = _validationService.CheckEntityForNull(employee, temporaryId.EmployeeId);

        if (employee is Subordinate)
        {
            throw NoPermissionException.NoAccessForReport();
        }

        var visitor = new ReportMakerEmployeeVisitor();
        employee.Accept(visitor);

        var report = new Report(DateTime.Now, employee.Identifier, visitor.HandledMessagesCount, visitor.Messages.Count(x => DateTime.Now - x.DateTime <= timeSpan), visitor.MessageSourceInfos);
        _context.Reports.Add(report);
        _context.SaveChanges();

        return Task.FromResult(report);
    }

    public Guid AddAccount(Guid messageSourceId, Guid addressee, Guid employeeId, Guid requestingId)
    {
        _validationService.CheckAccessForEmployee(requestingId, employeeId);

        Employee? employee = _context.Employee.Find(employeeId);
        _validationService.CheckEntityForNull(employee, employeeId);

        MessageSource? messageSource = _context.MessageSource.Find(messageSourceId);
        messageSource = _validationService.CheckEntityForNull(messageSource, messageSourceId);

        MessageSource? addresseeSource = _context.MessageSource.Find(addressee);
        addresseeSource = _validationService.CheckEntityForNull(addresseeSource, addressee);

        var newAccount = new Account(messageSource, new List<MessageSource> { addresseeSource });
        PrivateAccount? account = _context.PrivateAccounts.Find(employeeId);
        account = _validationService.CheckEntityForNull(account, employeeId);
        account.Accounts.Add(newAccount);
        _context.Accounts.Add(newAccount);
        _context.SaveChanges();
        return newAccount.Id;
    }

    public List<Report> GetReports(Guid requestingId)
    {
        var visitor = new CheckAccessEmployeeVisitor();
        TemporaryId? tempId = _context.TemporaryIds.FirstOrDefault(x => x.TempId == requestingId);
        if (tempId is null)
        {
            throw NotFoundException.EntityNotFoundException<TemporaryId>(requestingId);
        }

        Employee? employee = FindEmployee(tempId.EmployeeId);
        if (employee is null)
        {
            throw NotFoundException.EntityNotFoundException<Employee>(tempId.EmployeeId);
        }

        employee.Accept(visitor);
        var reports = new List<Report>(_context.Reports.Where(x => visitor.SubordinateIds.Contains(x.CreatorId)));
        return reports;
    }

    private static void AddSubordinate(Employee employee, Employee subordinate)
    {
        if (employee is not Supervisor supervisor)
        {
            throw NoPermissionException.EmployeeIsNotYourSubordinate(subordinate.Identifier);
        }

        if (supervisor.Employees.Contains(subordinate))
        {
            throw SubordinateException.SubordinateAlreadyExistsException(subordinate.Identifier);
        }

        supervisor.Employees.Add(subordinate);
    }

    private static void RemoveSubordinate(Employee employee, Employee subordinate)
    {
        if (employee is not Supervisor supervisor)
        {
            throw NoPermissionException.EmployeeIsNotYourSubordinate(subordinate.Identifier);
        }

        if (!supervisor.Employees.Contains(subordinate))
        {
            throw SubordinateException.SubordinateDoesNotExistException(subordinate.Identifier);
        }

        supervisor.Employees.Remove(subordinate);
    }
}
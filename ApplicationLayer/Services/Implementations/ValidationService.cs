using ApplicationLayer.Exceptions;
using ApplicationLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Implementations;

public class ValidationService : IValidationService
{
    private readonly DatabaseContext _context;
    private CheckAccessEmployeeVisitor _checkAccessEmployeeVisitor;

    public ValidationService(DatabaseContext context)
    {
        _context = context;
        _checkAccessEmployeeVisitor = new CheckAccessEmployeeVisitor();
    }

    public Supervisor CheckAccessForEmployee(Guid supervisorId, Guid subordinateId)
    {
        TemporaryId? id = _context.TemporaryIds.FirstOrDefault(x => x.TempId == supervisorId);
        if (id is null)
        {
            throw NotFoundException.EntityNotFoundException<Employee>(supervisorId);
        }

        supervisorId = id.EmployeeId;
        Employee? employee = _context.Employee.Find(supervisorId);
        switch (employee)
        {
            case null:
                throw NotFoundException.EntityNotFoundException<Employee>(supervisorId);
            case Supervisor supervisor:
            {
                supervisor.Accept(_checkAccessEmployeeVisitor);
                if (!_checkAccessEmployeeVisitor.SubordinateIds.Contains(subordinateId))
                {
                    throw NoPermissionException.EmployeeIsNotYourSubordinate(subordinateId);
                }

                return supervisor;
            }

            default:
                throw NoPermissionException.EmployeeIsNotYourSubordinate(subordinateId);
        }
    }

    public void CheckAccessForMessageSource(Guid employeeId, string sender, string addressee)
    {
        TemporaryId? id = _context.TemporaryIds.FirstOrDefault(x => x.TempId == employeeId);
        if (id is null)
        {
            throw NotFoundException.EntityNotFoundException<Employee>(employeeId);
        }

        employeeId = id.EmployeeId;

        MessageSource messageSource = CheckAddressForNull(sender);

        MessageSource messageSource2 = CheckAddressForNull(addressee);

        Guid messageSourceId = messageSource.Id;
        Employee? employee = _context.Employee.Find(employeeId);
        employee = CheckEntityForNull(employee, employeeId);
        PrivateAccount? privateAccount = _context.PrivateAccounts.FirstOrDefault(x => x.Id == employee.Identifier);
        privateAccount = CheckEntityForNull(privateAccount, employeeId);
        Account? account = privateAccount.Accounts.Find(x =>
            x.MessageSource.Id == messageSourceId || x.Addressees.Contains(messageSource2));

        if (account is null)
        {
            throw NoPermissionException.MessageSourceDoesNotBelongYourAccount(messageSourceId);
        }
    }

    public void CheckAccessForMessage(Guid employeeId, Guid messageId)
    {
        TemporaryId? id = _context.TemporaryIds.FirstOrDefault(x => x.TempId == employeeId);
        if (id is null)
        {
            throw NotFoundException.EntityNotFoundException<Employee>(employeeId);
        }

        employeeId = id.EmployeeId;
        Employee? employee = _context.Employee.Find(employeeId);

        employee = CheckEntityForNull(employee, employeeId);
        PrivateAccount privateAccount = CheckEntityForNull(employee.PrivateAccount, employeeId);
        Account? account = privateAccount.Accounts.Find(x => x.MessageSource.Messages.Any(m => m.Id == messageId));

        if (account is null)
        {
            throw NoPermissionException.MessageDoesNotBelongYourAccount(messageId);
        }
    }

    public T CheckEntityForNull<T>(T? entity, Guid entityId)
    {
        if (entity is null)
        {
            throw NotFoundException.EntityNotFoundException<T>(entityId);
        }

        return entity;
    }

    public T CheckEntityForNull<T>(T? entity, string entityId)
    {
        if (entity is null)
        {
            throw NotFoundException.EntityNotFoundException<T>(entityId);
        }

        return entity;
    }

    public MessageSource CheckAddressForNull(string address)
    {
        MessageSourceIdentifier? messageSourceIdentifier =
            _context.MessageSourceIdentifiers.FirstOrDefault(x => x.Address == address);
        messageSourceIdentifier = CheckEntityForNull(messageSourceIdentifier, address);
        MessageSource? messageSource = _context.MessageSource.Find(messageSourceIdentifier.MessageSourceId);
        return CheckEntityForNull(messageSource, messageSourceIdentifier.MessageSourceId);
    }
}
using ApplicationLayer.Dto;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Mapping;
using ApplicationLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Implementations;

internal class AccountService : IAccountService
{
    private readonly DatabaseContext _context;

    public AccountService(DatabaseContext context)
    {
        _context = context;
    }

    public Task<PrivateAccountDto> Login(string login, string password, CancellationToken cancellationToken)
    {
        PrivateAccount? account = _context.PrivateAccounts.FirstOrDefault(x => x.Login == login);
        if (account is null)
        {
            throw NotFoundException.PrivateAccountNotFoundException(login, password);
        }

        RegisterEmployee(account.Id);

        return Task.FromResult(account.AsDto());
    }

    public Employee GetEmployee(Guid employeeId)
    {
        Employee? employee = _context.Employee.Find(employeeId);
        if (employee is null)
        {
            throw NotFoundException.EntityNotFoundException<Employee>(employeeId);
        }

        return employee;
    }

    public Guid RegisterEmployee(Guid employeeId)
    {
        var tempId = Guid.NewGuid();
        _context.TemporaryIds.Add(new TemporaryId(employeeId, tempId));
        _context.SaveChanges();
        return tempId;
    }
}
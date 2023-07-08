using ApplicationLayer.Dto;
using DataAccessLayer.AbstractClasses;

namespace ApplicationLayer.Services.Interfaces;

public interface IAccountService
{
    Task<PrivateAccountDto> Login(string login, string password, CancellationToken cancellationToken);
    Employee GetEmployee(Guid employeeId);
    Guid RegisterEmployee(Guid employeeId);
}
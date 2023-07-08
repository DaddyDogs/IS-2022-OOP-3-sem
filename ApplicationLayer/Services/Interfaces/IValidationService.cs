using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Interfaces;

public interface IValidationService
{
    Supervisor CheckAccessForEmployee(Guid supervisorId, Guid subordinateId);
    void CheckAccessForMessageSource(Guid employeeId, string sender, string addressee);
    void CheckAccessForMessage(Guid employeeId, Guid messageId);
    T CheckEntityForNull<T>(T? entity, Guid entityId);
    T CheckEntityForNull<T>(T? entity, string entityId);
    MessageSource CheckAddressForNull(string address);
}
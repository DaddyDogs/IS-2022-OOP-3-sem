using ApplicationLayer.Dto;
using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Interfaces;

public interface ISupervisorService
{
    Task<Guid> AddEmployee(string firstName, string secondName, Guid supervisorId, Guid requestingId, string login, string password, CancellationToken cancellationToken);
    Task RemoveEmployee(Guid employeeId, Guid requestingId, CancellationToken cancellationToken);
    Employee? FindEmployee(Guid employeeId);
    Task<Report> MakeReport(Guid requestingId, TimeSpan timeSpan);
    Guid AddAccount(Guid messageSourceId, Guid addressee, Guid employeeId, Guid requestingId);
    List<Report> GetReports(Guid requestingId);
}
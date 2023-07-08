using DataAccessLayer.Models;

namespace ApplicationLayer.Dto;

public record SupervisorDto(string FirstName, string LastName, Guid? SupervisorId, List<Guid?> Employees, Guid PrivateAccount) : EmployeeDto(FirstName, LastName, SupervisorId, PrivateAccount);
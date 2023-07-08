using DataAccessLayer.Models;

namespace ApplicationLayer.Dto;

public record SubordinateDto(string FirstName, string LastName, Guid? SupervisorId, Guid PrivateAccount) : EmployeeDto(FirstName, LastName, SupervisorId, PrivateAccount);
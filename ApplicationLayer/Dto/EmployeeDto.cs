namespace ApplicationLayer.Dto;

public record EmployeeDto(string FirstName, string LastName, Guid? SupervisorId, Guid PrivateAccount);
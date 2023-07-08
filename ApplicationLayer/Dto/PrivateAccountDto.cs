namespace ApplicationLayer.Dto;

public record PrivateAccountDto(string Password, string Login, List<AccountDto> Accounts, Guid EmployeeId, Guid Id);
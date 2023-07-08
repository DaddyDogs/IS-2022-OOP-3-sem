using ApplicationLayer.Dto;

namespace PresentationLayer.Models.Accounts;

public record CreateAccountModel(Guid Password, Guid Login, List<MessageSourceDto> MessageSourceDtos, EmployeeDto Employee);
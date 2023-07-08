using DataAccessLayer.AbstractClasses;

namespace ApplicationLayer.Dto;

public record AccountDto(MessageSource MessageSource, List<MessageSourceDto> Addressees);
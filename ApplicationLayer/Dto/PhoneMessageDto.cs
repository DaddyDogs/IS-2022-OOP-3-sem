using DataAccessLayer.Enums;

namespace ApplicationLayer.Dto;

public record PhoneMessageDto(string Content, Guid Id, DateTime DateTime, MessageState State) : MessageDto(Content);
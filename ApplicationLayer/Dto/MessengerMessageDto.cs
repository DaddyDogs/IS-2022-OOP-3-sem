using DataAccessLayer.Enums;

namespace ApplicationLayer.Dto;

public record MessengerMessageDto(string Content, Guid Id, DateTime DateTime, MessageState State) : MessageDto(Content);
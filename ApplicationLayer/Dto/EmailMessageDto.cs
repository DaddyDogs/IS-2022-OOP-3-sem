using DataAccessLayer.Enums;

namespace ApplicationLayer.Dto;

public record EmailMessageDto(string Content, string? Subject, Guid Id, DateTime DateTime, MessageState State) : MessageDto(Content);
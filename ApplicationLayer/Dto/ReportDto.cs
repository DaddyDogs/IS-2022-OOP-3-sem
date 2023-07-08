namespace ApplicationLayer.Dto;

public record ReportDto(int HandledMessages, Dictionary<MessageSourceDto, int> ReceivedMessages);
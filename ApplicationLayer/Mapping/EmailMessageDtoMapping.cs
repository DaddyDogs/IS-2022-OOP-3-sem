using ApplicationLayer.Dto;

namespace ApplicationLayer.Mapping;

public class EmailMessageDtoMapping
{
    public static MessageDto AsDto(string content) => new MessageDto(content);
}
using ApplicationLayer.Dto;
using DataAccessLayer.AbstractClasses;

namespace ApplicationLayer.Mapping;

public static class MessageSourceDtoMapping
{
    public static MessageSourceDto AsDto(this MessageSource messageSource) =>
        new MessageSourceDto();
}
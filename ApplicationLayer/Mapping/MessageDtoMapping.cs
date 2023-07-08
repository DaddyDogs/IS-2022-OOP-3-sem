using ApplicationLayer.Dto;
using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Models;
namespace ApplicationLayer.Mapping;

public class MessageDtoMapping
{
    public static MessageDto AsDto(Message message)
    {
        return message switch
        {
            EmailMessage emailMessage => new EmailMessageDto(emailMessage.Content, emailMessage.Subject, emailMessage.Id, emailMessage.DateTime, emailMessage.State),
            PhoneMessage phoneMessage => new PhoneMessageDto(phoneMessage.Content, phoneMessage.Id, phoneMessage.DateTime, phoneMessage.State),
            MessengerMessage messengerMessage => new MessengerMessageDto(messengerMessage.Content, messengerMessage.Id, messengerMessage.DateTime, messengerMessage.State),
            _ => throw new Exception(),
        };
    }
}
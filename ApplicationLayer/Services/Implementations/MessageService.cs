using ApplicationLayer.Exceptions;
using ApplicationLayer.Extensions;
using ApplicationLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Enums;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Implementations;

public class MessageService : IMessageService
{
    private readonly IValidationService _validationService;
    private readonly DatabaseContext _context;
    public MessageService(DatabaseContext context)
    {
        _context = context;
        _validationService = new ValidationService(_context);
    }

    public Guid SendMessage(string addressee, string sender, string content, string? subject, Guid requestingId)
    {
        _validationService.CheckAccessForMessageSource(requestingId, sender, addressee);

        MessageSource messageSource = _validationService.CheckAddressForNull(sender);
        Message message = messageSource switch
        {
            Email => new EmailMessage(content, subject),
            Phone => new PhoneMessage(content),
            Messenger => new MessengerMessage(content),
            _ => throw NotFoundException.EntityNotFoundException<MessageSource>(sender),
        };

        MessageSource messageSource2 = _validationService.CheckAddressForNull(addressee);

        messageSource2.Messages.Add(message);
        message.State = MessageState.New;
        _context.SaveChanges();
        return message.Id;
    }

    public void ReplayOnMessage(string addressee, string sender, Guid messageId, string reply, string? subject, Guid requestingId)
    {
        _validationService.CheckAccessForMessage(requestingId, messageId);
        MarkAsRead(messageId, requestingId);
        SendMessage(addressee, sender, reply, subject, requestingId);
        _context.SaveChanges();
    }

    public void MarkAsRead(Guid messageId, Guid requestingId)
    {
        _validationService.CheckAccessForMessage(requestingId, messageId);

        Message? message = _context.Message.Find(messageId);
        message = _validationService.CheckEntityForNull(message, messageId);

        message.State = MessageState.Handled;
        message.DateTime = DateTime.Now;

        _context.Employee.GetEntity(requestingId, CancellationToken.None).Messages.Add(message);
        _context.SaveChanges();
    }

    public void Cancel(Guid requestingId)
    {
        TemporaryId? temporaryId = _context.TemporaryIds.Find(requestingId);
        temporaryId = _validationService.CheckEntityForNull(temporaryId, requestingId);

        _context.TemporaryIds.Remove(temporaryId);
        _context.SaveChanges();
    }
}
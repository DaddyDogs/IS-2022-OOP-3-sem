using ApplicationLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Implementations;

public class MessageSourceService : IMessageSourceService
{
    private readonly DatabaseContext _context;

    public MessageSourceService(DatabaseContext context)
    {
        _context = context;
    }

    public Guid AddEmail(string login)
    {
        var email = new Email(login);
        _context.MessageSource.Add(email);
        _context.MessageSourceIdentifiers.Add(new MessageSourceIdentifier(email.Id, login));
        _context.SaveChanges();
        return email.Id;
    }

    public Guid AddPhone(string number)
    {
        var phone = new Phone(number);
        _context.MessageSource.Add(phone);
        _context.MessageSourceIdentifiers.Add(new MessageSourceIdentifier(phone.Id, number));
        _context.SaveChanges();
        return phone.Id;
    }

    public Guid AddMessenger(string userId)
    {
        var messenger = new Messenger(userId);
        _context.MessageSource.Add(messenger);
        _context.MessageSourceIdentifiers.Add(new MessageSourceIdentifier(messenger.Id, userId));
        _context.SaveChanges();
        return messenger.Id;
    }
}
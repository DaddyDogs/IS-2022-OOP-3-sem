using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Interfaces;

public interface IMessageService
{
    Guid SendMessage(string addressee, string sender, string content, string? subject, Guid requestingId);
    void ReplayOnMessage(string addressee, string sender, Guid messageId, string reply, string? subject, Guid requestingId);
    void MarkAsRead(Guid messageId, Guid requestingId);
    void Cancel(Guid requestingId);
}
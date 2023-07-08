using DataAccessLayer.AbstractClasses;

namespace DataAccessLayer.Models;

public class MessengerMessage : Message
{
    public MessengerMessage(string content)
        : base(content)
    {
    }
    #pragma warning disable CS8618
    protected MessengerMessage() { }
    #pragma warning restore CS8618
}
using DataAccessLayer.AbstractClasses;

namespace DataAccessLayer.Models;

public class EmailMessage : Message
{
    public EmailMessage(string content, string? subject)
        : base(content)
    {
        Subject = subject;
    }

    #pragma warning disable CS8618
    protected EmailMessage()
        : base()
    { }
    #pragma warning restore CS8618
    public string? Subject { get; }
}
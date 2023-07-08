using DataAccessLayer.Enums;

namespace DataAccessLayer.AbstractClasses;

public abstract class Message
{
    public Message(string content)
    {
        Content = content;
        State = MessageState.Unsent;
        DateTime = DateTime.Now;
    }

#pragma warning disable CS8618
    protected Message() { }
#pragma warning restore CS8618
    public virtual string Content { get; }
    public Guid Id { get; set; }
    public virtual DateTime DateTime { get; set; }
    public virtual MessageState State { get; set; }
}
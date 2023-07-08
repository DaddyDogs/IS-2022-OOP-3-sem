namespace DataAccessLayer.AbstractClasses;

public abstract class MessageSource
{
    #pragma warning disable CS8618
    protected MessageSource() { }
    #pragma warning restore CS8618
    public Guid Id { get; set; }
    public virtual List<Message> Messages { get; set; } = new List<Message>();
}
using DataAccessLayer.AbstractClasses;

namespace DataAccessLayer.Models;

public class MessageSourceInfo
{
    public MessageSourceInfo(MessageSource messageSource, int count)
    {
        MessageSource = messageSource;
        Count = count;
        Id = Guid.NewGuid();
    }
    #pragma warning disable CS8618
    protected MessageSourceInfo()
    { }
    #pragma warning restore CS8618
    public Guid Id { get; set; }
    public virtual MessageSource MessageSource { get; set; }
    public virtual int Count { get; set; }
}
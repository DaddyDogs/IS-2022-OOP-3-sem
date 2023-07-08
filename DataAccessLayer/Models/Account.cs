using DataAccessLayer.AbstractClasses;

namespace DataAccessLayer.Models;

public class Account
{
    public Account(MessageSource messageSource, List<MessageSource> addressees)
    {
        MessageSource = messageSource;
        Addressees = addressees;
    }

    #pragma warning disable CS8618
    protected Account() { }
    #pragma warning restore CS8618
    public Guid Id { get; set; }

    public virtual MessageSource MessageSource { get; set; }
    public virtual List<MessageSource> Addressees { get; set; }
}
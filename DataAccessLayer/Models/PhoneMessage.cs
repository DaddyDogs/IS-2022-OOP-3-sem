using DataAccessLayer.AbstractClasses;

namespace DataAccessLayer.Models;

public class PhoneMessage : Message
{
    public PhoneMessage(string content)
        : base(content)
    {
    }
    #pragma warning disable CS8618
    protected PhoneMessage() { }
    #pragma warning restore CS8618
}
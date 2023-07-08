using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class MessageSourceIdentifier
{
    public MessageSourceIdentifier(Guid messageSourceId, string address)
    {
        MessageSourceId = messageSourceId;
        Address = address;
    }

    #pragma warning disable CS8618
    protected MessageSourceIdentifier()
    { }
    #pragma warning restore CS8618

    [Key]
    public Guid MessageSourceId { get; set; }
    public string Address { get; set; }
}
using DataAccessLayer.AbstractClasses;

namespace DataAccessLayer.Models;

public class Messenger : MessageSource
{
    public Messenger(string userId)
    {
        UserId = userId;
    }

    #pragma warning disable CS8618
    protected Messenger() { }
    #pragma warning restore CS8618

    public string UserId { get; }
}
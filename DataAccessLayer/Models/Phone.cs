using DataAccessLayer.AbstractClasses;

namespace DataAccessLayer.Models;

public class Phone : MessageSource
{
    public Phone(string number)
    {
        Number = number;
    }
    #pragma warning disable CS8618
    protected Phone() { }
    #pragma warning restore CS8618
    public string Number { get; }
}
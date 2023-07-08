using System.ComponentModel.DataAnnotations;
using DataAccessLayer.AbstractClasses;

namespace DataAccessLayer.Models;

public class Email : MessageSource
{
    public Email(string username)
    {
        Username = username;
    }

    #pragma warning disable CS8618
    protected Email() { }
    #pragma warning restore CS8618

    public string Username { get; set; }
}
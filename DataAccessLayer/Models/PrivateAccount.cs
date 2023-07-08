using System.ComponentModel.DataAnnotations;
using DataAccessLayer.AbstractClasses;

namespace DataAccessLayer.Models;
public class PrivateAccount
{
    public PrivateAccount(List<Account> accounts, Employee employee, string login, string password)
    {
        Password = password;
        Login = login;
        Accounts = accounts;
        Employee = employee;
    }

    #pragma warning disable CS8618
    protected PrivateAccount() { }
    #pragma warning restore CS8618
    public virtual List<Account> Accounts { get; set; } = new List<Account>(0);
    [Key]
    public Guid Id { get; set; }
    public string Login { get; set; }
    public virtual Employee Employee { get; set; }
    public string Password { get; set; }
}
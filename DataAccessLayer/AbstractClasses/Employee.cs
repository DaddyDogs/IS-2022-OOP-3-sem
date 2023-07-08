using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.AbstractClasses;

public abstract class Employee
{
    public Employee(string firstName, string lastName, Supervisor? supervisor)
    {
        FirstName = firstName;
        LastName = lastName;
        Supervisor = supervisor;
    }
#pragma warning disable CS8618
    protected Employee()
    {
    }
#pragma warning restore CS8618
    public virtual string FirstName { get; }
    public virtual string LastName { get; }
    public virtual Supervisor? Supervisor { get; set; }
    public virtual PrivateAccount? PrivateAccount { get; set; }
    public virtual List<Message> Messages { get; set; } = new List<Message>(0);
    [Key]
    public Guid Identifier { get; set; }

    public abstract void Accept(IEmployeeVisitor visitor);
}
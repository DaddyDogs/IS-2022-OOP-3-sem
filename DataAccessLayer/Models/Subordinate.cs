using System.ComponentModel.DataAnnotations;
using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models;

public class Subordinate : Employee
{
    public Subordinate(string firstName, string lastName, Supervisor supervisor)
        : base(firstName, lastName, supervisor)
    {
        Head = supervisor;
    }
    #pragma warning disable CS8618
    protected Subordinate()
    { }
    #pragma warning restore CS8618
    public virtual Supervisor Head { get; }

    public override void Accept(IEmployeeVisitor visitor)
    {
        visitor.Visit(this);
    }
}
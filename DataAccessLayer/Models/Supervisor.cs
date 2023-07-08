using System.ComponentModel.DataAnnotations;
using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Models;

public class Supervisor : Employee
{
    public Supervisor(string firstName, string lastName, Supervisor? supervisor)
        : base(firstName, lastName, supervisor)
    {
        Head = supervisor;
    }
    #pragma warning disable CS8618
    protected Supervisor() { }
    #pragma warning restore CS8618
    public virtual Supervisor? Head { get; }
    public virtual List<Employee> Employees { get; set; } = new List<Employee>(0);

    public override void Accept(IEmployeeVisitor visitor)
    {
        foreach (Employee employee in Employees)
        {
            employee.Accept(visitor);
        }

        visitor.Visit(this);
    }
}
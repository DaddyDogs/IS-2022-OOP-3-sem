using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces;

public interface IEmployeeVisitor
{
    void Visit(Employee employee);
}
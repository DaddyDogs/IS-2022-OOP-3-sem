using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Implementations;

public class CheckAccessEmployeeVisitor : IEmployeeVisitor
{
    private readonly List<Guid> _subordinateIds;

    public CheckAccessEmployeeVisitor()
    {
        _subordinateIds = new List<Guid>(0);
    }

    public IReadOnlyList<Guid> SubordinateIds => _subordinateIds;
    public void Visit(Employee employee)
    {
        _subordinateIds.Add(employee.Identifier);
    }
}
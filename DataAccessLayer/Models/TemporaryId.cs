using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class TemporaryId
{
    public TemporaryId(Guid employeeId, Guid tempId)
    {
        EmployeeId = employeeId;
        TempId = tempId;
    }

    [Key]
    public Guid TempId { get; set; }
    public Guid EmployeeId { get; set; }
}
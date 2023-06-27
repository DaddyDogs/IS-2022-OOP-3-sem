using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class IsgStream
{
    private readonly List<StudentDecorator> _students;

    public IsgStream(string groupName, Schedule schedule, MegaFaculties megaFaculty)
    {
        _students = new List<StudentDecorator>(0);
        GroupName = groupName;
        Schedule = schedule;
        MegaFaculty = megaFaculty;
    }

    public string GroupName { get;  }
    public Schedule Schedule { get; }
    public int MaxStudentNumber { get; } = 30;
    public MegaFaculties MegaFaculty { get; }
    public IReadOnlyList<StudentDecorator> Students => _students;
    public void AddStudent(StudentDecorator student)
    {
        if (Students.Count >= Group.MaxStudentNumber)
        {
            throw GroupException.FullGroupException(GroupName);
        }

        if (Students.Contains(student))
        {
            throw GroupException.StudentIsAlreadyInThisGroupException(student.Student.Name, student.Student.Id, GroupName);
        }

        _students.Add(student);
    }

    public void RemoveStudent(StudentDecorator student)
    {
        if (!Students.Contains(student))
        {
            throw GroupException.StudentIsNotInThisGroupException(student.Student.Name, student.Student.Id, GroupName);
        }

        _students.Remove(student);
    }
}
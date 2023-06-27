using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Models;

namespace Isu.Extra.Services;

public class StudentDecoratorService
{
    private readonly List<StudentDecorator> _listStudent;
    private readonly IdGenerator _idGenerator;

    public StudentDecoratorService()
    {
        _listStudent = new List<StudentDecorator>(0);
        _idGenerator = new IdGenerator();
    }

    public StudentDecorator AddStudent(GroupDecorator group, string name)
    {
        var newStudent = new Student(name, group.Group, _idGenerator.GetNewId());
        var newStudentDecorator = new StudentDecorator(newStudent, group);
        _listStudent.Add(newStudentDecorator);
        return newStudentDecorator;
    }

    public StudentDecorator GetStudent(int id)
    {
        StudentDecorator? newStudent = FindStudent(id);
        if (newStudent == null)
        {
            throw StudentDecoratorException.NonExistentStudentException(id);
        }

        return newStudent;
    }

    public StudentDecorator? FindStudent(int id)
    {
        if (id > _idGenerator.Id)
        {
            throw StudentDecoratorException.NonExistentStudentException(id);
        }

        return _listStudent.Find(student => student.Student.Id == id);
    }

    public List<Student> GetUnregisteredStudents(Group group)
    {
        var requiredList = new List<Student>(0);
        requiredList.AddRange(group.Students
            .Select(student => new { student, studentD = _listStudent.Find(s => s.Student.Id == student.Id) })
            .Where(@t => @t.studentD is null || @t.studentD.IsgStream.Count == 0)
            .Select(@t => @t.student));

        return requiredList;
    }
}
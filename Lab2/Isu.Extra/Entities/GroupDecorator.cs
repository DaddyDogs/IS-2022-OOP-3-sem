using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class GroupDecorator
{
    public GroupDecorator(GroupName groupName, Schedule schedule)
    {
        Group = new Group(groupName);
        Schedule = schedule;
    }

    public Group Group { get; }
    public Schedule Schedule { get; }
    public void AddStudent(Student student)
    {
        if (Group.Students.Count >= Group.MaxStudentNumber)
        {
            throw GroupException.FullGroupException(Group.GroupName.GetString());
        }

        if (Group.Students.Contains(student))
        {
            throw GroupException.StudentIsAlreadyInThisGroupException(student.Name, student.Id, Group.GroupName.GetString());
        }

        Group.AddStudent(student);
    }

    public void RemoveStudent(Student student)
    {
        if (!Group.Students.Contains(student))
        {
            throw GroupException.StudentIsNotInThisGroupException(student.Name, student.Id, Group.GroupName.GetString());
        }

        Group.RemoveStudent(student);
    }

    public MegaFaculties GetMegaFaculty()
    {
        char faculty = Group.GroupName.GetString()[0];
        return faculty switch
        {
            'M' or 'K' or 'J' or 'C' => MegaFaculties.Tint,
            'R' or 'P' or 'N' or 'H' or 'U' => MegaFaculties.Ctm,
            'W' => MegaFaculties.BTaLts,
            'L' or 'Q' or 'V' or 'Z' => MegaFaculties.Pt,
            'A' or 'G' or 'O' or 'T' => MegaFaculties.Ls,
            _ => throw MegaFacultyException.NonExistentGroupException(faculty)
        };
    }
}
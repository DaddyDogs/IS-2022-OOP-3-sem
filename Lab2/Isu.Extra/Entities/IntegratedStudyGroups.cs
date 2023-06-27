using Isu.Extra.Exceptions;
using Isu.Extra.Models;
namespace Isu.Extra.Entities;

public class IntegratedStudyGroups
{
    private readonly List<IsgStream> _groups;

    public IntegratedStudyGroups(string name, MegaFaculties megaFaculty)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw IntegratedStudyGroupsException.InvalidCourseNameException(name);
        }

        Name = name;
        MegaFaculty = megaFaculty;
        _groups = new List<IsgStream>();
    }

    public IReadOnlyList<IsgStream> Groups => _groups;
    public string Name { get; }
    public MegaFaculties MegaFaculty { get; }

    public IsgStream AddStream(string groupName, Schedule schedule)
    {
        var newStream = new IsgStream(groupName, schedule, MegaFaculty);
        _groups.Add(newStream);
        return newStream;
    }

    public IEnumerable<StudentDecorator> GetIsgStream()
    {
        IEnumerable<StudentDecorator> requiredList = Groups.SelectMany(s => s.Students);

        if (!requiredList.Any())
        {
            throw IntegratedStudyGroupsException.CourseHasNoStudents(Name);
        }

        return requiredList;
    }
}
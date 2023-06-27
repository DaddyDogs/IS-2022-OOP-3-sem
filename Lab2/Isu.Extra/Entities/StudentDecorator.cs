using Isu.Entities;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class StudentDecorator
{
    public StudentDecorator(Student student, GroupDecorator group)
    {
        Student = student;
        Group = group;
        IsgStream = new List<IsgStream>(0);
    }

    public Student Student { get; }
    public GroupDecorator Group { get; private set; }
    public List<IsgStream> IsgStream { get; }

    public void ChangeGroup(GroupDecorator newGroup)
    {
        if (Group == newGroup)
        {
            throw GroupException.StudentIsAlreadyInThisGroupException(Student.Name, Student.Id, Student.Group.GroupName.GetString());
        }

        newGroup.AddStudent(Student);
        Group.RemoveStudent(Student);
        Group = newGroup;
    }

    public Schedule GetSchedule()
    {
        Schedule newSchedule = Group.Schedule;
        if (IsgStream.Count > 0)
        {
            foreach (Lesson.Lesson lesson in IsgStream[0].Schedule.Lessons)
            {
                newSchedule.AddLesson(lesson);
            }
        }

        if (IsgStream.Count <= 1) return newSchedule;
        foreach (Lesson.Lesson lesson in IsgStream[1].Schedule.Lessons)
        {
            newSchedule.AddLesson(lesson);
        }

        return newSchedule;
    }

    public void RegisterStudent(IsgStream isgStream)
    {
        if (isgStream.Students.Count >= isgStream.MaxStudentNumber)
        {
            throw IsgStreamException.FullGroupException(isgStream);
        }

        if (IsgStream.Count > 1 && IsgStream[0] == isgStream)
        {
            throw StudentDecoratorException.StudentIsAlreadyInThisStream(Student.Id, isgStream.GroupName);
        }

        if (IsgStream.Count >= 2)
        {
            throw StudentDecoratorException.TooManyIsgException(Student.Id);
        }

        if (Group.GetMegaFaculty() == isgStream.MegaFaculty)
        {
            throw MegaFacultyException.InvalidIsgChoiceException(isgStream.GroupName);
        }

        Schedule schedule = GetSchedule();

        if (isgStream.Schedule.Lessons.Any(lesson => schedule.Lessons.Any(l => l.Equals(lesson))))
        {
            throw ScheduleException.ScheduleIntersectionException(isgStream.GroupName);
        }

        isgStream.AddStudent(this);
        IsgStream.Add(isgStream);
    }

    public void UnregisterStudent(IsgStream isgStream)
    {
        isgStream.RemoveStudent(this);
        IsgStream.Remove(isgStream);
    }
}
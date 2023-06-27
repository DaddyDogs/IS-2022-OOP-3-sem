using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Entities.Lesson;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraServiceTest
{
    private readonly IsuExtraService _isuExtraService;
    private readonly GroupDecoratorService _groupDecoratorService;
    private readonly StudentDecoratorService _studentDecoratorService;

    public IsuExtraServiceTest()
    {
        _isuExtraService = new IsuExtraService();
        _groupDecoratorService = new GroupDecoratorService();
        _studentDecoratorService = new StudentDecoratorService();
    }

    [Fact]
    public void RegisterStudentOnIsg_IsgStreamContainsStudent_StudentHasIsgStream()
    {
        const string studentName = "Vortan Surenov";
        var groupName = new GroupName('M', 3, new CourseNumber(1), "02", 1);
        var professor = new Professor("Fredi Cups");
        Lesson lesson1 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(new AllowedTime(8, 20)).WithProfessor(professor).WithAuditorium("228").Build();
        Lesson lesson2 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(new AllowedTime(10, 00)).WithProfessor(professor).WithAuditorium("228").Build();
        Lesson lesson3 = Lesson.Builder.WithName("English").WithDay(WeekDay.Monday).WithTime(new AllowedTime(10, 00)).WithProfessor(professor).WithAuditorium("228").Build();

        var schedule1 = new Schedule();
        schedule1.AddLesson(lesson1);
        schedule1.AddLesson(lesson3);

        var schedule2 = new Schedule();
        schedule2.AddLesson(lesson2);

        GroupDecorator group = _groupDecoratorService.AddGroup(groupName, schedule1);
        StudentDecorator student = _studentDecoratorService.AddStudent(group, studentName);

        IntegratedStudyGroups newIsg = _isuExtraService.AddIntegratedStudyGroups("Cybersafety", MegaFaculties.Ctm);
        IsgStream newStream = newIsg.AddStream("CS1.1", schedule2);

        student.RegisterStudent(newStream);

        Assert.Contains(student, newStream.Students);
        Assert.Equal(student.IsgStream[0], newStream);
    }

    [Fact]
    public void UnregisterStudentFromIsg_IsgStreamDoesNotContainStudent_StudentHasNotIsgStream_GetUnregisteredStudents()
    {
        const string studentName = "Vortan Surenov";
        var groupName = new GroupName('M', 3, new CourseNumber(1), "02", 1);
        var professor = new Professor("Fredi Cups");
        Lesson lesson1 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(new AllowedTime(8, 20)).WithProfessor(professor).WithAuditorium("228").Build();
        Lesson lesson2 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(new AllowedTime(10, 00)).WithProfessor(professor).WithAuditorium("228").Build();

        var schedule1 = new Schedule();
        schedule1.AddLesson(lesson1);

        var schedule2 = new Schedule();
        schedule2.AddLesson(lesson2);

        GroupDecorator group = _groupDecoratorService.AddGroup(groupName, schedule1);
        StudentDecorator student = _studentDecoratorService.AddStudent(group, studentName);

        IntegratedStudyGroups newIsg = _isuExtraService.AddIntegratedStudyGroups("Cybersafety", MegaFaculties.Ctm);
        IsgStream newStream = newIsg.AddStream("CS1.1", schedule2);

        student.RegisterStudent(newStream);
        student.UnregisterStudent(newStream);

        Assert.DoesNotContain(student, newStream.Students);
        Assert.DoesNotContain(newStream, student.IsgStream);

        List<Student> unregisteredStudents = _studentDecoratorService.GetUnregisteredStudents(group.Group);

        Assert.Contains(student.Student, unregisteredStudents);
    }

    [Fact]
    public void RegisterStudentOnHisMegaFacultyIsg_ThrowMegaFacultyException()
    {
        const string studentName = "Vortan Surenov";
        var groupName = new GroupName('M', 3, new CourseNumber(1), "02", 1);
        var professor = new Professor("Fredi Cups");
        Lesson lesson1 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(new AllowedTime(8, 20)).WithProfessor(professor).WithAuditorium("228").Build();
        Lesson lesson2 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(new AllowedTime(10, 00)).WithProfessor(professor).WithAuditorium("228").Build();

        var schedule1 = new Schedule();
        schedule1.AddLesson(lesson1);

        var schedule2 = new Schedule();
        schedule2.AddLesson(lesson2);

        GroupDecorator group = _groupDecoratorService.AddGroup(groupName, schedule1);
        StudentDecorator student = _studentDecoratorService.AddStudent(group, studentName);

        IntegratedStudyGroups newIsg = _isuExtraService.AddIntegratedStudyGroups("ML", MegaFaculties.Tint);
        IsgStream newStream = newIsg.AddStream("CS1.1", schedule2);

        Exception exception = Assert.Throws<MegaFacultyException>(() => student.RegisterStudent(newStream));
        Assert.IsType<MegaFacultyException>(exception);
    }

    [Fact]
    public void ReachMaxStudentPerIsgGroup_ThrowIsgGroupException()
    {
        const string studentName = "Vortan Surenov";
        var groupName = new GroupName('R', 3, new CourseNumber(1), "02", 1);
        var groupName2 = new GroupName('R', 3, new CourseNumber(1), "04", 1);
        var professor = new Professor("Fredi Cups");

        Lesson lesson1 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(new AllowedTime(8, 20)).WithProfessor(professor).WithAuditorium("228").Build();
        Lesson lesson2 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(new AllowedTime(10, 00)).WithProfessor(professor).WithAuditorium("228").Build();

        var schedule1 = new Schedule();
        schedule1.AddLesson(lesson1);

        var schedule2 = new Schedule();
        schedule2.AddLesson(lesson2);

        GroupDecorator group = _groupDecoratorService.AddGroup(groupName, schedule1);
        GroupDecorator group2 = _groupDecoratorService.AddGroup(groupName2, schedule1);

        IntegratedStudyGroups newIsg = _isuExtraService.AddIntegratedStudyGroups("ML", MegaFaculties.Tint);
        IsgStream newStream = newIsg.AddStream("CS1.1", schedule2);

        for (int i = 0; i < 30; i++)
        {
            StudentDecorator newStudent = _studentDecoratorService.AddStudent(group, studentName);
            newStudent.RegisterStudent(newStream);
        }

        StudentDecorator newStudent2 = _studentDecoratorService.AddStudent(group2, studentName);
        Exception exception = Assert.Throws<IsgStreamException>(() => newStudent2.RegisterStudent(newStream));
        Assert.IsType<IsgStreamException>(exception);
    }

    [Fact]
    public void RegisterStudentOnIsgWithIntersections_ThrowScheduleException()
    {
        const string studentName = "Vortan Surenov";
        var groupName = new GroupName('M', 3, new CourseNumber(1), "02", 1);
        var professor = new Professor("Fredi Cups");
        var time = new AllowedTime(8, 20);

        Lesson lesson1 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(time).WithProfessor(professor).WithAuditorium("228").Build();
        Lesson lesson2 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(time).WithProfessor(professor).WithAuditorium("228").Build();
        Lesson lesson3 = Lesson.Builder.WithName("French").WithDay(WeekDay.Monday).WithTime(time).WithProfessor(professor).WithAuditorium("228").WithWeekNumber(2).Build();

        var schedule1 = new Schedule();
        schedule1.AddLesson(lesson1);

        var schedule2 = new Schedule();
        schedule2.AddLesson(lesson2);

        GroupDecorator group = _groupDecoratorService.AddGroup(groupName, schedule1);
        StudentDecorator student = _studentDecoratorService.AddStudent(group, studentName);

        IntegratedStudyGroups newIsg = _isuExtraService.AddIntegratedStudyGroups("Cybersafety", MegaFaculties.Ctm);
        IsgStream newStream = newIsg.AddStream("CS1.1", schedule2);

        Exception exception = Assert.Throws<ScheduleException>(() => student.RegisterStudent(newStream));
        Assert.IsType<ScheduleException>(exception);

        schedule2.RemoveLesson(lesson2);
        schedule2.AddLesson(lesson3);

        exception = Assert.Throws<ScheduleException>(() => student.RegisterStudent(newStream));
        Assert.IsType<ScheduleException>(exception);
    }
}
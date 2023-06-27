namespace Isu.Extra.Exceptions;

public class IntegratedStudyGroupsException : IsuExtraException
{
    private IntegratedStudyGroupsException(string message)
        : base(message) { }
    public static IntegratedStudyGroupsException InvalidCourseNameException(string courseName)
    {
        return new IntegratedStudyGroupsException($"Isg course's name {courseName} is invalid. It cannot be empty.");
    }

    public static IntegratedStudyGroupsException CourseHasNoStudents(string courseName)
    {
        return new IntegratedStudyGroupsException($"Isg course {courseName} has no registered students");
    }
}
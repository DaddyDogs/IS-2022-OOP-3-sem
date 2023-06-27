namespace Isu.Extra.Exceptions;

public class LessonException : IsuExtraException
{
    private LessonException(string message)
        : base(message)
    {
    }

    public static LessonException InvalidNameException(string name)
    {
        return new LessonException($"Name cannot be empty. Name {name} is invalid.");
    }

    public static LessonException InvalidDayException()
    {
        return new LessonException("Please no");
    }

    public static LessonException InvalidTimeException(string time)
    {
        return new LessonException($"Time {time} is invalid. Lessons should being held between 8:20 and 20:20 and start at allowed time");
    }

    public static LessonException InvalidAuditoriumNumberException(string auditoriumNumber)
    {
        return new LessonException($"Number {auditoriumNumber} is invalid. It cannot be empty");
    }

    public static LessonException InvalidWeekNumberException(int weekNumber)
    {
        return new LessonException($"Number {weekNumber} is invalid. It should be 1 or 2");
    }
}
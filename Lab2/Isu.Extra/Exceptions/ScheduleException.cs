using Isu.Extra.Entities;
using Isu.Models;

namespace Isu.Extra.Exceptions;

public class ScheduleException : IsuExtraException
{
    private ScheduleException(string message)
        : base(message)
    {
    }

    public static ScheduleException NoScheduleException(string groupName)
    {
        return new ScheduleException($"Group {groupName} has no schedule");
    }

    public static ScheduleException ScheduleIntersectionException(string name)
    {
        return new ScheduleException($"Lesson's time {name} of intersects with the schedule");
    }

    public static ScheduleException NonexistentLessonException(string name)
    {
        return new ScheduleException($"{name} does not exist in this schedule. Cannot remove it.");
    }
}
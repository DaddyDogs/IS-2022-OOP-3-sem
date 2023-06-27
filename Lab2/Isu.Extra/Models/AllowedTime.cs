using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class AllowedTime
{
    private static readonly List<TimeOnly> AllowedTimeList = new List<TimeOnly>()
    {
        new TimeOnly(8, 20), new TimeOnly(10, 0),
        new TimeOnly(11, 40), new TimeOnly(13, 10), new TimeOnly(15, 20), new TimeOnly(17, 0),
        new TimeOnly(18, 40), new TimeOnly(20, 20),
    };

    public AllowedTime(int hour, int minute)
    {
        var time = new TimeOnly(hour, minute);
        if (!AllowedTimeList.Contains(time))
        {
            throw LessonException.InvalidTimeException(time.ToString());
        }

        Time = time;
    }

    public TimeOnly Time { get; }

    public override bool Equals(object? obj)
    {
        if (obj is not TimeOnly time) return false;
        if (Time.Hour != time.Hour)
        {
            return false;
        }

        return Time.Minute == time.Minute;
    }

    public override int GetHashCode()
    {
        return Time.Hour ^ Time.Minute;
    }
}
using Isu.Extra.Models;

namespace Isu.Extra.Entities.Lesson;

public interface IDayBuilder
{
    ITimeBuilder WithDay(WeekDay day);
}
namespace Isu.Extra.Entities.Lesson;

public interface ILessonBuilder
{
    ILessonBuilder WithWeekNumber(int weekNumber);
    Lesson Build();
}
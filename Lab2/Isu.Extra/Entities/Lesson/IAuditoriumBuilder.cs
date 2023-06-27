namespace Isu.Extra.Entities.Lesson;

public interface IAuditoriumBuilder
{
    ILessonBuilder WithAuditorium(string auditoriumNumber);
}
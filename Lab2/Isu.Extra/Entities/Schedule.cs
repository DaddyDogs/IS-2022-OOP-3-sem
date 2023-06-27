using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class Schedule
{
    private readonly List<Lesson.Lesson> _lessons;

    public Schedule()
    {
        _lessons = new List<Lesson.Lesson>(0);
    }

    public IReadOnlyList<Lesson.Lesson> Lessons => _lessons;

    public void AddLesson(Lesson.Lesson lesson)
    {
        if (_lessons.Any(l => l.Equals(lesson)))
        {
            throw ScheduleException.ScheduleIntersectionException(lesson.Name);
        }

        _lessons.Add(lesson);
    }

    public void RemoveLesson(Lesson.Lesson lesson)
    {
        if (!_lessons.Any(l => l.Equals(lesson)))
        {
            throw ScheduleException.NonexistentLessonException(lesson.Name);
        }

        _lessons.Remove(lesson);
    }
}
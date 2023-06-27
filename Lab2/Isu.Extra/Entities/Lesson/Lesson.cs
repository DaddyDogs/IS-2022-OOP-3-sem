using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities.Lesson;
public class Lesson
{
    public Lesson(AllowedTime time, string lesson, Professor professor, string auditoriumNumber, WeekDay day, int weekNumber)
    {
        Time = time;
        Name = lesson;
        Professor = professor;
        AuditoriumNumber = auditoriumNumber;
        Day = day;
        WeekNumber = weekNumber;
    }

    public static INameBuilder Builder => new LessonBuilder();

    public AllowedTime Time { get; }
    public string Name { get; }
    public Professor Professor { get; }
    public string AuditoriumNumber { get; }
    public WeekDay Day { get; }
    public int WeekNumber { get; }

    public bool IsEqual(Lesson lesson)
    {
        if (WeekNumber != lesson.WeekNumber && WeekNumber != 0 && lesson.WeekNumber != 0)
        {
            return false;
        }

        return Day == lesson.Day && Equals(Time, lesson.Time);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Lesson lesson) return false;
        if (WeekNumber != lesson.WeekNumber && (WeekNumber != 0 && lesson.WeekNumber != 0))
        {
            return false;
        }

        if (Day != lesson.Day)
        {
            return false;
        }

        return Time == lesson.Time;
    }

    public override int GetHashCode()
    {
        return WeekNumber ^ (int)Day ^ Time.Time.Hour ^ Time.Time.Minute;
    }

    public class LessonBuilder : INameBuilder, IDayBuilder, ITimeBuilder, IProfessorBuilder, IAuditoriumBuilder, ILessonBuilder
    {
        private AllowedTime? _time;
        private string? _name;
        private Professor? _professor;
        private string? _auditoriumNumber;
        private WeekDay _day;
        private int _weekNumber;

        public IDayBuilder WithName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw LessonException.InvalidNameException(name);
            }

            _name = name;
            return this;
        }

        public ITimeBuilder WithDay(WeekDay day)
        {
            if (day is WeekDay.Sunday)
            {
                throw LessonException.InvalidDayException();
            }

            _day = day;
            return this;
        }

        public IProfessorBuilder WithTime(AllowedTime time)
        {
            if (time.Time.IsBetween(new TimeOnly(20, 20), new TimeOnly(8, 20)))
            {
                throw LessonException.InvalidTimeException(time.Time.ToString());
            }

            _time = time;
            return this;
        }

        public IAuditoriumBuilder WithProfessor(Professor professor)
        {
            if (string.IsNullOrEmpty(professor.Name))
            {
                throw LessonException.InvalidNameException(professor.Name);
            }

            _professor = professor;
            return this;
        }

        public ILessonBuilder WithAuditorium(string auditoriumNumber)
        {
            if (string.IsNullOrEmpty(auditoriumNumber))
            {
                throw LessonException.InvalidAuditoriumNumberException(auditoriumNumber);
            }

            _auditoriumNumber = auditoriumNumber;
            return this;
        }

        public ILessonBuilder WithWeekNumber(int weekNumber)
        {
            if (_weekNumber != 1 && weekNumber != 2)
            {
                throw LessonException.InvalidWeekNumberException(weekNumber);
            }

            _weekNumber = weekNumber;

            return this;
        }

        public Lesson Build()
        {
            if (_name is null || _professor is null || _auditoriumNumber is null || _time is null)
            {
                throw new NullReferenceException();
            }

            return new Lesson(_time, _name, _professor, _auditoriumNumber, _day, _weekNumber);
        }
    }
}
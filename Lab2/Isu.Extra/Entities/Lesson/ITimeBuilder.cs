using Isu.Extra.Models;

namespace Isu.Extra.Entities.Lesson;

public interface ITimeBuilder
{
    IProfessorBuilder WithTime(AllowedTime time);
}
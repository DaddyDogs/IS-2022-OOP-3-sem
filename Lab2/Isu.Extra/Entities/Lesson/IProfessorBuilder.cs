namespace Isu.Extra.Entities.Lesson;

public interface IProfessorBuilder
{
    IAuditoriumBuilder WithProfessor(Professor professor);
}
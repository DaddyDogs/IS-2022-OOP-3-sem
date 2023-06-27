using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class StudentDecoratorException : IsuExtraException
{
    public StudentDecoratorException(string message)
        : base(message)
    {
    }

    public static StudentDecoratorException NonExistentStudentException(int id)
    {
        return new StudentDecoratorException($"Student with id {id} does not exist");
    }

    public static StudentDecoratorException TooManyIsgException(int id)
    {
        return new StudentDecoratorException($"Student with id {id} has already 2 integrated study groups. Cannot register at another one.");
    }

    public static StudentDecoratorException StudentIsAlreadyInThisStream(int id, string streamName)
    {
        return new StudentDecoratorException($"Student with id {id} has already registered on {streamName}.");
    }
}
namespace ApplicationLayer.Exceptions;

public class SubordinateException : ApplicationException
{
    private SubordinateException(string? message)
        : base(message) { }

    public static SubordinateException SubordinateAlreadyExistsException(Guid id)
    {
        return new SubordinateException($"Subordinate with id {id} already exists. Can't add twice");
    }

    public static SubordinateException SubordinateDoesNotExistException(Guid id)
    {
        return new SubordinateException($"Subordinate with id {id} does not exist and cannot be removed");
    }
}
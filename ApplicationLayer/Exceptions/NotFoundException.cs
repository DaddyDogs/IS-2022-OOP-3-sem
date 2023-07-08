namespace ApplicationLayer.Exceptions;

public class NotFoundException : ApplicationException
{
    private NotFoundException(string? message)
        : base(message) { }

    public static NotFoundException EntityNotFoundException<T>(Guid id)
    {
        return new NotFoundException($"Entity {typeof(T)} with id {id} does not exist");
    }

    public static NotFoundException EntityNotFoundException<T>(string id)
    {
        return new NotFoundException($"Entity {typeof(T)} with id {id} does not exist");
    }

    public static NotFoundException PrivateAccountNotFoundException(string login, string password)
    {
        return new NotFoundException($"Private account of employee with login {login} and password {password} does not exist");
    }
}
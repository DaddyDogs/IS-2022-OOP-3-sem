namespace Banks.Exceptions;

public class ClientException : BanksException
{
    private ClientException(string message)
        : base(message) { }

    public static ClientException AccountIsAlreadyRegistered(Guid id)
    {
        return new ClientException($"Account with id {id} is already registered for this client");
    }
}
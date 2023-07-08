namespace Banks.Exceptions;

public class ClientStateException : BanksException
{
    private ClientStateException(string message)
        : base(message) { }

    public static ClientStateException RestrictedTransactionException(decimal amount)
    {
        throw new ClientStateException($"Can't withdraw {amount} rubles before filling out your account");
    }
}
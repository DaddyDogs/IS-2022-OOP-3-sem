namespace Banks.Exceptions;

public class TransactionException : BanksException
{
    private TransactionException(string message)
        : base(message) { }

    public static TransactionException ImpossibleCancellation()
    {
        return new TransactionException($"Can't cancel transaction. It has not accomplished yet");
    }
}
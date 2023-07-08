namespace Banks.Exceptions;

public class DebitAccountException : BanksException
{
    private DebitAccountException(string message)
        : base(message) { }

    public static DebitAccountException InsufficientFundsException(decimal amount)
    {
        throw new DebitAccountException($"There are insufficient funds on your account. Withdraw of {amount} rubles is denied.");
    }
}
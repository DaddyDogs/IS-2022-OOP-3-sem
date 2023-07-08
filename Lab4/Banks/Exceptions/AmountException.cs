namespace Banks.Exceptions;

public class AmountException : BanksException
{
    private AmountException(string message)
        : base(message) { }

    public static AmountException InvalidAmountException(decimal amount)
    {
        throw new AmountException($"Amount {amount} is invalid. It can't be under 0");
    }
}
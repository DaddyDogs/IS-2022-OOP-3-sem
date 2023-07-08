namespace Banks.Exceptions;

public class CreditAccountException : BanksException
{
    private CreditAccountException(string message)
        : base(message) { }

    public static CreditAccountException OverstepLimitException(decimal amount)
    {
        if (amount < 0)
        {
            return new CreditAccountException($"Amount {amount} is not allowed because it's below the limit");
        }

        return new CreditAccountException($"Amount {amount} is not allowed because it exceeds the limit");
    }

    public static CreditAccountException HasNoClockException()
    {
        return new CreditAccountException($"Credit account has no interest percent. You don't need clock for it");
    }
}
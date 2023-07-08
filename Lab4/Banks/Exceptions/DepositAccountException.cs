namespace Banks.Exceptions;

public class DepositAccountException : BanksException
{
    private DepositAccountException(string message)
        : base(message) { }

    public static DepositAccountException DeniedWithdrawingException(DateTime expiryDate)
    {
        throw new DepositAccountException($"Can't withdraw money before account's expiry date {expiryDate.ToString()}");
    }

    public static DepositAccountException InvalidDepositAmount(decimal amount)
    {
        throw new DepositAccountException($"Can't create account with deposit {amount} because it's over the limit");
    }
}
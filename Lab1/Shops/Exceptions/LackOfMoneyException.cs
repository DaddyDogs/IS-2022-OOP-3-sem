namespace Shops.Exceptions;

public class LackOfMoneyException : Exceptions
{
    public LackOfMoneyException() { }
    public LackOfMoneyException(string message)
        : base(message) { }
    public LackOfMoneyException(string message, Exception innerException)
        : base(message, innerException) { }
}
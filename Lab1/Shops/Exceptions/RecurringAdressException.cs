namespace Shops.Exceptions;

public class RecurringAdressException : ShopAlreadyExistsException
{
    public RecurringAdressException() { }
    public RecurringAdressException(string message)
        : base(message) { }
    public RecurringAdressException(string message, Exception innerException)
        : base(message, innerException) { }
}
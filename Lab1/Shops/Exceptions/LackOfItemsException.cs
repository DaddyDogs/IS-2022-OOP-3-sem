namespace Shops.Exceptions;

public class LackOfItemsException : Exceptions
{
    public LackOfItemsException() { }
    public LackOfItemsException(string message)
        : base(message) { }
    public LackOfItemsException(string message, Exception innerException)
        : base(message, innerException) { }
}
namespace Shops.Exceptions;

public class ShopAlreadyExistsException : Exceptions
{
    public ShopAlreadyExistsException() { }
    public ShopAlreadyExistsException(string message)
        : base(message) { }
    public ShopAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException) { }
}
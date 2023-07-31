namespace Shops.Exceptions;

public class ShopExceptions : Exception
{
    public ShopExceptions() { }
    public ShopExceptions(string message)
        : base(message) { }
}
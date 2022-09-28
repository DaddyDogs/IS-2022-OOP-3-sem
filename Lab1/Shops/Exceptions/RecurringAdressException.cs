namespace Shops.Exceptions;

public class RecurringAddressException : ShopExceptions
{
    public RecurringAddressException()
        : base("Shop located to this address already exists") { }
}
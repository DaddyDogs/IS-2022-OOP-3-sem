namespace Shops.Exceptions;

public class LackOfMoneyException : ShopExceptions
{
    public LackOfMoneyException()
        : base("Card account does not have enough credit") { }
}
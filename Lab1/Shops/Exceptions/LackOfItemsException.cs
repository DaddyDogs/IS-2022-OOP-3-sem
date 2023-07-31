using Shops.Entities;
namespace Shops.Exceptions;

public class LackOfItemsException : ShopExceptions
{
    private LackOfItemsException(string message)
        : base(message) { }

    public static LackOfItemsException LackOfItemsInShopException(Item item) =>
        throw new LackOfItemsException($"There is not enough {item.Name} in this shop");
    public static LackOfItemsException LackOfItemsInBracketException(Item item) =>
        throw new LackOfItemsException($"There is not enough {item.Name} your bracket");
}
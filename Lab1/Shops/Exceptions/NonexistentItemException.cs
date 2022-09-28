using Shops.Entities;
namespace Shops.Exceptions;

public class NonexistentItemException : ShopExceptions
{
    public NonexistentItemException(Item item)
        : base($"Item {item.Name} does not exist in this shop. You can't change it's price") { }
}
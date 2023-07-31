using Shops.Exceptions;

namespace Shops.Entities;

public class CustomerOrder
{
    public CustomerOrder(Item item, int count, Shop shop, decimal price)
    {
        Items = new Dictionary<Item, int>(0) { { item, count } };
        Shop = shop;
        TotalCost += count * price;
    }

    public Shop Shop { get; }
    public Dictionary<Item, int> Items { get; private set; }
    public decimal TotalCost { get; private set; }

    public void AddItems(Item item, int count, decimal price)
    {
        if (Items.ContainsKey(item))
        {
            Items[item] += count;
        }
        else
        {
            Items.Add(item, count);
        }

        TotalCost += price * count;
    }

    public void RemoveItems(Item item, int count, decimal price)
    {
        if (Items[item] < count)
        {
            LackOfItemsException.LackOfItemsInBracketException(item);
        }

        Items[item] -= count;
        TotalCost -= price * count;
    }
}
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Customer
{
    private string _name;
    private Address _address;
    public Customer(decimal money, string name, Address address)
    {
        Money = money;
        _name = name;
        _address = address;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public decimal Money { get; private set; }
    public CustomerOrder Order { get; private set;  } = null!;

    public void AddToBracket(Item item, int count, decimal currentPrice, Shop shop)
    {
        if (Order == null!)
        {
            Order = new CustomerOrder(item, count, shop, currentPrice);
        }
        else
        {
            Order.AddItems(item, count, currentPrice);
        }
    }

    public void RemoveFromBracket(Item item, int count, decimal price)
    {
        Order.RemoveItems(item, count, price);
    }

    public void Buy()
    {
        if (Order.TotalCost > Money)
        {
            throw new LackOfMoneyException();
        }

        Money -= Order.TotalCost;
    }
}
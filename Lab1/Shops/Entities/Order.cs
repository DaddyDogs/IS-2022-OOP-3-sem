namespace Shops.Entities;

public class Order
{
    public Order(Dictionary<Item, int> items, Shop shop, Customer customer)
    {
        Items = items;
        Shop = shop;
        Customer = customer;
    }

    public Order(Item item, int count, Shop shop, Customer customer)
    {
        Items = new Dictionary<Item, int>(0);
        Items.Add(item, count);
        Shop = shop;
        Customer = customer;
    }

    public Dictionary<Item, int> Items { get; }
    public Shop Shop { get; }
    public Customer Customer { get; }
    public double TotalCost { get; set; }
}
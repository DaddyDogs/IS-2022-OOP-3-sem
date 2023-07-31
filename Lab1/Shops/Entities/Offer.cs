namespace Shops.Entities;

public class Offer
{
    public Offer(Item item, int count, decimal price)
    {
        Item = item;
        Count = count;
        Price = price;
    }

    public Item Item { get; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}
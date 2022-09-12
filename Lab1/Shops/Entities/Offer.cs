namespace Shops.Entities;

public class Offer
{
    public Offer(Item item, int count, double price)
    {
        Item = item;
        Count = count;
        Price = price;
    }

    public Item Item { get; }
    public int Count { get; set; }
    public double Price { get; set; }
}
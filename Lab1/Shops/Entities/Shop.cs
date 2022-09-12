using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private double _revenue;
    public Shop(string name, Adress adress)
    {
        Id = Guid.NewGuid();
        Name = name;
        Adress = adress;
        OfferList = new List<Offer>(0);
        ItemsList = new List<Item>(0);
        _revenue = 0;
    }

    public Guid Id { get; }
    public string Name { get; }
    public Adress Adress { get; }
    public List<Offer> OfferList { get; }
    public List<Item> ItemsList { get; }
    public void SetRevenue(double newRevenue)
    {
        _revenue += newRevenue;
    }

    public double GetRevenue()
    {
        return _revenue;
    }
}
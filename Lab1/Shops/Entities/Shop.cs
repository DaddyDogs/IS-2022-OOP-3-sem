using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private readonly List<Offer> _offerList;
    private readonly List<Item> _itemsList;
    private decimal _revenue;
    public Shop(string name, Address address)
    {
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
        _offerList = new List<Offer>(0);
        _itemsList = new List<Item>(0);
        _revenue = 0;
    }

    public Guid Id { get; }
    public string Name { get; }
    public Address Address { get; }
    public IReadOnlyList<Offer> OfferList => _offerList;
    private IReadOnlyList<Item> ItemsList => _itemsList;
    public void SetRevenue(decimal newRevenue)
    {
        _revenue += newRevenue;
    }

    public decimal GetRevenue()
    {
        return _revenue;
    }

    public void AddItem(Item item, decimal price, int count)
    {
        if (ItemsList.Contains(item))
        {
            OfferList.First(offer => offer.Item == item).Count += count;
        }
        else
        {
            var newOffer = new Offer(item, count, price);
            _itemsList.Add(item);
            _offerList.Add(newOffer);
        }
    }

    public void AddItems(List<Offer> offers)
    {
        foreach (Offer offer in offers)
        {
            AddItem(offer.Item, offer.Price, offer.Count);
        }
    }

    public decimal KnowPrice(Item item, int count)
    {
        decimal currentPrice = 0;
        Offer? offer = OfferList.FirstOrDefault(offer => offer.Item == item);
        if (offer is null || offer.Count < count)
        {
            LackOfItemsException.LackOfItemsInShopException(item);
        }
        else
        {
            currentPrice = offer.Price;
        }

        return currentPrice;
    }

    public void Buy(Dictionary<Item, int> order, decimal totalCost)
    {
        foreach (KeyValuePair<Item, int> item in order)
        {
            OfferList.First(offer => offer.Item == item.Key).Count -= item.Value;
        }

        SetRevenue(totalCost);
    }
}
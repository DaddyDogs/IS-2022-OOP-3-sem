using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopService : IShopService
{
    private readonly List<Shop> _shopsList;

    public ShopService()
    {
        _shopsList = new List<Shop>(0);
    }

    public Shop AddShop(string shopName, Address shopAddress)
    {
        if (_shopsList.Any(shop => shop.Address == shopAddress))
        {
            throw new RecurringAddressException();
        }

        var newShop = new Shop(shopName, shopAddress);
        _shopsList.Add(newShop);
        return newShop;
    }

    public void AddItem(Shop shop, Item item, decimal price, int count)
    {
        shop.AddItem(item, price, count);
    }

    public void SupplyItems(Shop shop, List<Offer> offers)
    {
        shop.AddItems(offers);
    }

    public void AddToBracket(Shop shop, Customer customer, Item item, int count)
    {
        decimal currentPrice = shop.KnowPrice(item, count);
        customer.AddToBracket(item, count, currentPrice, shop);
    }

    public void RemoveFromBracket(Shop shop, Customer customer, Item item, int count)
    {
        decimal currentPrice = shop.KnowPrice(item, count);
        customer.RemoveFromBracket(item, count, currentPrice);
    }

    public void SetShopRevenue(Shop shop, decimal revenue)
    {
        shop.SetRevenue(revenue);
    }

    public void Buy(Customer customer)
    {
        customer.Buy();
        customer.Order.Shop.Buy(customer.Order.Items, customer.Order.TotalCost);
    }

    public void ChangePrice(Shop shop, Item item, decimal newPrice)
    {
        Offer? requiredOffer = shop.OfferList.FirstOrDefault(offer => offer.Item == item);
        if (requiredOffer is null)
        {
            throw new NonexistentItemException(item);
        }

        requiredOffer.Price = newPrice;
    }

    public Shop? FindTheBestOffer(Item item, int count)
    {
        decimal bestPrice = decimal.MaxValue;
        Shop? bestShop = null;
        foreach (Shop shop in _shopsList)
        {
            Offer? offer = shop.OfferList.FirstOrDefault(offer => offer.Item == item);
            if (offer is null || offer.Count < count) continue;
            if (offer.Price >= bestPrice) continue;
            bestPrice = offer.Price;
            bestShop = shop;
        }

        if (bestShop == null)
        {
            LackOfItemsException.LackOfItemsInShopException(item);
        }

        return bestShop;
    }
}
using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopService : IShopService
{
    private List<Order> _ordersList;
    private List<Shop> _shopsList;

    public ShopService()
    {
        _ordersList = new List<Order>(0);
        _shopsList = new List<Shop>(0);
    }

    public Shop AddShop(string shopName, Adress shopAdress)
    {
        foreach (Shop shop in _shopsList)
        {
            if (shop.Adress == shopAdress)
            {
                throw new RecurringAdressException();
            }
        }

        var newShop = new Shop(shopName, shopAdress);
        _shopsList.Add(newShop);
        return newShop;
    }

    public void AddItem(Shop shop, Item item, double price, int count)
    {
        if (shop.ItemsList.Contains(item))
        {
            foreach (Offer offer in shop.OfferList)
            {
                if (offer.Item == item)
                {
                    offer.Count += count;
                }
            }
        }
        else
        {
            var newOffer = new Offer(item, count, price);
            shop.ItemsList.Add(item);
            shop.OfferList.Add(newOffer);
        }
    }

    public void AddItems(Shop shop, List<Offer> offers)
    {
        foreach (Offer offer in offers)
        {
            AddItem(shop, offer.Item, offer.Price, offer.Count);
        }
    }

    public Order AddToBracket(Shop shop, Customer customer, Item item, int count)
    {
        double currentPrice = 0;
        foreach (Offer offer in shop.OfferList)
        {
            if (offer.Item == item)
            {
                if (offer.Count < count)
                {
                    throw new LackOfItemsException();
                }

                currentPrice = offer.Price;
                break;
            }
        }

        if (currentPrice == 0)
        {
            throw new LackOfItemsException();
        }

        foreach (Order order in _ordersList)
        {
            if (order.Customer == customer)
            {
                order.Items.Add(item, count);
                order.TotalCost += currentPrice * count;
                return order;
            }
        }

        var newOrder = new Order(item, count, shop, customer);
        newOrder.TotalCost = currentPrice * count;
        _ordersList.Add(newOrder);
        return newOrder;
    }

    public void SetShopRevenue(Shop shop, double revenue)
    {
        shop.SetRevenue(revenue);
    }

    public void Buy(Order order)
    {
        if (order.TotalCost > order.Customer.Money)
        {
            throw new LackOfMoneyException();
        }

        SetShopRevenue(order.Shop, order.TotalCost);
        order.Customer.Money -= order.TotalCost;
        foreach (var product in order.Items)
        {
            foreach (Offer item in order.Shop.OfferList)
            {
                if (product.Key == item.Item)
                {
                    item.Count -= product.Value;
                }
            }
        }
    }

    public void ChangePrice(Shop shop, Item item, double newPrice)
    {
        foreach (Offer offer in shop.OfferList)
        {
            if (offer.Item != item) continue;
            offer.Price = newPrice;
            break;
        }
    }

    public Shop FindTheBestOffer(Item item, int count)
    {
        double bestPrice = double.MaxValue;
        Shop? bestShop = null;
        foreach (Shop shop in _shopsList)
        {
            foreach (Offer offer in shop.OfferList)
            {
                if (offer.Item != item) continue;
                if (offer.Count < count) continue;
                if (offer.Price < bestPrice)
                {
                    bestPrice = offer.Price;
                    bestShop = shop;
                }

                break;
            }
        }

        if (bestShop == null)
        {
            throw new LackOfItemsException();
        }

        return bestShop;
    }
}
using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopService
{
    private readonly IShopService _shopService;

    public ShopService()
    {
        _shopService = new Services.ShopService();
    }

    [Fact]
    public void AddShopAndSupplyItems_BuyItems()
    {
        Shop newShop = _shopService.AddShop("Yagodki", new Adress("Internet", 31));
        var milk = new Item("Milk");
        var croissant = new Item("Croissant");
        var newOffer1 = new Offer(milk, 35, 59);
        var newOffer2 = new Offer(croissant, 50, 100);
        var offersList = new List<Offer>
        {
            newOffer1,
            newOffer2,
        };
        _shopService.AddItems(newShop, offersList);
        var newCustomer = new Customer(5000, "Dochka Dirova", new Adress("Irkutsk", 28));
        Order newOrder = _shopService.AddToBracket(newShop, newCustomer, milk, 31);
        _shopService.Buy(newOrder);
        Assert.Equal(newShop.OfferList[0].Count, 35 - 31);
        Assert.Equal(newShop.GetRevenue(), 31 * offersList[0].Price);
        Assert.Equal(newCustomer.Money, 5000 - (31 * offersList[0].Price));
    }

    [Fact]
    public void ChangePrices()
    {
        Shop newShop = _shopService.AddShop("BlueBunny", new Adress("Pornass", 31));
        var strawberry = new Item("Strawberry");
        var cream = new Item("Cream");
        var newOffer1 = new Offer(strawberry, 5000, 999);
        var newOffer2 = new Offer(cream, 50, 190);
        var offersList = new List<Offer>
        {
            newOffer1,
            newOffer2,
        };
        _shopService.AddItems(newShop, offersList);
        _shopService.ChangePrice(newShop, strawberry, 1035.95);
        Assert.Equal(1035.95, newShop.OfferList[0].Price);
        Assert.Equal(190, newShop.OfferList[1].Price);
    }

    [Fact]
    public void FindTheBestOffer()
    {
        Shop newShop1 = _shopService.AddShop("Putinteam", new Adress("Kremlin", 227));
        Shop newShop2 = _shopService.AddShop("Aliexpress", new Adress("Chine", 19));
        Shop newShop3 = _shopService.AddShop("Ozona", new Adress("Russian", 229));
        var shirt = new Item("Shirt");
        var cap = new Item("cap");
        var newOffer1 = new Offer(shirt, 50, 899);
        var newOffer2 = new Offer(cap, 49, 100);
        var offersList = new List<Offer>
        {
            newOffer1,
            newOffer2,
        };
        _shopService.AddItems(newShop1, offersList);
        _shopService.AddItems(newShop2, offersList);
        _shopService.AddItems(newShop3, offersList);
        _shopService.ChangePrice(newShop1, shirt, 3999);
        _shopService.ChangePrice(newShop3, shirt, 999);
        Shop? bestOffer = _shopService.FindTheBestOffer(shirt, 50);
        Assert.Equal(newShop2, bestOffer);
        try
        {
            _shopService.FindTheBestOffer(cap, 50);
        }
        catch (LackOfItemsException)
        {
        }
    }

    [Fact]
    public void BuyItems_CustomerHasNotEnoughMoney_ShopHasNotEnoughItems()
    {
        Shop newShop = _shopService.AddShop("Yagodki", new Adress("Internet", 31));
        var milk = new Item("Milk");
        var croissant = new Item("Croissant");
        var newOffer1 = new Offer(milk, 35, 59);
        var newOffer2 = new Offer(croissant, 150, 100);
        var offersList = new List<Offer>
        {
            newOffer1,
            newOffer2,
        };
        _shopService.AddItems(newShop, offersList);
        var newCustomer = new Customer(5000, "Dochka Dirova", new Adress("Irkutsk", 28));
        Order newOrder = _shopService.AddToBracket(newShop, newCustomer, milk, 35);
        newOrder = _shopService.AddToBracket(newShop, newCustomer, croissant, 100);
        try
        {
            _shopService.Buy(newOrder);
        }
        catch (LackOfMoneyException)
        {
        }

        var newCustomer2 = new Customer(500000, "Lena Golovach", new Adress("of Suren Vrotanov", 2));
        try
        {
            _shopService.AddToBracket(newShop, newCustomer2, milk, 10);
        }
        catch (LackOfItemsException)
        {
        }
    }
}
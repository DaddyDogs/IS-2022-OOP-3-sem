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
        Shop newShop = _shopService.AddShop("Yagodki", new Address("Internet", 31));
        var milk = new Item("Milk");
        var croissant = new Item("Croissant");
        var newOffer1 = new Offer(milk, 35, 59);
        var newOffer2 = new Offer(croissant, 50, 100);
        var offersList = new List<Offer>
        {
            newOffer1,
            newOffer2,
        };
        _shopService.SupplyItems(newShop, offersList);
        var newCustomer = new Customer(5000, "Dochka Dirova", new Address("Irkutsk", 28));
        _shopService.AddToBracket(newShop, newCustomer, milk, 31);
        _shopService.Buy(newCustomer);
        Assert.Equal(newShop.OfferList[0].Count, 35 - 31);
        Assert.Equal(newShop.GetRevenue(), 31 * offersList[0].Price);
        Assert.Equal(newCustomer.Money, 5000 - (31 * offersList[0].Price));
    }

    [Fact]
    public void ChangePrices()
    {
        Shop newShop = _shopService.AddShop("BlueBunny", new Address("Pornass", 31));
        var strawberry = new Item("Strawberry");
        var cream = new Item("Cream");
        var newOffer1 = new Offer(strawberry, 5000, 999);
        var newOffer2 = new Offer(cream, 50, 190);
        var offersList = new List<Offer>
        {
            newOffer1,
            newOffer2,
        };
        _shopService.SupplyItems(newShop, offersList);
        _shopService.ChangePrice(newShop, strawberry, new decimal(1035.95));
        Assert.Equal(new decimal(1035.95), newShop.OfferList[0].Price);
        Assert.Equal(190, newShop.OfferList[1].Price);
    }

    [Fact]
    public void FindTheBestOffer()
    {
        Shop newShop1 = _shopService.AddShop("Putinteam", new Address("Kremlin", 227));
        Shop newShop2 = _shopService.AddShop("Aliexpress", new Address("Chine", 19));
        Shop newShop3 = _shopService.AddShop("Ozona", new Address("Russian", 229));
        var shirt = new Item("Shirt");
        var cap = new Item("cap");
        var newOffer1 = new Offer(shirt, 50, 899);
        var newOffer2 = new Offer(cap, 49, 100);
        var offersList = new List<Offer>
        {
            newOffer1,
            newOffer2,
        };
        _shopService.SupplyItems(newShop1, offersList);
        _shopService.SupplyItems(newShop2, offersList);
        _shopService.SupplyItems(newShop3, offersList);
        _shopService.ChangePrice(newShop1, shirt, 3999);
        _shopService.ChangePrice(newShop3, shirt, 999);
        Shop? bestOffer = _shopService.FindTheBestOffer(shirt, 50);
        Assert.Equal(newShop2, bestOffer);
        Exception exception = Assert.Throws<LackOfItemsException>(() => _shopService.FindTheBestOffer(cap, 50));
        Assert.IsType<LackOfItemsException>(exception);
    }

    [Fact]
    public void BuyItems_CustomerHasNotEnoughMoney_ShopHasNotEnoughItems()
    {
        Shop newShop = _shopService.AddShop("Yagodki", new Address("Internet", 31));
        var milk = new Item("Milk");
        var croissant = new Item("Croissant");
        var newOffer1 = new Offer(milk, 35, 59);
        var newOffer2 = new Offer(croissant, 150, 100);
        var offersList = new List<Offer>
        {
            newOffer1,
            newOffer2,
        };
        _shopService.SupplyItems(newShop, offersList);
        var newCustomer = new Customer(5000, "Dochka Dirova", new Address("Irkutsk", 28));
        _shopService.AddToBracket(newShop, newCustomer, milk, 35);
        _shopService.AddToBracket(newShop, newCustomer, croissant, 100);
        Exception exception = Assert.Throws<LackOfMoneyException>(() => _shopService.Buy(newCustomer));
        Assert.IsType<LackOfMoneyException>(exception);
        _shopService.RemoveFromBracket(newShop, newCustomer, croissant, 100);
        _shopService.Buy(newCustomer);
        var newCustomer2 = new Customer(500000, "Lena Golovach", new Address("of Suren Vrotanov", 2));
        Exception exception2 = Assert.Throws<LackOfItemsException>(() => _shopService.AddToBracket(newShop, newCustomer2, milk, 10));
        Assert.IsType<LackOfItemsException>(exception2);
    }
}
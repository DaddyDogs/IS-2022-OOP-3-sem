using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopService
{
    public Shop AddShop(string shopName, Adress shopAdress);
    public void AddItem(Shop shop, Item item, double price, int count);
    void AddItems(Shop shop, List<Offer> offers);
    public Order AddToBracket(Shop shop, Customer customer, Item item, int count);
    public void SetShopRevenue(Shop shop, double revenue);
    public void Buy(Order order);
    public void ChangePrice(Shop shop, Item item, double newPrice);
    public Shop? FindTheBestOffer(Item item, int count);
}
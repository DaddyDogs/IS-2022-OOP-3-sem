using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopService
{
    public Shop AddShop(string shopName, Address shopAddress);
    public void AddItem(Shop shop, Item item, decimal price, int count);
    void SupplyItems(Shop shop, List<Offer> offers);
    public void AddToBracket(Shop shop, Customer customer, Item item, int count);
    public void RemoveFromBracket(Shop shop, Customer customer, Item item, int count);
    public void SetShopRevenue(Shop shop, decimal revenue);
    public void Buy(Customer customer);
    public void ChangePrice(Shop shop, Item item, decimal newPrice);
    public Shop? FindTheBestOffer(Item item, int count);
}
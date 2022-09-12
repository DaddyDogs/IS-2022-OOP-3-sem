using Shops.Models;

namespace Shops.Entities;

public class Customer
{
    private string _name;
    public Customer(double money, string name, Adress adress)
    {
        Money = money;
        _name = name;
        Adress = adress;
        Id = Guid.NewGuid();
    }

    public Adress Adress { get; }
    public Guid Id { get; }
    public double Money { get; set; }
}
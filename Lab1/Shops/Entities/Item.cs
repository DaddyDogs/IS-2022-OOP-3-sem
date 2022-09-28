namespace Shops.Entities;

public class Item
{
    public Item(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }

    public string Name { get; }
    public Guid Id { get; }
}
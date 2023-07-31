namespace Shops.Models;

public class Address
{
    private string _streetName;
    private int _buildNumber;
    public Address(string streetName, int buildNumber)
    {
        _streetName = streetName;
        _buildNumber = buildNumber;
    }
}
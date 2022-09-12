namespace Shops.Models;

public class Adress
{
    private string _streetName;
    private int _buildNumber;
    public Adress(string streetName, int buildNumber)
    {
        _streetName = streetName;
        _buildNumber = buildNumber;
    }
}
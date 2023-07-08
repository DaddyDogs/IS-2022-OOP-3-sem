namespace Banks.Models.Address;

public interface IAddressBuilder
{
    IAddressBuilder WithEntrance(string entranceNumber);
    IAddressBuilder WithFloor(int floorNumber);
    Address Build();
}
namespace Banks.Models.Address;

public interface IBuildingBuilder
{
    IFlatBuilder WithBuilding(string buildingNumber);
}
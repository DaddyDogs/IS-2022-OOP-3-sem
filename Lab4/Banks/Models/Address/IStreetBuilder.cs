namespace Banks.Models.Address;

public interface IStreetBuilder
{
    IBuildingBuilder WithStreet(string streetName);
}
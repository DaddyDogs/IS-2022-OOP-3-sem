namespace Banks.Models.Address;

public interface IFlatBuilder
{
    IAddressBuilder WitFlat(int flatNumber);
}
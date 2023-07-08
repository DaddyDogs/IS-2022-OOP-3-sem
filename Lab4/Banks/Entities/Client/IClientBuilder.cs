using Banks.Models;
using Banks.Models.Address;

namespace Banks.Entities.Client;

public interface IClientBuilder
{
    IClientBuilder WithAddress(Address? address);
    IClientBuilder WithPassport(Passport? passport);
    Client Build();
}
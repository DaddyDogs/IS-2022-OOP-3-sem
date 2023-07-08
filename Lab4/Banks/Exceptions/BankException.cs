using Banks.Entities.Client;

namespace Banks.Exceptions;

public class BankException : BanksException
{
    private BankException(string message)
        : base(message) { }

    public static BankException ClientIsAlreadyRegistered(Client client)
    {
        return new BankException($"Client {client.LastName + " " + client.LastName} with id {client.Id} is already registered");
    }

    public static BankException ClientIsNotRegistered(Guid clientId)
    {
        return new BankException($"Client with id {clientId} is not registered in the bank");
    }

    public static BankException AccountIsNotRegistered(Guid accountId)
    {
        return new BankException($"Account with id {accountId} is not registered in the bank");
    }
}
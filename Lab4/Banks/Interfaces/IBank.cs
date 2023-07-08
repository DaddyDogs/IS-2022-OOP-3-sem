using Banks.Entities.Client;
using Banks.Models;
using Banks.Models.Address;

namespace Banks.Interfaces;

public interface IBank
{
    Guid RegisterCreditAccount(Client client);
    Guid RegisterDebitAccount(Client client);
    Guid RegisterDepositAccount(Client client, Amount amount);
    Client RegisterClient(string firstName, string lastName, Address? address, Passport? passport);
    void ChangeInterestPercent(Amount newPercent);
    void ChangeCreditLimit(Amount newLimit);
    Guid GetId();
    Client? FindClientByAccount(Guid accountId);
    CreditConditions GetCreditConditions();
    DebitConditions GetDebitConditions();
    decimal GetMoneyAmount(Guid accountId);
    string GetName();
    IReadOnlyCollection<Client> GetClients();
}
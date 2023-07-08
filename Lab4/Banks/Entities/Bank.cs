using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;
using Banks.Models.Address;

namespace Banks.Entities;

public class Bank : IBank
{
    private readonly List<Client.Client> _clients;

    public Bank(string name, DepositConditions depositConditions, CreditConditions creditConditions, DebitConditions debitConditions)
    {
        _clients = new List<Client.Client>(0);
        Id = Guid.NewGuid();
        if (string.IsNullOrWhiteSpace(name))
        {
            ArgumentNullException.ThrowIfNull(name);
        }

        Name = name;
        CreditConditions = creditConditions;
        DebitConditions = debitConditions;
        DepositConditions = depositConditions;
    }

    public CreditConditions CreditConditions { get; }
    public DebitConditions DebitConditions { get; }
    public DepositConditions DepositConditions { get; }

    public IReadOnlyList<Client.Client> Clients => _clients;
    public Guid Id { get; }
    public string Name { get; }

    public Guid RegisterCreditAccount(Client.Client client)
    {
        GetClient(client.Id);

        var newAccount = new CreditAccount(CreditConditions, Id);
        client.AddAccount(newAccount);
        return newAccount.Id;
    }

    public Guid RegisterDebitAccount(Client.Client client)
    {
        GetClient(client.Id);

        var newAccount = new DebitAccount(DebitConditions, Id);
        client.AddAccount(newAccount);
        return newAccount.Id;
    }

    public Guid RegisterDepositAccount(Client.Client client, Amount amount)
    {
        GetClient(client.Id);

        var newAccount = new DepositAccount(DepositConditions, Id, amount);
        client.AddAccount(newAccount);
        return newAccount.Id;
    }

    public Client.Client RegisterClient(string firstName, string lastName, Address? address, Passport? passport)
    {
        Client.Client client = Client.Client.Builder.WithName(firstName, lastName).WithAddress(address).WithPassport(passport).Build();
        _clients.Add(client);
        return client;
    }

    public void ChangeInterestPercent(Amount newPercent)
    {
        DebitConditions.ChangePercent(newPercent);
    }

    public void ChangeCreditLimit(Amount newLimit)
    {
        CreditConditions.ChangeLimit(newLimit);
    }

    public Guid GetId()
    {
        return Id;
    }

    public Client.Client? FindClientByAccount(Guid accountId)
    {
        return _clients.FirstOrDefault(client => client.Accounts.FirstOrDefault(acc => acc.GetId() == accountId) is not null);
    }

    public Client.Client GetClient(Guid clientId)
    {
        Client.Client? client = _clients.Find(c => c.Id == clientId);
        if (client is null)
        {
            throw BankException.ClientIsNotRegistered(clientId);
        }

        return client;
    }

    public CreditConditions GetCreditConditions()
    {
        return CreditConditions;
    }

    public DebitConditions GetDebitConditions()
    {
        return DebitConditions;
    }

    public decimal GetMoneyAmount(Guid accountId)
    {
        Client.Client? client = FindClientByAccount(accountId);
        if (client is null)
        {
            throw BankException.AccountIsNotRegistered(accountId);
        }

        return client.Accounts.First(acc => acc.GetId() == accountId).GetAmount();
    }

    public string GetName()
    {
        return Name;
    }

    public IReadOnlyCollection<Client.Client> GetClients()
    {
        return _clients;
    }
}
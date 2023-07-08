using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;
using Banks.Models.Address;
using Banks.Services;

namespace Banks.Entities.Client;

public class Client : IObserver
{
    private readonly List<IAccount> _accounts;
    private readonly Suspicious _suspicious = new ();
    private readonly Reliable _reliable = new ();
    private INotifier _notifier = new ConsoleNotifier();
    private Client(string firstName, string lastName, Address? address, Passport? passport)
    {
        _accounts = new List<IAccount>(0);
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Passport = passport;
        if (Address is null || Passport is null)
        {
            State = _suspicious;
        }
        else
        {
            State = _reliable;
        }

        Id = Guid.NewGuid();
    }

    public static INameBuilder Builder => new ClientBuilder();

    public IEnumerable<IAccount> Accounts => _accounts;
    public string FirstName { get; }
    public string LastName { get; }
    public Address? Address { get; private set; }
    public Passport? Passport { get; private set; }
    public IClientState State { get; private set; }
    public Guid Id { get; }

    public void SetAddress(Address address)
    {
        Address = address;
        if (Passport is not null)
        {
            State = _reliable;
        }
    }

    public void SetPassport(int series, int number)
    {
        Passport = new Passport(series, number);
        if (Address is not null)
        {
            State = _reliable;
        }
    }

    public void AddAccount(IAccount account)
    {
        if (_accounts.Contains(account))
        {
            throw ClientException.AccountIsAlreadyRegistered(account.GetId());
        }

        _accounts.Add(account);
    }

    public void SubscribeToLimitUpdate(IBank bank)
    {
        bank.GetCreditConditions().AddObserver(this);
    }

    public void UnsubscribeToLimitUpdate(IBank bank)
    {
        bank.GetCreditConditions().RemoveObserver(this);
    }

    public void SubscribeToPercentUpdate(IBank bank)
    {
        bank.GetDebitConditions().AddObserver(this);
    }

    public void UnsubscribeToPercentUpdate(IBank bank)
    {
        bank.GetDebitConditions().RemoveObserver(this);
    }

    public void Update(string subject, decimal newValue)
    {
        _notifier.Notify(subject, newValue);
    }

    public void SetNotifier(INotifier notifier)
    {
        _notifier = notifier;
    }

    private class ClientBuilder : INameBuilder, IClientBuilder
    {
        private string? _firstName;
        private string? _lastName;
        private Address? _address;
        private Passport? _passport;

        public IClientBuilder WithName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                ArgumentNullException.ThrowIfNull(firstName);
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                ArgumentNullException.ThrowIfNull(lastName);
            }

            _firstName = firstName;
            _lastName = lastName;
            return this;
        }

        public IClientBuilder WithAddress(Address? address)
        {
            _address = address;
            return this;
        }

        public IClientBuilder WithPassport(Passport? passport)
        {
            _passport = passport;
            return this;
        }

        public Client Build()
        {
            if (_firstName is null || _lastName is null)
            {
                throw new ArgumentNullException();
            }

            return new Client(_firstName, _lastName, _address, _passport);
        }
    }
}
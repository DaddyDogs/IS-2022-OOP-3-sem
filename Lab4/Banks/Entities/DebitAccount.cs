using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;
using Banks.Services;

namespace Banks.Entities;

public class DebitAccount : IAccount, IDisposable
{
    private readonly Clock _clock = new Clock();
    private decimal _saved;
    public DebitAccount(DebitConditions conditions, Guid bankId)
    {
        Amount = 0;
        Conditions = conditions;
        BankId = bankId;
        Id = Guid.NewGuid();
        _saved = 0;
        _clock.Notify1 += AddInterest;
        _clock.Notify2 += PayInterest;
    }

    public DebitConditions Conditions { get; }
    public decimal Amount { get; private set; }
    public Guid Id { get; }
    public Guid BankId { get; }

    public void DepositMoney(Amount amount)
    {
        Amount += amount.Sum;
    }

    public void WithDrawMoney(Amount amount)
    {
        if (Amount - amount.Sum < 0)
        {
            throw DebitAccountException.InsufficientFundsException(amount.Sum);
        }

        Amount -= amount.Sum;
    }

    public void AddInterest()
    {
        _saved += Amount * (Conditions.Percent / (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365));
    }

    public void PayInterest()
    {
        Amount += _saved;
        _saved = 0;
    }

    public void SubtractCommission()
    {
    }

    public Guid GetId()
    {
        return Id;
    }

    public Guid GetBankId()
    {
        return BankId;
    }

    public decimal GetAmount()
    {
        return Amount;
    }

    public decimal GetRestriction()
    {
        return Conditions.Restriction;
    }

    public decimal CalculateFee(decimal subtrahend)
    {
        return 0;
    }

    public IClock GetClock()
    {
        return _clock;
    }

    public void Dispose()
    {
        _clock.Dispose();
    }
}
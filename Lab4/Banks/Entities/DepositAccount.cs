using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;
using Banks.Services;

namespace Banks.Entities;

public class DepositAccount : IAccount, IDisposable
{
    private readonly Clock _clock = new Clock();
    private decimal _saved;
    public DepositAccount(DepositConditions conditions, Guid bankId, Amount amount)
    {
        foreach (DepositCondition condition in conditions.DepositConditionsList)
        {
            if (amount.Sum >= condition.MaximalAmount) continue;
            Percent = condition.Percent;
            break;
        }

        if (Percent == 0)
        {
            throw DepositAccountException.InvalidDepositAmount(amount.Sum);
        }

        _saved = 0;
        Amount = amount.Sum;
        Restriction = conditions.Restriction;
        BankId = bankId;
        ExpiryDate = DateTime.Now.Add(conditions.Term);
        Id = Guid.NewGuid();
        _clock.Notify1 += AddInterest;
        _clock.Notify2 += PayInterest;
    }

    public decimal Restriction { get; }
    public decimal Percent { get; } = 0;
    public decimal Amount { get; private set; }
    public DateTime ExpiryDate { get; }
    public Guid Id { get; }
    public Guid BankId { get; }

    public void DepositMoney(Amount amount)
    {
        Amount += amount.Sum;
    }

    public void WithDrawMoney(Amount amount)
    {
        if (DateTime.Now < ExpiryDate)
        {
            throw DepositAccountException.DeniedWithdrawingException(ExpiryDate);
        }

        Amount -= amount.Sum;
    }

    public void AddInterest()
    {
        _saved += Amount * Percent / (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365);
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
        return Restriction;
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
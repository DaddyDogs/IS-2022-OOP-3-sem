using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class CreditAccount : IAccount
{
    public const double Percent = 0;
    private readonly List<AbstractTransaction> _transactions;
    public CreditAccount(CreditConditions conditions, Guid bankId)
    {
        Amount = 0;
        Conditions = conditions;
        _transactions = new List<AbstractTransaction>(0);
        Id = Guid.NewGuid();
        BankId = bankId;
    }

    public CreditConditions Conditions { get; }
    public decimal Amount { get; private set; }
    public Guid Id { get; }
    public Guid BankId { get; }

    public void DepositMoney(Amount amount)
    {
        if (Amount + amount.Sum > Conditions.Limit)
        {
            throw CreditAccountException.OverstepLimitException(Amount + amount.Sum);
        }

        Amount += amount.Sum;
    }

    public void WithDrawMoney(Amount amount)
    {
        if (Math.Abs(Amount - amount.Sum) > Conditions.Limit)
        {
            throw CreditAccountException.OverstepLimitException(Amount - amount.Sum);
        }

        Amount -= amount.Sum;
        if (Amount < 0)
        {
            Amount -= Conditions.Fee;
        }
    }

    public void AddInterest()
    {
    }

    public void PayInterest()
    {
    }

    public void SubtractCommission()
    {
        if (Amount < 0)
        {
            Amount -= Conditions.Fee;
        }
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
        return Amount - subtrahend < 0 ? Conditions.Fee : 0;
    }

    public IClock GetClock()
    {
        throw CreditAccountException.HasNoClockException();
    }
}
using Banks.Models;

namespace Banks.Interfaces;

public interface IAccount
{
    void DepositMoney(Amount amount);
    void WithDrawMoney(Amount amount);
    void AddInterest();
    void PayInterest();
    void SubtractCommission();
    Guid GetId();
    Guid GetBankId();
    decimal GetAmount();
    decimal GetRestriction();
    decimal CalculateFee(decimal subtrahend);
    IClock GetClock();
}
using Banks.Entities;
using Banks.Entities.Client;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;
using Banks.Models.Address;
using Banks.Services;
using Xunit;

namespace Banks.Test;

public class TransactionsTests
{
    private readonly CentralBank _centralBank;
    private readonly IBank _bank;
    private readonly DepositConditions _depositConditions;
    private readonly CreditConditions _creditConditions;
    private readonly DebitConditions _debitConditions;
    private readonly Client _client;

    public TransactionsTests()
    {
        _centralBank = new CentralBank();
        var depositCondition = new List<DepositCondition>(0)
        {
            new DepositCondition(new Amount(1000), new Amount(5)),
            new DepositCondition(new Amount(2000), new Amount(13)),
            new DepositCondition(new Amount(3000), new Amount(17)),
        };
        _depositConditions = new DepositConditions(depositCondition, new TimeSpan(365, 0, 0, 0), new Amount(1000));
        _creditConditions = new CreditConditions(new Amount(1500), new Amount(10), new Amount(1500));
        _debitConditions = new DebitConditions(new Amount(6), new Amount(900));

        _bank = _centralBank.CreateBank("Tinkoff", _depositConditions, _creditConditions, _debitConditions);
        _client = _bank.RegisterClient("Suren", "Nerus", null, null);
    }

    [Fact]
    public void AccelerateTimeForMonth_InterestWasPayed()
    {
        Guid accountId = _bank.RegisterDebitAccount(_client);

        _centralBank.ReplenishAccount(new Amount(100), accountId);
        _centralBank.Accelerate(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), accountId);

        decimal percent = _debitConditions.Percent / (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365);
        decimal result = 100;
        decimal saved = 0;

        for (int i = 0; i < DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
        {
            saved += result * percent;
        }

        result += saved;

        Assert.Equal(result, _bank.GetMoneyAmount(accountId));
    }

    [Fact]
    public void TransferMoneyFromCreditAccount_CancelTransfer_CheckAmountsAndReturnedFee()
    {
        Guid accountId = _bank.RegisterCreditAccount(_client);
        Client newClient = _bank.RegisterClient("Vor", "Tan", null, null);
        Guid newAccountId = _bank.RegisterDebitAccount(newClient);

        Guid transactionId = _centralBank.TransferMoney(new Amount(500), accountId, newAccountId);

        Assert.Equal(500, _bank.GetMoneyAmount(newAccountId));
        Assert.Equal(-510, _bank.GetMoneyAmount(accountId));

        _centralBank.CancelTransaction(transactionId);

        Assert.Equal(0, _bank.GetMoneyAmount(newAccountId));
        Assert.Equal(0, _bank.GetMoneyAmount(accountId));
    }

    [Fact]
    public void ChangeConditions_ConditionsHaveChanged()
    {
        _bank.ChangeInterestPercent(new Amount(15));
        Assert.Equal(15, _bank.GetDebitConditions().Percent);

        _bank.ChangeCreditLimit(new Amount(6000));
        Assert.Equal(6000, _bank.GetCreditConditions().Limit);
    }

    [Fact]
    private void SuspiciousClient_TryWithdrawMoney_Exception_FillOutAccount_AccomplishTransaction()
    {
        Guid accountId = _bank.RegisterDebitAccount(_client);

        _centralBank.ReplenishAccount(new Amount(5000), accountId);

        Exception exception = Assert.Throws<ClientStateException>(() => _centralBank.WithDrawMoney(new Amount(901), accountId));
        Assert.IsType<ClientStateException>(exception);

        _client.SetAddress(Address.Builder.WithStreet("Surenova").WithBuilding("115A").WitFlat(15).Build());
        _client.SetPassport(1234, 228669);

        _centralBank.WithDrawMoney(new Amount(905), accountId);

        Assert.Equal(5000 - 905, _bank.GetMoneyAmount(accountId));
    }
}
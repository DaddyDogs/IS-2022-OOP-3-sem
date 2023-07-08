using Banks.Entities.Client;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;
using Banks.Services;
using Xunit;

namespace Banks.Test;

public class AccountsTests
{
    private readonly CentralBank _centralBank;
    private readonly IBank _bank;
    private readonly DepositConditions _depositConditions;
    private readonly CreditConditions _creditConditions;
    private readonly DebitConditions _debitConditions;
    private readonly Client _client;
    public AccountsTests()
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
    public void DebitAccount_TryGoIntoMinus_Exception()
    {
        Guid newAccountId = _bank.RegisterDebitAccount(_client);

        Exception exception = Assert.Throws<DebitAccountException>(() => _centralBank.WithDrawMoney(new Amount(100), newAccountId));
        Assert.IsType<DebitAccountException>(exception);
    }

    [Fact]
    public void DepositAccount_TryWithdrawMoneyBeforeExpiryDate_Exception()
    {
        Guid newAccountId = _bank.RegisterDepositAccount(_client, new Amount(1500));

        Exception exception = Assert.Throws<DepositAccountException>(() => _centralBank.WithDrawMoney(new Amount(100), newAccountId));
        Assert.IsType<DepositAccountException>(exception);
    }

    [Fact]
    public void CreditAccount_OverstepLimit_Exception_CheckFee()
    {
        Guid newAccountId = _bank.RegisterCreditAccount(_client);

        _centralBank.WithDrawMoney(new Amount(100), newAccountId);

        Exception exception = Assert.Throws<CreditAccountException>(() => _centralBank.WithDrawMoney(new Amount(1420), newAccountId));
        Assert.IsType<CreditAccountException>(exception);

        Assert.Equal(-110, _bank.GetMoneyAmount(newAccountId));
    }
}
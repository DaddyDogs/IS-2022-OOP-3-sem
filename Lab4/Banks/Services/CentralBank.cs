using Banks.Entities;
using Banks.Entities.Client;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Services;

public class CentralBank : ICentralBank
{
    private readonly List<IBank> _banks = new (0);
    private readonly List<AbstractTransaction> _transactions = new (0);

    public IEnumerable<IBank> Banks => _banks;

    public IBank CreateBank(string name, DepositConditions depositConditions, CreditConditions creditConditions, DebitConditions debitConditions)
    {
        var bank = new Bank(name, depositConditions, creditConditions, debitConditions);
        _banks.Add(bank);
        return bank;
    }

    public Guid ReplenishAccount(Amount amount, Guid accountId)
    {
        Entities.Client.Client client = GetClient(accountId);

        IAccount account = client.Accounts.First(acc => acc.GetId() == accountId);
        AbstractTransaction replenishment = new Replenishment(amount.Sum, account);

        replenishment.Execute(client.State);
        _transactions.Add(replenishment);
        return replenishment.GetId();
    }

    public Guid TransferMoney(Amount amount, Guid accountId, Guid recipientId)
    {
        Entities.Client.Client client = GetClient(accountId);
        Entities.Client.Client recipient = GetClient(recipientId);

        IAccount account = client.Accounts.First(acc => acc.GetId() == accountId);
        IAccount recipientAccount = recipient.Accounts.First(acc => acc.GetId() == recipientId);

        AbstractTransaction transfer = new Transfer(amount.Sum, account, recipientAccount);

        transfer.Execute(client.State);
        _transactions.Add(transfer);
        return transfer.GetId();
    }

    public Guid WithDrawMoney(Amount amount, Guid accountId)
    {
        Entities.Client.Client client = GetClient(accountId);

        IAccount account = client.Accounts.First(acc => acc.GetId() == accountId);

        AbstractTransaction withdrawal = new Withdrawal(amount.Sum, account);

        withdrawal.Execute(client.State);
        _transactions.Add(withdrawal);
        return withdrawal.GetId();
    }

    public IBank GetBank(Guid id)
    {
        IBank? bank = _banks.Find(b => b.GetId() == id);
        if (bank is null)
        {
            throw CentralBankException.BankDoesNotExist(id);
        }

        return bank;
    }

    public Entities.Client.Client GetClient(Guid accountId)
    {
        Entities.Client.Client? client = null;
        foreach (Client? c in _banks.Select(bank => bank.FindClientByAccount(accountId)).Where(c => c is not null))
        {
            client = c;
        }

        if (client is null)
        {
            throw CentralBankException.AccountIsNotRegistered(accountId);
        }

        return client;
    }

    public void Accelerate(int daysNumber, Guid accountId)
    {
        IAccount account = GetClient(accountId).Accounts.First(acc => acc.GetId() == accountId);
        account.GetClock().Accelerate(daysNumber);
    }

    public IReadOnlyList<IBank> GetBanks()
    {
        return _banks;
    }

    public void CancelTransaction(Guid transactionId)
    {
        AbstractTransaction? transaction = _transactions.Find(t => t.GetId() == transactionId);
        if (transaction is null)
        {
            throw CentralBankException.TransactionDoesNotExist(transactionId);
        }

        transaction.Undo();
    }
}
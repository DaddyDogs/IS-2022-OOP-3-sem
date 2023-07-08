using Banks.Entities.Client;
using Banks.Models;

namespace Banks.Interfaces;

public interface ICentralBank
{
    IBank CreateBank(string name, DepositConditions depositConditions, CreditConditions creditConditions, DebitConditions debitConditions);
    Guid ReplenishAccount(Amount amount, Guid accountId);
    Guid TransferMoney(Amount amount, Guid accountId, Guid recipientId);
    Guid WithDrawMoney(Amount amount, Guid accountId);
    void CancelTransaction(Guid transactionId);
    IBank GetBank(Guid id);
    Client GetClient(Guid accountId);
    void Accelerate(int daysNumber, Guid accountId);
    IReadOnlyList<IBank> GetBanks();
}
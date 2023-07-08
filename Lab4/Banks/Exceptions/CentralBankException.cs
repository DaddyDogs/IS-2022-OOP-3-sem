using Banks.Entities;

namespace Banks.Exceptions;

public class CentralBankException : BanksException
{
    private CentralBankException(string message)
        : base(message) { }

    public static CentralBankException BankDoesNotExist(Guid id)
    {
        return new CentralBankException($"Bank with id {id} is not registered");
    }

    public static CentralBankException TransactionDoesNotExist(Guid id)
    {
        return new CentralBankException($"Transaction with id {id} is not registered");
    }

    public static CentralBankException AccountIsNotRegistered(Guid accountId)
    {
        return new CentralBankException($"Account with id {accountId} is not registered in the bank");
    }
}
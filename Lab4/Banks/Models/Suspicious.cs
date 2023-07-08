using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Models;

public class Suspicious : IClientState
{
    public void Check(AbstractTransaction abstractTransaction, decimal newAmount)
    {
        if (abstractTransaction.GetAccount().GetAmount() - abstractTransaction.GetAccount().GetRestriction() > newAmount)
        {
            throw ClientStateException.RestrictedTransactionException(abstractTransaction.GetAccount().GetAmount() - newAmount);
        }
    }
}
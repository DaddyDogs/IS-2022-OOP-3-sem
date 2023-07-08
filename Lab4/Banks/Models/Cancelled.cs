using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Models;

public class Cancelled : ITransactionState
{
    public void TryUndo()
    {
        throw TransactionException.ImpossibleCancellation();
    }
}
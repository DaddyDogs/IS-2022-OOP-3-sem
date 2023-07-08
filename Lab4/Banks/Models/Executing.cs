using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Models;

public class Executing : ITransactionState
{
    public void TryUndo()
    {
        throw TransactionException.ImpossibleCancellation();
    }
}
using Banks.Interfaces;

namespace Banks.Models;

public class Accomplished : ITransactionState
{
    public void TryUndo()
    {
    }
}
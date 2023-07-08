namespace Banks.Interfaces;

public interface ITransactionState
{
    void TryUndo();
}
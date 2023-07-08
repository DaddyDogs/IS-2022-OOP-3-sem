namespace Banks.Interfaces;

public interface IClientState
{
    void Check(AbstractTransaction abstractTransaction, decimal newAmount);
}
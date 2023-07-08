namespace Banks.Interfaces;

public abstract class AbstractTransaction
{
    public void Execute(IClientState clientState)
    {
        decimal newAmount = Evaluate();
        clientState.Check(this, newAmount);
        Accomplish();
    }

    public abstract Guid GetId();
    public abstract IAccount GetAccount();
    public abstract decimal GetAmount();
    public abstract void Undo();
    protected abstract decimal Evaluate();
    protected abstract void Accomplish();
}
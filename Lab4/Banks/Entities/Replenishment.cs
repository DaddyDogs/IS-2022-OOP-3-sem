using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class Replenishment : AbstractTransaction
{
    private readonly Amount _amount;
    private readonly Guid _id;
    private readonly IAccount _account;
    private ITransactionState _state = new Executing();
    public Replenishment(decimal amount, IAccount account)
    {
        _amount = new Amount(amount);
        _account = account;
        _id = Guid.NewGuid();
    }

    public override void Undo()
    {
        _state.TryUndo();
        _account.WithDrawMoney(_amount);
        _state = new Cancelled();
    }

    public override Guid GetId()
    {
        return _id;
    }

    public override IAccount GetAccount()
    {
        return _account;
    }

    public override decimal GetAmount()
    {
        return _amount.Sum;
    }

    protected override void Accomplish()
    {
        _account.DepositMoney(_amount);
        _state = new Accomplished();
    }

    protected override decimal Evaluate()
    {
        return _amount.Sum + _account.GetAmount();
    }
}
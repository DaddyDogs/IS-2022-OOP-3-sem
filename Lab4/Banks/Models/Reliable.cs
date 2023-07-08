using Banks.Interfaces;

namespace Banks.Models;

public class Reliable : IClientState
{
    public void Check(AbstractTransaction abstractTransaction, decimal newAmount)
    {
    }
}
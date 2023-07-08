using Banks.Exceptions;

namespace Banks.Models;

public class Amount
{
    public Amount(decimal amount)
    {
        if (amount < 0)
        {
            throw AmountException.InvalidAmountException(amount);
        }

        Sum = amount;
    }

    public decimal Sum { get; }
}
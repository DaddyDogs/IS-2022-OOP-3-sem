namespace Banks.Models;

public class DepositCondition
{
    public DepositCondition(Amount maximalAmount, Amount percent)
    {
        MaximalAmount = maximalAmount.Sum;
        Percent = percent.Sum;
    }

    public decimal MaximalAmount { get; }
    public decimal Percent { get; }
}
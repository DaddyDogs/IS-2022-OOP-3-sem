namespace Banks.Models;

public class DepositConditions
{
    public const int Fee = 0;
    private readonly List<DepositCondition> _depositConditions;

    public DepositConditions(List<DepositCondition> depositConditions, TimeSpan term, Amount restriction)
    {
        _depositConditions = depositConditions;
        Term = term;
        Restriction = restriction.Sum;
    }

    public TimeSpan Term { get; }
    public decimal Restriction { get; }

    public IEnumerable<DepositCondition> DepositConditionsList => _depositConditions;
}
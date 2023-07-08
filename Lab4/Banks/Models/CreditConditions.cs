using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Models;

public class CreditConditions : IObservable
{
    public const decimal Percent = 0;
    private readonly List<IObserver> _observers = new (0);
    public CreditConditions(Amount limit, Amount fee, Amount restriction)
    {
        Limit = limit.Sum;
        Fee = fee.Sum;
        Restriction = restriction.Sum;
    }

    public decimal Limit { get; private set; }
    public decimal Fee { get; }
    public decimal Restriction { get; }

    public void NotifyObservers(decimal newValue)
    {
        foreach (IObserver observer in _observers)
        {
            observer.Update("Credit limit", newValue);
        }
    }

    public void AddObserver(IObserver observer)
    {
        if (_observers.Contains(observer))
        {
            throw ObservableException.ClientIsAlreadySubscribed();
        }

        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            throw ObservableException.ClientIsNotSubscribed();
        }

        _observers.Remove(observer);
    }

    public void ChangeLimit(Amount newLimit)
    {
        Limit = newLimit.Sum;
        NotifyObservers(newLimit.Sum);
    }
}
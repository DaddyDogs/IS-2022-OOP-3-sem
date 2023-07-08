using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Models;

public class DebitConditions : IObservable
{
    public const int Fee = 0;
    private readonly List<IObserver> _observers = new (0);
    public DebitConditions(Amount percent, Amount restriction)
    {
        Percent = percent.Sum;
        Restriction = restriction.Sum;
    }

    public decimal Percent { get; private set; }
    public decimal Restriction { get; }
    public void NotifyObservers(decimal newValue)
    {
        foreach (IObserver observer in _observers)
        {
            observer.Update("Interest percent", newValue);
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

    public void ChangePercent(Amount newPercent)
    {
        Percent = newPercent.Sum;
        NotifyObservers(newPercent.Sum);
    }
}
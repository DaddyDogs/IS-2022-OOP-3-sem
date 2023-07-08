namespace Banks.Interfaces;

public interface IObservable
{
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers(decimal newValue);
}
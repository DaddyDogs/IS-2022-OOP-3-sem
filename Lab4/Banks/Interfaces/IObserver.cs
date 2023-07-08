namespace Banks.Interfaces;

public interface IObserver
{
    void SubscribeToLimitUpdate(IBank bank);
    void UnsubscribeToLimitUpdate(IBank bank);
    void SubscribeToPercentUpdate(IBank bank);
    void UnsubscribeToPercentUpdate(IBank bank);
    void Update(string subject, decimal newValue);
    void SetNotifier(INotifier notifier);
}
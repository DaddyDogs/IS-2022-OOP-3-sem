namespace Banks.Interfaces;

public interface INotifier
{
    void Notify(string subject, decimal newValue);
}
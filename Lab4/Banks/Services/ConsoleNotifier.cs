using Banks.Interfaces;

namespace Banks.Services;

public class ConsoleNotifier : INotifier
{
    public void Notify(string subject, decimal newValue)
    {
        Console.WriteLine($"{subject} was changed and now amounts {newValue}");
    }
}
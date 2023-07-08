using System.Timers;
using Banks.Interfaces;

namespace Banks.Services;

public class Clock : IDisposable, IClock
{
    private const int Day = 1000 * 60 * 60 * 24;
    private int _invocationNumber = 0;
    public Clock()
    {
        Timer1 = new System.Timers.Timer(Day);
        Timer1.Elapsed += OnTimed1Event;
        Timer1.AutoReset = true;
        Timer1.Enabled = true;
    }

    public delegate void AccountHandler();

    public event AccountHandler? Notify1;
    public event AccountHandler? Notify2;
    public System.Timers.Timer Timer1 { get; }

    public void Dispose()
    {
        Timer1.Dispose();
    }

    public void Accelerate(int daysNumber)
    {
        for (int i = 0; i < daysNumber; i++)
        {
            Notify1?.Invoke();
            Update();
        }
    }

    private void OnTimed1Event(object? sender, ElapsedEventArgs e)
    {
        Notify1?.Invoke();
        Update();
    }

    private void Update()
    {
        _invocationNumber += 1;
        if (_invocationNumber == DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
        {
            _invocationNumber = 0;
            Notify2?.Invoke();
        }
    }
}
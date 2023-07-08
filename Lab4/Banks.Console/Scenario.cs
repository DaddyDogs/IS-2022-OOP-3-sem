using Banks.Interfaces;
using Banks.Services;

namespace Banks.Console;

public class Scenario
{
    private CentralBank _centralBank;

    public Scenario(CentralBank centralBank)
    {
        _centralBank = centralBank;
    }

    public IBank ChooseBank()
    {
        System.Console.WriteLine("Choose the bank:");
        foreach (IBank b in _centralBank.Banks)
        {
            System.Console.WriteLine($"{b.GetName()}");
        }

        string? answer = System.Console.ReadLine();
        while (answer is null || (_centralBank.Banks.FirstOrDefault(b => b.GetName() == answer) is null))
        {
            System.Console.WriteLine("Invalid input");
            answer = System.Console.ReadLine();
        }

        return _centralBank.Banks.First(b => b.GetName() == answer);
    }
}
namespace Banks.Console;
using System;
public class ExitHandler : IConsoleHandler
{
    private IConsoleHandler _successor;

    public ExitHandler(IConsoleHandler successor)
    {
        _successor = successor;
    }

    public void HandleRequest(string command, int state)
    {
        if (command == "Exit")
        {
            Console.WriteLine("Bye");
            Environment.Exit(0);
        }
        else
        {
            _successor.HandleRequest(command, state);
        }
    }
}
namespace Banks.Console;

public class InvalidOperationHandler : IConsoleHandler
{
    private IConsoleHandler _successor;

    public InvalidOperationHandler(IConsoleHandler successor)
    {
        _successor = successor;
    }

    public void HandleRequest(string command, int state)
    {
        System.Console.WriteLine("Invalid request. You can choose operation from suggested");
        string? answer = System.Console.ReadLine();
        while (string.IsNullOrWhiteSpace(answer))
        {
            System.Console.WriteLine("Wait for the command");
            answer = System.Console.ReadLine();
        }

        _successor.HandleRequest(answer, state);
    }
}
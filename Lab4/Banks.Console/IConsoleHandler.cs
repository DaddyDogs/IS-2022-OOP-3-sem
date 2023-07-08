namespace Banks.Console;

public interface IConsoleHandler
{
    void HandleRequest(string command, int state);
}
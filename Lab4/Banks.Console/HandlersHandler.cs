using System.Reflection.Metadata;

namespace Banks.Console;

public class HandlersHandler : IConsoleHandler
{
    public HandlersHandler()
    {
        CreationHandler creationHandler = new CreationHandler();
        ExitHandler = new ExitHandler(creationHandler);
        InvalidOperationHandler = new InvalidOperationHandler(ExitHandler);
        UpdateHandler updateHandler = new UpdateHandler(InvalidOperationHandler, creationHandler.CentralBank);
        var transactionHandler = new TransactionHandler(updateHandler, creationHandler.CentralBank);
        creationHandler.SetSuccessor1(transactionHandler);
        creationHandler.SetSuccessor2(InvalidOperationHandler);
    }

    public ExitHandler ExitHandler { get; private set; }
    public InvalidOperationHandler InvalidOperationHandler { get; private set; }
    public void HandleRequest(string command, int state)
    {
        ExitHandler.HandleRequest(command, state);
    }
}
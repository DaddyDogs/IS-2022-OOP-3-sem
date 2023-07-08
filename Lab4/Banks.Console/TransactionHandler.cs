using Banks.Entities.Client;
using Banks.Interfaces;
using Banks.Models;
using Banks.Services;

namespace Banks.Console;

public class TransactionHandler : IConsoleHandler
{
    private IConsoleHandler _successor;
    private ICentralBank _centralBank;

    public TransactionHandler(IConsoleHandler successor, ICentralBank centralBank)
    {
        _successor = successor;
        _centralBank = centralBank;
    }

    public void HandleRequest(string command, int state)
    {
        if (command == "Make transaction")
        {
            if (state < 4)
            {
                System.Console.WriteLine("You have to create an account at first");
                _successor.HandleRequest(command, state);
            }
            else
            {
                var client = ChooseClient();

                var accountId = ChooseAccountId(client);
                System.Console.WriteLine("Input amount");
                string? answer = System.Console.ReadLine();
                decimal answerDecimal = 0;
                while (string.IsNullOrWhiteSpace(answer) || !decimal.TryParse(answer, out answerDecimal) || answerDecimal <= 0)
                {
                    System.Console.WriteLine("Invalid input");
                    answer = System.Console.ReadLine();
                }

                decimal amount = answerDecimal;
                System.Console.WriteLine("Choose operation: \n replenish \n withdraw \n transfer");
                answer = System.Console.ReadLine();
                while (answer != "replenish" && answer != "withdraw" && answer != "transfer")
                {
                    System.Console.WriteLine("Invalid input");
                    answer = System.Console.ReadLine();
                }

                switch (answer)
                {
                    case "replenish":
                    {
                        _centralBank.ReplenishAccount(new Amount(amount), accountId);
                        break;
                    }

                    case "withdraw":
                    {
                        _centralBank.WithDrawMoney(new Amount(amount), accountId);
                        break;
                    }

                    case "transfer":
                    {
                        System.Console.WriteLine("Choose the recipient:");
                        var recipient = ChooseClient();
                        _centralBank.TransferMoney(new Amount(amount), accountId, ChooseAccountId(recipient));
                        break;
                    }
                }
            }
        }
        else
        {
            _successor.HandleRequest(command, state);
        }
    }

    public Client ChooseClient()
    {
        System.Console.WriteLine("Choose the client (print number):");

        var clients = new List<Client>(0);

        foreach (IBank b in _centralBank.GetBanks())
        {
            foreach (var c in b.GetClients())
            {
                System.Console.WriteLine($"{c.LastName + " " + c.FirstName}");
                clients.Add(c);
            }
        }

        string? answer = System.Console.ReadLine();
        int ans = 0;
        while (string.IsNullOrWhiteSpace(answer) || !int.TryParse(answer, out ans) || ans <= 0 || ans > clients.Count)
        {
            System.Console.WriteLine("Invalid input");
            answer = System.Console.ReadLine();
        }

        return clients[ans - 1];
    }

    public Guid ChooseAccountId(Client client)
    {
        System.Console.WriteLine("Choose the account (print number):");
        List<Guid> accountsId = new List<Guid>(0);

        foreach (IAccount account in client.Accounts)
        {
            System.Console.WriteLine(account.GetId());
            accountsId.Add(account.GetId());
        }

        string? answer = System.Console.ReadLine();
        int ans = 0;
        while (string.IsNullOrWhiteSpace(answer) || !int.TryParse(answer, out ans) || ans <= 0 || ans > client.Accounts.Count())
        {
            System.Console.WriteLine("Invalid input");
            answer = System.Console.ReadLine();
        }

        return accountsId[ans - 1];
    }
}
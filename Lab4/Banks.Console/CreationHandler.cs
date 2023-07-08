using Banks.Entities;
using Banks.Entities.Client;
using Banks.Interfaces;
using Banks.Models;
using Banks.Models.Address;
using Banks.Services;

namespace Banks.Console;

public class CreationHandler : IConsoleHandler
{
    private IConsoleHandler? _successor;
    private IConsoleHandler? _successor2;
    public CentralBank CentralBank { get; } = new CentralBank();

    public void SetSuccessor1(IConsoleHandler successor)
    {
        _successor = successor;
    }

    public void SetSuccessor2(IConsoleHandler successor2)
    {
        _successor2 = successor2;
    }

    public void HandleRequest(string command, int state)
    {
        if (command.Split()[0] != "Create")
        {
            _successor!.HandleRequest(command, state);
        }
        else
        {
            string item = command.Split()[1];
            if (item == "bank")
            {
                System.Console.WriteLine("Input the name");
                string? name = System.Console.ReadLine();
                CheckForNull(name);

                System.Console.WriteLine("Input the maximal amount and interest percent for deposit account");

                var depositConditions = new List<DepositCondition>(0);

                string? answer = System.Console.ReadLine();
                CheckForNull(answer);
                while (!string.IsNullOrWhiteSpace(answer))
                {
                    depositConditions.Add(new DepositCondition(new Amount(Convert.ToDecimal(answer.Split()[0])), new Amount(Convert.ToDecimal(answer.Split()[1]))));
                    answer = System.Console.ReadLine();
                }

                System.Console.WriteLine("Input the term of the deposit in days");
                answer = System.Console.ReadLine();
                while (answer is null)
                {
                    answer = System.Console.ReadLine();
                    System.Console.WriteLine("Invalid input");
                }

                var term = new TimeSpan(Convert.ToInt32(answer), 0, 0, 0);

                System.Console.WriteLine("Input the amount of restriction for suspicious clients for deposit");
                answer = System.Console.ReadLine();
                while (answer is null)
                {
                    answer = System.Console.ReadLine();
                }

                var amount = new Amount(Convert.ToDecimal(answer));

                var depositCondition = new DepositConditions(depositConditions, term, amount);

                System.Console.WriteLine("Input limit, fee and restriction for suspicious clients for credit");
                answer = System.Console.ReadLine();
                CheckForNull(answer);

                var creditCondition = new CreditConditions(new Amount(Convert.ToDecimal(answer!.Split()[0])), new Amount(Convert.ToDecimal(answer.Split()[1])), new Amount(Convert.ToDecimal(answer.Split()[1])));

                System.Console.WriteLine("Input percent and restriction for suspicious clients for debit");
                answer = System.Console.ReadLine();
                while (answer is null)
                {
                    System.Console.WriteLine("Invalid input");
                    answer = System.Console.ReadLine();
                }

                var debitCondition = new DebitConditions(new Amount(Convert.ToDecimal(answer.Split()[0])), new Amount(Convert.ToDecimal(answer.Split()[1])));
                CentralBank.CreateBank(name!, depositCondition, creditCondition, debitCondition);
                System.Console.WriteLine("Bank was successfully created!");
            }
            else if (item == "client")
            {
                if (state < 2)
                {
                    System.Console.WriteLine("You have to create a bank at first");
                    _successor2!.HandleRequest(command, state);
                }

                System.Console.WriteLine("Choose the bank:");
                foreach (IBank b in CentralBank.Banks)
                {
                    System.Console.WriteLine($"{b.GetName()}");
                }

                string? answer = System.Console.ReadLine();
                while (answer is null || (CentralBank.Banks.FirstOrDefault(b => b.GetName() == answer) is null))
                {
                    System.Console.WriteLine("Invalid input");
                    answer = System.Console.ReadLine();
                }

                var bank = CentralBank.Banks.First(b => b.GetName() == answer);

                System.Console.WriteLine("Input the last name");
                string? name = System.Console.ReadLine();
                CheckForNull(name);

                var lastname = name;

                System.Console.WriteLine("Input the first name");
                name = System.Console.ReadLine();
                CheckForNull(name);

                var firstname = name;

                System.Console.WriteLine("Want to fill out account to have all access?");
                answer = System.Console.ReadLine();
                while (string.IsNullOrWhiteSpace(answer) || (answer != "Yes" && answer != "No"))
                {
                    System.Console.WriteLine("Invalid input");
                    answer = System.Console.ReadLine();
                }

                if (answer == "No")
                {
                    bank.RegisterClient(firstname!, lastname!, null, null);
                }
                else
                {
                    System.Console.WriteLine("Input passport series");
                    answer = System.Console.ReadLine();
                    int ans = 0;
                    while (string.IsNullOrWhiteSpace(answer) || !int.TryParse(answer, out ans) || answer.Length != 4)
                    {
                        System.Console.WriteLine("Invalid input");
                        answer = System.Console.ReadLine();
                    }

                    int series = ans;

                    System.Console.WriteLine("Input passport number");
                    answer = System.Console.ReadLine();
                    ans = 0;
                    while (string.IsNullOrWhiteSpace(answer) || !int.TryParse(answer, out ans) || answer.Length != 6)
                    {
                        System.Console.WriteLine("Invalid input");
                        answer = System.Console.ReadLine();
                    }

                    int numberPassport = ans;

                    System.Console.WriteLine("Input your address \n street: ");
                    answer = System.Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(answer))
                    {
                        System.Console.WriteLine("Invalid input");
                        answer = System.Console.ReadLine();
                    }

                    string street = answer;
                    System.Console.WriteLine("\n building number: ");
                    answer = System.Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(answer))
                    {
                        System.Console.WriteLine("Invalid input");
                        answer = System.Console.ReadLine();
                    }

                    string bulding = answer;

                    System.Console.WriteLine("\n flat number: ");
                    answer = System.Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(answer) || !int.TryParse(answer, out ans))
                    {
                        System.Console.WriteLine("Invalid input");
                        answer = System.Console.ReadLine();
                    }

                    int flat = ans;

                    bank.RegisterClient(firstname!, lastname!, Address.Builder.WithStreet(street).WithBuilding(bulding).WitFlat(flat).Build(), new Passport(series, numberPassport));
                }

                System.Console.WriteLine("Client was registered successfully");
            }
            else if (item == "account")
            {
                if (state < 3)
                {
                    System.Console.WriteLine("You have to create a client at first");
                    _successor2!.HandleRequest(command, state);
                }

                System.Console.WriteLine("Choose the client (print number):");

                var clients = new List<Client>(0);

                foreach (IBank b in CentralBank.Banks)
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

                var client = clients[ans - 1];

                System.Console.WriteLine("Choose the bank:");
                var banks = new List<IBank>(0);
                foreach (IBank b in CentralBank.Banks.Where(b => b.GetClients().Contains(client)))
                {
                    System.Console.WriteLine($"{b.GetName()}");
                    banks.Add(b);
                }

                answer = System.Console.ReadLine();
                while (answer is null || (banks.Find(b => b.GetName() == answer) is null))
                {
                    System.Console.WriteLine("Invalid input");
                    answer = System.Console.ReadLine();
                }

                IBank bank = banks.Find(b => b.GetName() == answer) !;

                System.Console.WriteLine("Choose account's type: \n Credit \n Debit \n Deposit");
                answer = System.Console.ReadLine();
                while (string.IsNullOrWhiteSpace(answer) ||
                       (answer != "Credit" && answer != "Debit" && answer != "Deposit"))
                {
                    System.Console.WriteLine("Invalid input");
                    answer = System.Console.ReadLine();
                }

                switch (answer)
                {
                    case "Credit":
                    {
                        bank.RegisterCreditAccount(client);
                        break;
                    }

                    case "Debit":
                    {
                        bank.RegisterDebitAccount(client);
                        break;
                    }

                    case "Deposit":
                    {
                        System.Console.WriteLine("Input the amount you want to put on the account");
                        while (string.IsNullOrWhiteSpace(answer) || int.TryParse(answer, out ans) || ans < 0)
                        {
                            System.Console.WriteLine("Invalid input");
                            answer = System.Console.ReadLine();
                        }

                        bank.RegisterDepositAccount(client, new Amount(ans));
                        break;
                    }
                }

                System.Console.WriteLine("Account was created successfully!");
            }
            else
            {
                _successor2!.HandleRequest(command, state);
            }
        }
    }

    public void CheckForNull(string? answer)
    {
        while (string.IsNullOrWhiteSpace(answer))
        {
            System.Console.WriteLine("Invalid input");
            answer = System.Console.ReadLine();
        }
    }
}
using Banks.Entities.Client;
using Banks.Interfaces;
using Banks.Models.Address;
using Banks.Services;

namespace Banks.Console;

public class UpdateHandler : IConsoleHandler
{
    private IConsoleHandler _successor;
    private ICentralBank _centralBank;

    public UpdateHandler(IConsoleHandler successor, ICentralBank centralBank)
    {
        _successor = successor;
        _centralBank = centralBank;
    }

    public void HandleRequest(string command, int state)
    {
        if (command == "Update client")
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

            var client = clients[ans - 1];
            System.Console.WriteLine("What do you want to update: passport or address?");
            answer = System.Console.ReadLine();
            while (answer != "passport" && answer != "address")
            {
                System.Console.WriteLine("Invalid input");
                answer = System.Console.ReadLine();
            }

            switch (answer)
            {
                case "passport":
                {
                    System.Console.WriteLine("Input passport series");
                    answer = System.Console.ReadLine();
                    ans = 0;
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
                    client.SetPassport(series, numberPassport);
                    break;
                }

                case "address":
                {
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
                    client.SetAddress(Address.Builder.WithStreet(street).WithBuilding(bulding).WitFlat(flat).Build());
                    break;
                }
            }
        }
    }
}
using System.Globalization;
using System.Net.Mime;
using Banks.Console;

Console.WriteLine("Hello! Choose the command: \n Create bank \n Exit");
string? answer = Console.ReadLine();
while (answer is null)
{
    Console.WriteLine("Invalid input");
    answer = Console.ReadLine();
}

var handler = new HandlersHandler();
handler.HandleRequest(answer, 1);

Console.WriteLine("Choose the command: \n Create client \n Create bank \n Exit");
answer = Console.ReadLine();
while (answer is null)
{
    Console.WriteLine("Invalid input");
    answer = Console.ReadLine();
}

handler.HandleRequest(answer, 2);

Console.WriteLine("Choose the command: \n Create client \n Create bank \n Create account \n Update client \n Exit");
answer = Console.ReadLine();
while (answer is null)
{
    Console.WriteLine("Invalid input");
    answer = Console.ReadLine();
}

handler.HandleRequest(answer, 3);

Console.WriteLine("Choose the command: \n Create client \n Create bank \n Create account \n Make transaction \n Update client \n Exit");
answer = Console.ReadLine();
while (answer != "Exit")
{
    while (answer is null)
    {
        Console.WriteLine("Invalid input");
        answer = Console.ReadLine();
    }

    handler.HandleRequest(answer, 4);

    Console.WriteLine("Choose the command: \n Create client \n Create bank \n Create account \n Make transaction \n Update client \n Exit");
    answer = Console.ReadLine();
}

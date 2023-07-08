namespace Banks.Exceptions;

public class AddressException : BanksException
{
    private AddressException(string message)
        : base(message) { }

    public static AddressException InvalidNumberException(string subject, int number)
    {
        throw new AddressException($"{subject} number {number} is invalid. It can't be under 1");
    }
}
namespace Banks.Exceptions;

public class BanksException : Exception
{
    protected BanksException(string message)
    : base(message) { }
}
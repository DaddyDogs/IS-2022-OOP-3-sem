namespace Banks.Exceptions;

public class PassportException : BanksException
{
    private PassportException(string message)
        : base(message) { }

    public static PassportException InvalidPassportSeriesException(int series)
    {
        throw new PassportException($"Series {series} is invalid. It should contains 4 numbers");
    }

    public static PassportException InvalidPassportNumberException(int number)
    {
        throw new PassportException($"Number {number} is invalid. It should contains 6 numbers");
    }
}
using Banks.Exceptions;

namespace Banks.Models;

public class Passport
{
    public Passport(int series, int number)
    {
        if (series.ToString().Length != 4)
        {
            throw PassportException.InvalidPassportSeriesException(series);
        }

        if (number.ToString().Length != 6)
        {
            throw PassportException.InvalidPassportNumberException(number);
        }

        Series = series;
        Number = number;
    }

    public int Series { get; }
    public int Number { get; }
}
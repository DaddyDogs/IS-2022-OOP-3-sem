namespace Banks.Exceptions;

public class ObservableException : BanksException
{
    private ObservableException(string message)
        : base(message) { }

    public static ObservableException ClientIsAlreadySubscribed()
    {
        throw new ObservableException("Can't subscribe client because he is already subscribed");
    }

    public static ObservableException ClientIsNotSubscribed()
    {
        throw new ObservableException("Can't unsubscribe client because he is not subscribed");
    }
}
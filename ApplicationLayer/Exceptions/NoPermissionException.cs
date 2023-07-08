namespace ApplicationLayer.Exceptions;

public class NoPermissionException : ApplicationException
{
    protected NoPermissionException(string? message)
        : base(message) { }

    public static NoPermissionException EmployeeIsNotYourSubordinate(Guid id)
    {
        return new NoPermissionException($"Employee {id} is not your subordinate");
    }

    public static NoPermissionException MessageDoesNotBelongYourAccount(Guid id)
    {
        return new NoPermissionException($"Message {id} does not belong your account");
    }

    public static NoPermissionException MessageSourceDoesNotBelongYourAccount(Guid id)
    {
        return new NoPermissionException($"Message source {id} does not belong your account");
    }

    public static NoPermissionException NoAccessForReport()
    {
        return new NoPermissionException($"You can't make a report because you're a not a supervisor");
    }
}
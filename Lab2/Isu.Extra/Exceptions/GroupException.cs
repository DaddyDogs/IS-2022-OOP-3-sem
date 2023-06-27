namespace Isu.Extra.Exceptions;

public class GroupException : IsuExtraException
{
    public GroupException(string message)
        : base(message)
    {
    }

    public static GroupException FullGroupException(string groupName)
    {
        return new GroupException($"GroupName {groupName} is full. Cannot add the student");
    }

    public static GroupException StudentIsAlreadyInThisGroupException(string name, int id, string groupName)
    {
        return new GroupException($"Student {name} with id {id} is already in group {groupName}");
    }

    public static GroupException StudentIsNotInThisGroupException(string name, int id, string groupName)
    {
        return new GroupException($"Student {name} with id {id} is not in group {groupName}. " +
                                           $"You cannot remove him from this group.");
    }
}
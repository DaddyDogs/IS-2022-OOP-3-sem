using Isu.Extra.Entities;
using Isu.Models;

namespace Isu.Extra.Services;

public class GroupDecoratorService
{
    private readonly List<GroupDecorator> _listGroup;

    public GroupDecoratorService()
    {
        _listGroup = new List<GroupDecorator>(0);
    }

    public GroupDecorator AddGroup(GroupName groupName, Schedule schedule)
    {
        var newGroup = new GroupDecorator(groupName, schedule);
        _listGroup.Add(newGroup);
        return newGroup;
    }

    public GroupDecorator? FindGroup(GroupName groupName)
    {
        return _listGroup.FirstOrDefault(group => group.Group.GroupName == groupName);
    }

    public List<GroupDecorator> FindGroups(CourseNumber courseNumber)
    {
        return _listGroup.Where(group => group.Group.GroupName.CourseNumber == courseNumber).ToList();
    }
}
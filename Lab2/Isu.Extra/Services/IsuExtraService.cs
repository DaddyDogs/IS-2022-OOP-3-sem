using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Services;

public class IsuExtraService : IIsuExtraService
{
    private readonly List<IntegratedStudyGroups> _isgList;

    public IsuExtraService()
    {
        _isgList = new List<IntegratedStudyGroups>(0);
    }

    public IntegratedStudyGroups AddIntegratedStudyGroups(string name, MegaFaculties megaFaculty)
    {
        var newIsg = new IntegratedStudyGroups(name, megaFaculty);
        _isgList.Add(newIsg);
        return newIsg;
    }
}
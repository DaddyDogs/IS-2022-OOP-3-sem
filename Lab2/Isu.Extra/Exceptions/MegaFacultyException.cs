using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Exceptions;

public class MegaFacultyException : IsuExtraException
{
    private MegaFacultyException(string message)
        : base(message) { }
    public static MegaFacultyException NonExistentGroupException(char faculty)
    {
        return new MegaFacultyException($"There is no beginning with letter {faculty} group in ITMO");
    }

    public static MegaFacultyException InvalidIsgChoiceException(string stream)
    {
        return new MegaFacultyException($"You can't choose stream {stream} from your megafaculty");
    }

    public static MegaFacultyException NonExistentIsgStreamException(string name)
    {
        return new MegaFacultyException($"There is no ISG stream {name} in ITMO");
    }
}
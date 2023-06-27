using Isu.Extra.Entities;
using Isu.Extra.Models;
namespace Isu.Extra.Services;

public interface IIsuExtraService
{
    IntegratedStudyGroups AddIntegratedStudyGroups(string name, MegaFaculties megaFaculty);
}
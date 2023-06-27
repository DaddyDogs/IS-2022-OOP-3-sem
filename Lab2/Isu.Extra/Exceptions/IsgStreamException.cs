using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class IsgStreamException : IsuExtraException
{
    private IsgStreamException(string message)
        : base(message) { }
    public static IsgStreamException FullGroupException(IsgStream isgStream)
    {
        return new IsgStreamException($"Isg stream {isgStream.GroupName} is full");
    }
}
using ApplicationLayer.Exceptions;
using DataAccessLayer.AbstractClasses;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.Implementations;

public class ReportMakerEmployeeVisitor : IEmployeeVisitor
{
    private readonly List<MessageSourceInfo> _messageSourceInfo;
    private readonly List<Message> _messages;
    public ReportMakerEmployeeVisitor()
    {
        HandledMessagesCount = 0;
        _messageSourceInfo = new List<MessageSourceInfo>();
        _messages = new List<Message>();
    }

    public int HandledMessagesCount { get; private set; }
    public IReadOnlyList<MessageSourceInfo> MessageSourceInfos => _messageSourceInfo;
    public IReadOnlyList<Message> Messages => _messages;

    public void Visit(Employee employee)
    {
        HandledMessagesCount += employee.Messages.Count(x => x.DateTime.Day == DateTime.Now.Day);
        if (employee.PrivateAccount is null)
        {
            throw NotFoundException.EntityNotFoundException<PrivateAccount>(employee.Identifier);
        }

        foreach (Account account in employee.PrivateAccount.Accounts)
        {
            _messageSourceInfo.Add(new MessageSourceInfo(account.MessageSource, account.MessageSource.Messages.Count(x => x.DateTime.Day == DateTime.Now.Day)));
            _messages.AddRange(account.MessageSource.Messages);
        }
    }
}
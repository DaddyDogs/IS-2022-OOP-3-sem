using DataAccessLayer.AbstractClasses;

namespace DataAccessLayer.Models;

public class Report
{
    public Report(DateTime dateTime, Guid creatorId, int handledMessagesCount, int messagesDuringTime, IReadOnlyList<MessageSourceInfo> receivedMessages)
    {
        DateTime = dateTime;
        CreatorId = creatorId;
        HandledMessagesCount = handledMessagesCount;
        MessagesDuringTime = messagesDuringTime;
        ReceivedMessages = receivedMessages;
    }

    #pragma warning disable CS8618
    protected Report() { }
    #pragma warning restore CS8618

    public virtual int HandledMessagesCount { get; private set; }
    public virtual IReadOnlyList<MessageSourceInfo> ReceivedMessages { get; private set; }
    public virtual int MessagesDuringTime { get; private set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CreatorId { get; set; }
    public DateTime DateTime { get; set; }
}
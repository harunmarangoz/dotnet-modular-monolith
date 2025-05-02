namespace Shared.Contracts.Messages;

public interface CreateClickEventMessage
{
    public Guid LinkId { get; set; }
    public string LinkUniqueKey { get; set; }

    public string UserAgent { get; set; }
    public string IpAddress { get; set; }
}
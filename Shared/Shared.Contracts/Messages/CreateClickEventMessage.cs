namespace Shared.Contracts.Messages;

public class CreateClickEventMessage
{
    public Guid LinkId { get; set; }
    public string LinkUniqueKey { get; set; }

    public string UserAgent { get; set; }
    public string IpAddress { get; set; }
}
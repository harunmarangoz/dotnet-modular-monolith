using Shared.Domain.Entities;

namespace AnalyticModule.Domain.Entities;

public class ClickEvent : BaseEntity
{
    public Guid LinkId { get; set; }
    public string LinkUniqueKey { get; set; }

    public string UserAgent { get; set; }
    public string IpAddress { get; set; }
    public DateTime OccurredAt { get; set; }
}
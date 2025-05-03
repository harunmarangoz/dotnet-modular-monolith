using MediatR;

namespace AnalyticModule.Application.ClickEventRequests.Commands;

public record CreateClickEventCommand(CreateClickEventCommandParameter Parameter) : IRequest;

public class CreateClickEventCommandParameter
{
    public Guid LinkId { get; set; }
    public string LinkUniqueKey { get; set; }

    public string UserAgent { get; set; }
    public string IpAddress { get; set; }
    public DateTime OccurredAt { get; set; }
}
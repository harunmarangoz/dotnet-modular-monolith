using MediatR;
using Shared.Application.Results;

namespace AnalyticModule.Application.ClickEventRequests.Queries;

public record ReportClickEventCommand(Guid LinkId) : IRequest<DataResult<ReportClickEventCommandResult>>;

public class ReportClickEventCommandResult
{
    public Guid LinkId { get; set; }
    public string LinkName { get; set; }
    public string LinkUrl { get; set; }
    public string LinkUniqueKey { get; set; }

    public string QrBase64Image { get; set; }

    public int TotalClickCount { get; set; }
    public int Last7DaysClickCount { get; set; }
    public int Last30DaysClickCount { get; set; }

    public DateTime? LastClickDate { get; set; }
}
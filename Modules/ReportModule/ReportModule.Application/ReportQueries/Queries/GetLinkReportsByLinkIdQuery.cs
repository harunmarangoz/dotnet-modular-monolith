using MediatR;

namespace ReportModule.Application.ReportQueries.Queries;

public record GetLinkReportsByLinkIdQuery(Guid Id) : IRequest<GetLinkReportsByLinkIdQueryResult>;

public class GetLinkReportsByLinkIdQueryResult
{
    public string Name { get; set; }
    public string Description { get; set; }

    public int TotalClicked { get; set; }
    public int Last7DaysClicked { get; set; }
    public int Last30DaysClicked { get; set; }
}
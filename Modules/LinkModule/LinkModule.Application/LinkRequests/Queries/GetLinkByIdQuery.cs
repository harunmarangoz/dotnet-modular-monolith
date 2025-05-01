using MediatR;

namespace LinkModule.Application.LinkRequests.Queries;

public record GetLinkByIdQuery(Guid Id) : IRequest<GetLinkByIdQueryResult>;

public class GetLinkByIdQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string UniqueKey { get; set; }
}
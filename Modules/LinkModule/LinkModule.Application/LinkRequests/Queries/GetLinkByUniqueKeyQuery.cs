using MediatR;

namespace LinkModule.Application.LinkRequests.Queries;

public record GetLinkByUniqueKeyQuery(string UniqueKey) : IRequest<GetLinkByUniqueKeyQueryResult>;

public class GetLinkByUniqueKeyQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string UniqueKey { get; set; }
}
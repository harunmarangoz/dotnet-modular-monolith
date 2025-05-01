using LinkModule.Application.LinkRequests.Queries;
using LinkModule.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;

namespace LinkModule.Infrastructure.UrlRequests.Queries;

public class GetLinkByIdQueryHandler(IDbContextFactory<LinkModuleDatabaseContext> contextFactory)
    : IRequestHandler<GetLinkByIdQuery, GetLinkByIdQueryResult>
{
    public async Task<GetLinkByIdQueryResult> Handle(GetLinkByIdQuery request, CancellationToken cancellationToken)
    {
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var link = await context.Links.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (link == null) throw new AppNotFoundException("Link not found");

        return new GetLinkByIdQueryResult()
        {
            Id = link.Id,
            Name = link.Name,
            Url = link.Url,
            UniqueKey = link.UniqueKey
        };
    }
}
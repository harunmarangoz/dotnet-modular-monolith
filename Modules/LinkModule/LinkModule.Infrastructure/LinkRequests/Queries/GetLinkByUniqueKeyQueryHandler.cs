using System.Text.Json;
using LinkModule.Application.LinkRequests.Queries;
using LinkModule.Contracts.Constants;
using LinkModule.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Domain.Exceptions;

namespace LinkModule.Infrastructure.LinkRequests.Queries;

public class GetLinkByUniqueKeyQueryHandler(
    IDbContextFactory<LinkModuleDatabaseContext> contextFactory,
    IDistributedCache cache)
    : IRequestHandler<GetLinkByUniqueKeyQuery, GetLinkByUniqueKeyQueryResult>
{
    public async Task<GetLinkByUniqueKeyQueryResult> Handle(GetLinkByUniqueKeyQuery request,
        CancellationToken cancellationToken)
    {
        var cacheKey = string.Format(LinkConstants.LinkUniqueKeyCacheKey, request.UniqueKey);
        var cachedLinkStr = await cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedLinkStr))
        {
            var cachedLink = JsonSerializer.Deserialize<GetLinkByUniqueKeyQueryResult>(cachedLinkStr);
            if (cachedLink != null) return cachedLink;
        }

        var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var link = await context.Links.FirstOrDefaultAsync(x => x.UniqueKey == request.UniqueKey, cancellationToken);
        if (link == null) throw new AppNotFoundException("Link not found");

        return new GetLinkByUniqueKeyQueryResult()
        {
            Id = link.Id,
            Name = link.Name,
            Url = link.Url,
            UniqueKey = link.UniqueKey
        };
    }
}
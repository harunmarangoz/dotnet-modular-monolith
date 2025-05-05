using System.Text.Json;
using Shared.Application.DataTransferObjects;
using LinkModule.Application.LinkRequests.Queries;
using LinkModule.Contracts.Constants;
using LinkModule.Persistence.Contexts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Application.Results;

namespace LinkModule.Infrastructure.LinkRequests.Queries;

public class GetLinkByUniqueKeyQueryHandler(
    IDbContextFactory<LinkModuleDatabaseContext> contextFactory,
    IDistributedCache cache)
    : IRequestHandler<GetLinkByUniqueKeyQuery, DataResult<LinkDto>>
{
    public async Task<DataResult<LinkDto>> Handle(GetLinkByUniqueKeyQuery request,
        CancellationToken cancellationToken)
    {
        var cacheKey = string.Format(LinkConstants.LinkUniqueKeyCacheKey, request.UniqueKey);
        var cachedLinkStr = await cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedLinkStr))
        {
            var cachedLink = JsonSerializer.Deserialize<LinkDto>(cachedLinkStr);
            if (cachedLink != null) return DataResult<LinkDto>.Success(cachedLink);
        }

        var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var link = await context.Links.FirstOrDefaultAsync(x => x.UniqueKey == request.UniqueKey, cancellationToken);
        if (link == null) return DataResult<LinkDto>.Failure("Link not found");

        return DataResult<LinkDto>.Success(link.Adapt<LinkDto>());
    }
}
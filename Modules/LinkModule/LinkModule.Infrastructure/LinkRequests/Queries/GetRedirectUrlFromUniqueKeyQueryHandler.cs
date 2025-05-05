using LinkModule.Application.LinkRequests.Queries;
using LinkModule.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Results;

namespace LinkModule.Infrastructure.LinkRequests.Queries;

public class GetRedirectUrlFromUniqueKeyQueryHandler(IDbContextFactory<LinkModuleDatabaseContext> contextFactory)
    : IRequestHandler<GetRedirectUrlFromUniqueKeyQuery, DataResult<string>>
{
    public async Task<DataResult<string>> Handle(GetRedirectUrlFromUniqueKeyQuery request,
        CancellationToken cancellationToken)
    {
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await context.Links
            .FirstOrDefaultAsync(x => x.UniqueKey == request.UniqueKey, cancellationToken);

        if (entity == null) return DataResult<string>.Failure("No link found for unique key");

        return DataResult<string>.Success(entity.Url);
    }
}
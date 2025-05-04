using LinkModule.Application.LinkRequests.Queries;
using LinkModule.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;

namespace LinkModule.Infrastructure.LinkRequests.Queries;

public class GetRedirectUrlFromUniqueKeyQueryHandler(IDbContextFactory<LinkModuleDatabaseContext> contextFactory)
    : IRequestHandler<GetRedirectUrlFromUniqueKeyQuery, string>
{
    public async Task<string> Handle(GetRedirectUrlFromUniqueKeyQuery request,
        CancellationToken cancellationToken)
    {
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var entity = await context.Links
            .FirstOrDefaultAsync(x => x.UniqueKey == request.UniqueKey, cancellationToken);

        if (entity == null) throw new AppNotFoundException("Link not found");

        return entity.Url;
    }
}
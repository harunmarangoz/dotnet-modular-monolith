using LinkModule.Application.LinkRequests.DataTransferObjects;
using LinkModule.Application.LinkRequests.Queries;
using LinkModule.Persistence.Contexts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Results;

namespace LinkModule.Infrastructure.LinkRequests.Queries;

public record ListLinksQueryHandler(IDbContextFactory<LinkModuleDatabaseContext> contextFactory)
    : IRequestHandler<ListLinksQuery, ListDataResult<LinkDto>>
{
    public async Task<ListDataResult<LinkDto>> Handle(ListLinksQuery request, CancellationToken cancellationToken)
    {
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var links = await context.Links.ToListAsync(cancellationToken);
        var dto = links.Select(x => x.Adapt<LinkDto>()).ToList();

        return ListDataResult<LinkDto>.Success(dto);
    }
}
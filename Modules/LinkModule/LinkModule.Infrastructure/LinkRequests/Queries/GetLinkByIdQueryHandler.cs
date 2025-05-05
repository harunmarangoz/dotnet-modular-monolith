using Shared.Application.DataTransferObjects;
using LinkModule.Application.LinkRequests.Queries;
using LinkModule.Persistence.Contexts;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Results;

namespace LinkModule.Infrastructure.LinkRequests.Queries;

public class GetLinkByIdQueryHandler(IDbContextFactory<LinkModuleDatabaseContext> contextFactory)
    : IRequestHandler<GetLinkByIdQuery, DataResult<LinkDto>>
{
    public async Task<DataResult<LinkDto>> Handle(GetLinkByIdQuery request, CancellationToken cancellationToken)
    {
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        var link = await context.Links.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (link == null) return DataResult<LinkDto>.Failure("Link not found");

        return DataResult<LinkDto>.Success(link.Adapt<LinkDto>());
    }
}
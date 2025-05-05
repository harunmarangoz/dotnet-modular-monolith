using LinkModule.Application.LinkRequests.Queries;
using MediatR;
using Shared.Application.DataTransferObjects;
using Shared.Application.Results;
using Shared.Application.Services;

namespace LinkModule.Infrastructure.Services;

public class LinkModuleService(ISender sender) : ILinkModuleService
{
    public async Task<DataResult<string>> GetRedirectUrlFromUniqueKeyAsync(string uniqueKey,
        CancellationToken cancellationToken = default)
    {
        return await sender.Send(new GetRedirectUrlFromUniqueKeyQuery(uniqueKey), cancellationToken);
    }

    public Task<DataResult<LinkDto>> GetLinkByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return sender.Send(new GetLinkByIdQuery(id), cancellationToken);
    }

    public Task<DataResult<LinkDto>> GetLinkByUniqueKeyAsync(string uniqueKey,
        CancellationToken cancellationToken = default)
    {
        return sender.Send(new GetLinkByUniqueKeyQuery(uniqueKey), cancellationToken);
    }
}
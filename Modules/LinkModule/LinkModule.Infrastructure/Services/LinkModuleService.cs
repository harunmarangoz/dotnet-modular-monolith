using LinkModule.Application.LinkRequests.Queries;
using MediatR;
using Shared.Application.DataTransferObjects;
using Shared.Application.Results;
using Shared.Application.Services;

namespace LinkModule.Infrastructure.Services;

public class LinkModuleService(ISender sender) : ILinkModuleService
{
    public async Task<DataResult<string>> GetRedirectUrlFromUniqueKeyAsync(string uniqueKey)
    {
        return await sender.Send(new GetRedirectUrlFromUniqueKeyQuery(uniqueKey));
    }

    public Task<DataResult<LinkDto>> GetLinkByIdAsync(Guid id)
    {
        return sender.Send(new GetLinkByIdQuery(id));
    }

    public Task<DataResult<LinkDto>> GetLinkByUniqueKeyAsync(string uniqueKey)
    {
        return sender.Send(new GetLinkByUniqueKeyQuery(uniqueKey));
    }
}
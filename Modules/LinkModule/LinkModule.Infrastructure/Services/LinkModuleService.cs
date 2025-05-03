using LinkModule.Application.LinkRequests.Queries;
using MediatR;
using Shared.Application.Services;

namespace LinkModule.Infrastructure.Services;

public class LinkModuleService(ISender sender) : ILinkModuleService
{
    public async Task<string> GetRedirectUrlFromUniqueKeyAsync(string uniqueKey)
    {
        var result = await sender.Send(new GetRedirectUrlFromUniqueKeyQuery(uniqueKey));
        return result;
    }
}
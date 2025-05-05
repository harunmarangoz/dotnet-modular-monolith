using Shared.Application.Results;

namespace Shared.Application.Services;

public interface ILinkModuleService
{
    Task<DataResult<string>> GetRedirectUrlFromUniqueKeyAsync(string uniqueKey);
}
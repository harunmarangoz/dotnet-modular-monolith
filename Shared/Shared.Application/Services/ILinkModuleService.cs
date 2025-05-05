using Shared.Application.DataTransferObjects;
using Shared.Application.Results;

namespace Shared.Application.Services;

public interface ILinkModuleService
{
    Task<DataResult<string>> GetRedirectUrlFromUniqueKeyAsync(string uniqueKey);
    Task<DataResult<LinkDto>> GetLinkByIdAsync(Guid id);
    Task<DataResult<LinkDto>> GetLinkByUniqueKeyAsync(string uniqueKey);
}
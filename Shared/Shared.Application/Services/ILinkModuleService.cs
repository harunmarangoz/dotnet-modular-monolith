using Shared.Application.DataTransferObjects;
using Shared.Application.Results;

namespace Shared.Application.Services;

public interface ILinkModuleService
{
    Task<DataResult<string>> GetRedirectUrlFromUniqueKeyAsync(string uniqueKey, CancellationToken cancellationToken = default);
    Task<DataResult<LinkDto>> GetLinkByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<DataResult<LinkDto>> GetLinkByUniqueKeyAsync(string uniqueKey, CancellationToken cancellationToken = default);
}
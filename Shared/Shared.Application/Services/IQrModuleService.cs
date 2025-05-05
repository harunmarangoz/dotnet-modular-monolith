using Shared.Application.Results;

namespace Shared.Application.Services;

public interface IQrModuleService
{
    Task<DataResult<string>> GetQrImageAsync(Guid id);
}
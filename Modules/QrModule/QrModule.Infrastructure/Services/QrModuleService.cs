using MediatR;
using QrModule.Application.QrQueries.Queries;
using Shared.Application.Results;
using Shared.Application.Services;

namespace QrModule.Infrastructure.Services;

public class QrModuleService(ISender sender) : IQrModuleService
{
    public async Task<DataResult<string>> GetQrImageAsync(Guid id)
    {
        var result = await sender.Send(new GetQrByLinkIdQuery(id));
        if (result.HasError) return DataResult<string>.Failure(result.Message);
        return DataResult<string>.Success(result.Data.Base64Image);
    }
}
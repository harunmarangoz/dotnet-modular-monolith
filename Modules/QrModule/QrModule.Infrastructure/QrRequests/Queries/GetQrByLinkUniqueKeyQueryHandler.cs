using MediatR;
using QRCoder;
using QrModule.Application.QrQueries.Queries;
using Shared.Application.Results;
using Shared.Application.Services;
using Shared.Domain.Exceptions;

namespace QrModule.Infrastructure.QrRequests.Queries;

public class GetQrByLinkUniqueKeyQueryHandler(ILinkModuleService linkModuleService)
    : IRequestHandler<GetQrByLinkUniqueKeyQuery, DataResult<GetQrByLinkUniqueKeyQueryResult>>
{
    public async Task<DataResult<GetQrByLinkUniqueKeyQueryResult>> Handle(GetQrByLinkUniqueKeyQuery request,
        CancellationToken cancellationToken)
    {
        var linkResult = await linkModuleService.GetLinkByUniqueKeyAsync(request.UniqueKey, cancellationToken);
        if (linkResult.HasError) return DataResult<GetQrByLinkUniqueKeyQueryResult>.Failure(linkResult.Message);

        var generator = new QRCodeGenerator();
        var data = generator.CreateQrCode(linkResult.Data.Url, QRCodeGenerator.ECCLevel.Q);

        var qrCode = new PngByteQRCode(data);
        var qrBytes = qrCode.GetGraphic(20);

        var base64String = Convert.ToBase64String(qrBytes);
        var base64Image = $"data:image/png;base64,{base64String}";

        return DataResult<GetQrByLinkUniqueKeyQueryResult>.Success(new GetQrByLinkUniqueKeyQueryResult
        {
            Base64Image = base64Image
        });
    }
}
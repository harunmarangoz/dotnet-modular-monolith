using MediatR;
using QRCoder;
using QrModule.Application.QrQueries.Queries;
using Shared.Application.Results;
using Shared.Application.Services;
using Shared.Domain.Exceptions;

namespace QrModule.Infrastructure.QrRequests.Queries;

public class GetQrByLinkIdQueryHandler(ILinkModuleService linkModuleService)
    : IRequestHandler<GetQrByLinkIdQuery, DataResult<GetQrByLinkIdQueryResult>>
{
    public async Task<DataResult<GetQrByLinkIdQueryResult>> Handle(GetQrByLinkIdQuery request,
        CancellationToken cancellationToken)
    {
        var linkResult = await linkModuleService.GetLinkByIdAsync(request.LinkId, cancellationToken);
        if (linkResult.HasError) return DataResult<GetQrByLinkIdQueryResult>.Failure(linkResult.Message);

        var generator = new QRCodeGenerator();
        var data = generator.CreateQrCode(linkResult.Data.Url, QRCodeGenerator.ECCLevel.Q);

        var qrCode = new PngByteQRCode(data);
        var qrBytes = qrCode.GetGraphic(20);

        var base64String = Convert.ToBase64String(qrBytes);
        var base64Image = $"data:image/png;base64,{base64String}";

        return DataResult<GetQrByLinkIdQueryResult>.Success(new GetQrByLinkIdQueryResult
        {
            Base64Image = base64Image
        });
    }
}
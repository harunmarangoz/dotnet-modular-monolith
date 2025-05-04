using MediatR;
using QRCoder;
using QrModule.Application.QrQueries.Queries;
using Shared.Application.Services;

namespace QrModule.Infrastructure.QrRequests.Queries;

public class GetQrByLinkUniqueKeyQueryHandler(ILinkModuleService linkModuleService)
    : IRequestHandler<GetQrByLinkUniqueKeyQuery, GetQrByLinkUniqueKeyQueryResult>
{
    public async Task<GetQrByLinkUniqueKeyQueryResult> Handle(GetQrByLinkUniqueKeyQuery request, CancellationToken cancellationToken)
    {
        var redirectUrl = await linkModuleService.GetRedirectUrlFromUniqueKeyAsync(request.UniqueKey);

        var generator = new QRCodeGenerator();
        var data = generator.CreateQrCode(redirectUrl, QRCodeGenerator.ECCLevel.Q);

        var qrCode = new PngByteQRCode(data);
        var qrBytes = qrCode.GetGraphic(20);

        var base64String = Convert.ToBase64String(qrBytes);
        var base64Image = $"data:image/png;base64,{base64String}";

        return new GetQrByLinkUniqueKeyQueryResult
        {
            Base64Image = base64Image
        };
    }
}
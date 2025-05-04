using MediatR;

namespace QrModule.Application.QrQueries.Queries;

public record GetQrByLinkUniqueKeyQuery(string UniqueKey) : IRequest<GetQrByLinkUniqueKeyQueryResult>;

public class GetQrByLinkUniqueKeyQueryResult
{
    public string Base64Image { get; set; }
}
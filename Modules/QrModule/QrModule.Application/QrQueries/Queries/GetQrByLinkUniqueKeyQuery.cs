using MediatR;
using Shared.Application.Results;

namespace QrModule.Application.QrQueries.Queries;

public record GetQrByLinkUniqueKeyQuery(string UniqueKey) : IRequest<DataResult<GetQrByLinkUniqueKeyQueryResult>>;

public class GetQrByLinkUniqueKeyQueryResult
{
    public string Base64Image { get; set; }
}
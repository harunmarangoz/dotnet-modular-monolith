using MediatR;
using Shared.Application.Results;

namespace QrModule.Application.QrQueries.Queries;

public record GetQrByLinkIdQuery(Guid LinkId) : IRequest<DataResult<GetQrByLinkIdQueryResult>>;

public class GetQrByLinkIdQueryResult
{
    public string Base64Image { get; set; }
}
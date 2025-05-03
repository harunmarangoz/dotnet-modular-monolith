using MediatR;

namespace QrModule.Application.QrQueries.Queries;

public record GetQrByLinkIdQuery(Guid LinkId) : IRequest<GetQrByLinkIdQueryResult>;

public class GetQrByLinkIdQueryResult
{
    public string Base64Image { get; set; }
}
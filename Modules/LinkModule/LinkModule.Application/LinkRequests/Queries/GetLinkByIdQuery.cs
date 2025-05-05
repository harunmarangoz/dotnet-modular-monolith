using Shared.Application.DataTransferObjects;
using MediatR;
using Shared.Application.Results;

namespace LinkModule.Application.LinkRequests.Queries;

public record GetLinkByIdQuery(Guid Id) : IRequest<DataResult<LinkDto>>;
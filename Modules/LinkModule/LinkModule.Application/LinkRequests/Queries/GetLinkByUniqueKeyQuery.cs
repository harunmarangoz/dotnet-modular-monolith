using Shared.Application.DataTransferObjects;
using MediatR;
using Shared.Application.Results;

namespace LinkModule.Application.LinkRequests.Queries;

public record GetLinkByUniqueKeyQuery(string UniqueKey) : IRequest<DataResult<LinkDto>>;
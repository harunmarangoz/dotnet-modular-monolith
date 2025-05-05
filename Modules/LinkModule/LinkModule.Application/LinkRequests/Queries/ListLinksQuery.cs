using Shared.Application.DataTransferObjects;
using MediatR;
using Shared.Application.Results;

namespace LinkModule.Application.LinkRequests.Queries;

public record ListLinksQuery() : IRequest<ListDataResult<LinkDto>>;
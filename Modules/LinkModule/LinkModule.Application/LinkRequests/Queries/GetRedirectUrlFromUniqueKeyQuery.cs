using MediatR;
using Shared.Application.Results;

namespace LinkModule.Application.LinkRequests.Queries;

public record GetRedirectUrlFromUniqueKeyQuery(string UniqueKey) : IRequest<DataResult<string>>;
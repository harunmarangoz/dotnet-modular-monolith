using MediatR;

namespace LinkModule.Application.LinkRequests.Queries;

public record GetRedirectUrlFromUniqueKeyQuery(string UniqueKey) : IRequest<string>;
using MediatR;
using Shared.Application.Results;

namespace LinkModule.Application.LinkRequests.Commands;

public record CreateLinkCommand(CreateLinkCommandParameter Parameter) : IRequest<DataResult<Guid>>;

public class CreateLinkCommandParameter
{
    public string Name { get; set; }
    public string Url { get; set; }
}
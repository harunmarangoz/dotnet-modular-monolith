using MediatR;

namespace LinkModule.Application.LinkRequests.Commands;

public record CreateLinkCommand(CreateLinkCommandParameter Parameter) : IRequest<CreateLinkCommandResult>;

public class CreateLinkCommandParameter
{
    public string Name { get; set; }
    public string Url { get; set; }
}

public class CreateLinkCommandResult
{
    public Guid LinkId { get; set; }
}
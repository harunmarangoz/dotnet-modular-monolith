namespace Shared.Application.Services;

public interface IActorContext
{
    string ActorId { get; }
    IReadOnlyCollection<string> Roles { get; }
}
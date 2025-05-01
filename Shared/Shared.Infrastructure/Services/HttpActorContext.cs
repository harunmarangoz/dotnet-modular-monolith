using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Shared.Application.Services;

namespace Shared.Infrastructure.Services;

public class HttpActorContext(IHttpContextAccessor httpContextAccessor) : IActorContext
{
    public long Id =>
        long.Parse(httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

    public string ActorId => httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

    public IReadOnlyCollection<string> Roles => httpContextAccessor.HttpContext.User.Claims
        .Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value)
        .ToList();
}

public class SystemActorContext : IActorContext
{
    public string ActorId => "SYSTEM";
    public IReadOnlyCollection<string> Roles => new[] { "Admin", "System" };
}

public class ActorContextHolder : IActorContext
{
    private static readonly AsyncLocal<IActorContext> _current = new();

    public static void Set(IActorContext ctx) => _current.Value = ctx;
    private static IActorContext Current => _current.Value;

    public string ActorId => Current?.ActorId;
    public IReadOnlyCollection<string> Roles => Current?.Roles;
}
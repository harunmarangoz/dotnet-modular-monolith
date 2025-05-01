using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Services;

namespace Shared.Api.Middlewares;

public class ActorContextMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var httpCtx = context.RequestServices.GetRequiredService<HttpActorContext>();
        ActorContextHolder.Set(httpCtx);
        await next(context);
    }
}
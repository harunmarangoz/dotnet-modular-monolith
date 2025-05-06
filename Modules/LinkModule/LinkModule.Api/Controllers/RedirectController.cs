using LinkModule.Application.LinkRequests.Queries;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Messages;

namespace LinkModule.Api.Controllers;

public class RedirectController(ISender sender, IBus bus) : Controller
{
    public async Task<ActionResult> Index()
    {
        var uniqueKey = HttpContext.Request.Path.Value.TrimStart('/');
        if (string.IsNullOrEmpty(uniqueKey)) return NotFound();

        var linkResult = await sender.Send(new GetLinkByUniqueKeyQuery(uniqueKey));
        if (linkResult.HasError) return BadRequest(linkResult);

        await bus.Publish(new CreateClickEventMessage
        {
            LinkId = linkResult.Data.Id,
            LinkUniqueKey = linkResult.Data.UniqueKey,
            UserAgent = HttpContext.Request.Headers["User-Agent"].ToString(),
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
        });

        return RedirectPermanent(linkResult.Data.Url);
    }
}
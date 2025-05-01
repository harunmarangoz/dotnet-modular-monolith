using LinkModule.Application.LinkRequests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkModule.Api.Controllers;

public class RedirectController(ISender sender) : Controller
{
    public async Task<ActionResult> Index()
    {
        var uniqueKey = HttpContext.Request.Path.Value.TrimStart('/');
        if (string.IsNullOrEmpty(uniqueKey)) return NotFound();

        var redirect = await sender.Send(new GetRedirectUrlFromUniqueKeyQuery(uniqueKey));
        if (string.IsNullOrEmpty(redirect)) return NotFound();

        return RedirectPermanent(redirect);
    }
}
using AnalyticModule.Application.ClickEventRequests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Controller;

namespace AnalyticModule.Api.Controllers;

[Route("api/[controller]")]
public class AnalyticController(ISender sender) : BaseApiController
{
    [HttpGet("{linkId:guid}")]
    public async Task<ActionResult> GetLinkAnalytics([FromRoute] Guid linkId)
    {
        var result = await sender.Send(new ReportClickEventCommand(linkId));
        return Ok(result);
    }
}
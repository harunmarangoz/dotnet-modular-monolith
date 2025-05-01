using LinkModule.Application.LinkRequests.Commands;
using LinkModule.Application.LinkRequests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkModule.Api.Controllers;

[Route("api/[controller]")]
public class LinkController(ISender sender) : Controller
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateLinkCommandParameter parameter)
    {
        var createResult = await sender.Send(new CreateLinkCommand(parameter));
        return Ok(createResult);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> RedirectUrl([FromRoute] Guid id)
    {
        var linkResult = await sender.Send(new GetLinkByIdQuery(id));
        return Ok(linkResult);
    }
}
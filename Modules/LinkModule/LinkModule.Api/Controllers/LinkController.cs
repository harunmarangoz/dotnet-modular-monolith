using LinkModule.Application.LinkRequests.Commands;
using LinkModule.Application.LinkRequests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Controller;

namespace LinkModule.Api.Controllers;

[Route("api/[controller]")]
public class LinkController(ISender sender) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateLinkCommandParameter parameter)
    {
        var createResult = await sender.Send(new CreateLinkCommand(parameter));
        return Ok(createResult);
    }

    [HttpGet]
    public async Task<ActionResult> List()
    {
        var listResult = await sender.Send(new ListLinksQuery());
        return Ok(listResult);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> RedirectUrl([FromRoute] Guid id)
    {
        var linkResult = await sender.Send(new GetLinkByIdQuery(id));
        return Ok(linkResult);
    }
}
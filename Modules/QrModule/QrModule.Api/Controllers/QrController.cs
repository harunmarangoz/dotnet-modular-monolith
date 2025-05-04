using MediatR;
using Microsoft.AspNetCore.Mvc;
using QrModule.Application.QrQueries.Queries;

namespace QrModule.Api.Controllers;

[Route("api/[controller]")]
public class QrController(ISender sender) : Controller
{
    [HttpGet("{uniqueKey}")]
    public async Task<IActionResult> GetQrByLinkUniqueKey(string uniqueKey)
    {
        var result = await sender.Send(new GetQrByLinkUniqueKeyQuery(uniqueKey));
        return Ok(result);
    }
}
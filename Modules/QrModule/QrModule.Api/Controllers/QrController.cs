using MediatR;
using Microsoft.AspNetCore.Mvc;
using QrModule.Application.QrQueries.Queries;

namespace QrModule.Api.Controllers;

[Route("api/[controller]")]
public class QrController(ISender sender) : Controller
{
    [HttpGet("get-qr-by-link-id/{linkId}")]
    public async Task<IActionResult> GetQrByLinkId(Guid linkId)
    {
        var result = await sender.Send(new GetQrByLinkIdQuery(linkId));
        return Ok(result);
    }
}
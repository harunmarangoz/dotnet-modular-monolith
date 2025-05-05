using Microsoft.AspNetCore.Mvc;
using Shared.Application.Results;

namespace Shared.Api.Controller;

public class BaseApiController : Microsoft.AspNetCore.Mvc.Controller
{
    protected ActionResult Ok(Result result)
    {
        if (result.HasError) return BadRequest(result);
        return base.Ok(result);
    }

    protected ActionResult Ok<T>(DataResult<T> result)
    {
        if (result.HasError) return BadRequest(result);
        return base.Ok(result);
    }

    protected ActionResult Ok<T>(ListDataResult<T> result)
    {
        if (result.HasError) return BadRequest(result);
        return base.Ok(result);
    }
}
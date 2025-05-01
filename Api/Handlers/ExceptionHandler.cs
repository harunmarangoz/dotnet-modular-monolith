using Microsoft.AspNetCore.Diagnostics;
using Shared.Domain.Exceptions;

namespace Api.Handlers;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var response = new Response
        {
            Message = GetMessage(exception)
        };
        if (exception is AppValidationException validationException)
            response.Errors = validationException.Errors;

        httpContext.Response.StatusCode = GetStatusCode(exception);
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }

    private static string GetMessage(Exception exception)
    {
        if (exception is AppException appException)
            return appException.Message;
        return "Internal Server Error";
    }

    private static int GetStatusCode(Exception exception)
    {
        if (exception is not AppException) return StatusCodes.Status500InternalServerError;
        return exception switch
        {
            AppValidationException => StatusCodes.Status422UnprocessableEntity,
            AppNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest
        };
    }
}

file class Response
{
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; }
}
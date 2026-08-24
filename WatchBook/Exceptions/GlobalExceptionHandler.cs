using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WatchBook.Exceptions;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Unhandled exception occurred.");

        var statusCode = exception switch
        {
            HttpRequestException httpRequestException
                when httpRequestException.StatusCode is not null
                => (int)httpRequestException.StatusCode,

            ArgumentException => StatusCodes.Status400BadRequest,

            _ => StatusCodes.Status500InternalServerError
        };

        httpContext.Response.StatusCode = statusCode;

        return await problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = GetTitle(statusCode),
                    Detail = GetDetail(statusCode)
                }
            });
    }

    private static string GetTitle(int statusCode) =>
        statusCode switch
        {
            StatusCodes.Status400BadRequest => "Bad Request",
            StatusCodes.Status404NotFound => "Resource Not Found",
            StatusCodes.Status502BadGateway => "External Service Error",
            _ => "An unexpected error occurred."
        };

    private static string GetDetail(int statusCode) =>
        statusCode switch
        {
            StatusCodes.Status400BadRequest =>
                "The request was invalid.",

            StatusCodes.Status404NotFound =>
                "The requested resource could not be found.",

            StatusCodes.Status502BadGateway =>
                "The external service could not be reached successfully.",

            _ =>
                "An unexpected error occurred while processing the request."
        };
}
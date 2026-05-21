using System.Net;
using FluentValidation;
using TaskFlow.Application.Common;

namespace TaskFlow.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception");

        int statusCode;
        string message;

        if (exception is ValidationException validationEx)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            message = string.Join("; ", validationEx.Errors.Select(e => e.ErrorMessage));
        }
        else if (exception is UnauthorizedAccessException)
        {
            statusCode = (int)HttpStatusCode.Unauthorized;
            message = "Unauthorized";
        }
        else if (exception is KeyNotFoundException)
        {
            statusCode = (int)HttpStatusCode.NotFound;
            message = "Not found";
        }
        else
        {
            statusCode = (int)HttpStatusCode.InternalServerError;
            message = "Something went wrong";
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var result = Result.Failure(message, statusCode);
        await context.Response.WriteAsJsonAsync(result);
    }
}

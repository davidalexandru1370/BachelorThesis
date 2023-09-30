using System.Text;
using System.Text.Json;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ApplicationException = Domain.Exceptions.ApplicationException;

namespace SDIA.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApplicationException applicationException)
        {
            Log.Error("The following exception was thrown: {Exception}", applicationException);
            var problemDetails = GetProblemDetailsFromException(applicationException, context);
            await WriteProblemDetailsToResponse(problemDetails, context);
        }
        catch (Exception exception)
        {
            Log.Error("The following exception was thrown: {Exception}", exception);
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Internal Server Error"
            };

            await WriteProblemDetailsToResponse(problemDetails, context);
        }
    }

    private async Task WriteProblemDetailsToResponse(ProblemDetails problemDetails, HttpContext context)
    {
        context.Response.StatusCode = problemDetails.Status.Value;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }

    private ProblemDetails GetProblemDetailsFromException(ApplicationException exception, HttpContext context)
    {
        ProblemDetails problemDetails = new ProblemDetails()
        {
            Instance = context.Request.Path
        };

        switch (exception)
        {
            case NotFoundException notFoundException:
                problemDetails.Title = "Not found!";
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
                problemDetails.Detail = notFoundException.Message;
                break;
            case BadRequestException badRequestException:
                problemDetails.Title = "Bad request!";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                problemDetails.Detail = badRequestException.Message;
                break;
            case DuplicateEntryException duplicateEntryException:
                problemDetails.Title = "Conflict!";
                problemDetails.Status = StatusCodes.Status409Conflict;
                problemDetails.Detail = duplicateEntryException.Message;
                break;
        }

        return problemDetails;
    }
}
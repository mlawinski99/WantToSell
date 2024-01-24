using System.Net;
using Newtonsoft.Json;
using WantToSell.Api.Models;
using WantToSell.Application.Exceptions;

namespace WantToSell.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var response = httpContext.Response;
        var customProblemDetails = new CustomProblemDetails();
        response.ContentType = "application/json";

        switch (exception)
        {
            case BadRequestException ex:
                customProblemDetails = new CustomProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = ex.Message,
                    Type = nameof(BadRequestException),
                    Errors = ex.ValidationErrors
                };
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case NotFoundException ex:
                customProblemDetails = new CustomProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Title = ex.Message,
                    Type = nameof(NotFoundException)
                };
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case AccessDeniedException ex:
                customProblemDetails = new CustomProblemDetails
                {
                    Status = (int)HttpStatusCode.Forbidden,
                    Title = ex.Message,
                    Type = nameof(AccessDeniedException)
                };
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                break;
            default:
                customProblemDetails = new CustomProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Something went wrong! Contact administrator.",
                    Type = nameof(HttpStatusCode.InternalServerError)
                };
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var result = JsonConvert.SerializeObject(customProblemDetails);
        _logger.LogError(result);
        await response.WriteAsJsonAsync(customProblemDetails);
    }
}
using System.Net;
using Azure;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using WantToSell.Application.Exceptions;

namespace WantToSell.Api.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

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
			response.ContentType = "application/json";

			switch(exception)
			{
				case BadRequestException ex:
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					break;
				case NotFoundException ex:
					response.StatusCode = (int)HttpStatusCode.NotFound;
					break;
				default:
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;
			}
			
			var result = JsonSerializer.Serialize(new { message = exception?.Message, stackTrace = exception?.StackTrace });
			_logger.LogError(exception, result);
			await response.WriteAsync(result);
		}
	}
}

using System.Net;
using Newtonsoft.Json;
using WantToSell.Api.Models;
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
			var customProblemDetails = new CustomProblemDetails();
			response.ContentType = "application/json";

			switch(exception)
			{
				case BadRequestException ex:
					customProblemDetails = new CustomProblemDetails
					{
						Status = (int)HttpStatusCode.BadRequest,
						Title = ex.Message,
						Type = nameof(BadRequestException),
						Errors = ex.ValidationErrors
					};
					break;
				case NotFoundException ex:
					customProblemDetails = new CustomProblemDetails()
					{
						Status = (int)HttpStatusCode.NotFound,
						Title = ex.Message,
						Type = nameof(NotFoundException),
					};
					break;
				default:
					customProblemDetails = new CustomProblemDetails()
					{
						Status = (int)HttpStatusCode.InternalServerError,
						Title = exception.Message,
						Type = nameof(HttpStatusCode.InternalServerError),
					};
					break;
			}
			
			var result = JsonConvert.SerializeObject(customProblemDetails);
			_logger.LogError(result);
			await response.WriteAsJsonAsync(customProblemDetails);
		}
	}
}

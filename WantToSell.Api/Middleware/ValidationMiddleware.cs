using FluentValidation;
using Microsoft.AspNetCore.Mvc.Controllers;
using WantToSell.Application.Exceptions;

namespace WantToSell.Api.Middleware;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public ValidationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Method == "POST" || context.Request.Method == "PUT")
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>() is ControllerActionDescriptor controllerActionDescriptor)
            {
                var modelType = controllerActionDescriptor.Parameters
                    .FirstOrDefault(parameter => parameter.ParameterType.IsClass)?.ParameterType;

                if (modelType != null)
                {
                    var validatorType = typeof(IValidator<>).MakeGenericType(modelType);
                    var validator = _serviceProvider.GetService(validatorType);

                    if (validator != null)
                    {
                        using (var reader = new StreamReader(context.Request.Body))
                        {
                            var requestBody = await reader.ReadToEndAsync();
                            var instance = Newtonsoft.Json.JsonConvert.DeserializeObject(requestBody, modelType);

                            var validateAsyncMethod = validator.GetType().GetMethod("ValidateAsync", new[] { modelType, typeof(CancellationToken) });
                            var validationResultTask = (Task)validateAsyncMethod.Invoke(validator, new object[] { instance, CancellationToken.None });

                            await validationResultTask;

                            var validationResultType = validationResultTask.GetType().GetProperty("Result").PropertyType;
                            var isValidProperty = validationResultType.GetProperty("IsValid");
                            var validationResult = validationResultTask.GetType().GetProperty("Result").GetValue(validationResultTask);
                            var isValid = (bool)isValidProperty.GetValue(validationResult);
                            
                            if (!isValid)
                            {
                                var errorsProperty = validationResultType.GetProperty("Errors");
                                var errors = (IEnumerable<object>)errorsProperty.GetValue(validationResult);
                                var validationErrors = errors.ToDictionary(
                                    e => e.GetType().GetProperty("PropertyName")?.GetValue(e)?.ToString(),
                                    e => new[] { e.GetType().GetProperty("ErrorMessage")?.GetValue(e)?.ToString() }
                                );

                                var exceptionMessage = "Invalid request!";
                                throw new BadRequestException(exceptionMessage)
                                {
                                    ValidationErrors = validationErrors
                                };
                            }
                        }
                    }
                }
            }
        }

        await _next(context);
    }
}
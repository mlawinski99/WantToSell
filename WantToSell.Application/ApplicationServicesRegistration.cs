using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using WantToSell.Application.Services;

namespace WantToSell.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServicesCollection(
        this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IItemImagesService, ItemImagesService>();
        
        RegisterValidators(services);
        RegisterMappers(services);

        services.AddFluentValidationAutoValidation();
        return services;
    }

    private static void RegisterValidators(IServiceCollection services)
    {
        var validatorTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type =>
                type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
            .ToList();

        foreach (var validatorType in validatorTypes)
        {
            var interfaceType = validatorType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>));
            services.AddTransient(interfaceType, validatorType);
        }
    }

    private static void RegisterMappers(IServiceCollection services)
    {
            var applicationAssembly = Assembly.GetExecutingAssembly();

            var mapperTypes = applicationAssembly.GetTypes().Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                t.Name.EndsWith("Mapper")
            ).ToList();

            foreach (var type in mapperTypes)
            {
                services.AddTransient(type);
            }
    }
}
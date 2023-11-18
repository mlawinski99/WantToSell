using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WantToSell.Application
{
	public static class ApplicationServicesRegistration
	{
		public static IServiceCollection AddApplicationServicesCollection(
			this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			return services;
		}
	}
}

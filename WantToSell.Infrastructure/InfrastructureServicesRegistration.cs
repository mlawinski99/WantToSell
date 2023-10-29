using Microsoft.Extensions.DependencyInjection;
using WantToSell.Application.Contracts.Logs;
using WantToSell.Infrastructure.Logs;

namespace WantToSell.Infrastructure
{
	public static class InfrastructureServicesRegistration
	{
		public static IServiceCollection AddInfrastructureServicesCollection(
			this IServiceCollection services)
		{
			services.AddScoped(typeof(IApplicationLogger<>), typeof(ApplicationLogger<>));

			return services;
		}
	}
}
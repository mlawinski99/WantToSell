using Microsoft.Extensions.DependencyInjection;
using WantToSell.Application.Contracts.Logs;
using WantToSell.Infrastructure.Logs;

namespace WantToSell.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrastructureServiceCollection(
			this IServiceCollection services)
		{
			services.AddScoped(typeof(IApplicationLogger<>), typeof(ApplicationLogger<>));

			return services;
		}
	}
}
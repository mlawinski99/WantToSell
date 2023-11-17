using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Persistence.DbContext;
using WantToSell.Persistence.Repositories;

namespace WantToSell.Persistence
{
	public static class PersistenceServicesRegistration
	{
			public static IServiceCollection AddPersistenceServicesCollection(
				this IServiceCollection services, IConfiguration configuration)
			{
				services.AddDbContext<WantToSellContext>(options => 
					options.UseSqlServer(configuration.GetConnectionString("WantToSellDbConnectionString")));

				services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
				services.AddScoped<ICategoryRepository, CategoryRepository>();
				services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
				services.AddScoped<IAddressRepository, AddressRepository>();
				services.AddScoped<IItemRepository, ItemRepository>();

				return services;
			}
	}
}
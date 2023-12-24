using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WantToSell.Application.Contracts.Persistence;
using WantToSell.Persistence.DbContexts;
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

			//	services.AddDatabaseMigrationServices();
				
				services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
				services.AddScoped<ICategoryRepository, CategoryRepository>();
				services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
				services.AddScoped<IAddressRepository, AddressRepository>();
				services.AddScoped<IItemRepository, ItemRepository>();

				return services;
			}
			
			private static void AddDatabaseMigrationServices(this IServiceCollection services)
			{
				using var serviceProvider = services.BuildServiceProvider();
				using var scope = serviceProvider.CreateScope();

				var dbContext = scope.ServiceProvider.GetRequiredService<WantToSellContext>();

				ApplyMigrations(dbContext);
			}

			private static void ApplyMigrations(WantToSellContext dbContext)
			{
				dbContext.Database.EnsureCreated();

				dbContext.Database.Migrate();
			}
	}
}
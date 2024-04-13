using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WantToSell.Application.Contracts.Cache;
using WantToSell.Cache.Helpers;

namespace WantToSell.Cache;

public static class CacheHelpersRegistration
{
    public static IServiceCollection AddCacheHelpersCollection(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnectionString");
        });
        services.AddScoped<ICacheHelper, RedisCacheHelper>();
        
        return services;
    }
}
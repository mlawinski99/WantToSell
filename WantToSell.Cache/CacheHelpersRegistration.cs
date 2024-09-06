using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using WantToSell.Application.Contracts.Cache;
using WantToSell.Cache.Helpers;

namespace WantToSell.Cache;

public static class CacheHelpersRegistration
{
    public static IServiceCollection AddCacheHelpersCollection(this IServiceCollection services,
        IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("RedisConnectionString");

        var redisConfiguration = ConfigurationOptions.Parse(redisConnectionString);
        redisConfiguration.SyncTimeout = 20000;
        redisConfiguration.ConnectTimeout = 20000;

        services.AddStackExchangeRedisCache(options => { options.ConfigurationOptions = redisConfiguration; });
        services.AddScoped<ICacheHelper, RedisCacheHelper>();

        return services;
    }
}
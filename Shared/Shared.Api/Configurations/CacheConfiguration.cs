using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Settings;

namespace Shared.Api.Configurations;

public static class CacheConfiguration
{
    public static IServiceCollection AddCacheConfiguration(this IServiceCollection services,
        CacheSettings cacheSettings)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = cacheSettings.ConnectionString;
            options.InstanceName = cacheSettings.InstanceName;
        });

        return services;
    }
}
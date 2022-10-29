using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;
using StackExchange.Redis;

namespace Poq.ProductService.Api.Caching;

public static class OutputCacheExtensions
{
    /// <summary>
    ///     Add output caching services using Redis.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> for adding services.</param>
    /// <returns></returns>
    public static IServiceCollection AddRedisOutputCache(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" },
                Ssl = false
            }));

        services.TryAddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
        services.TryAddSingleton<IOutputCacheStore, RedisOutputCacheStore>();

        return services;
    }
}

using Microsoft.AspNetCore.OutputCaching;
using StackExchange.Redis;

namespace Poq.ProductService.Api.Caching;

public sealed class RedisOutputCacheStore : IOutputCacheStore
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisOutputCacheStore(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    /// <inheritdoc />
    public async ValueTask EvictByTagAsync(string tag, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(tag);

        var db = _connectionMultiplexer.GetDatabase();
        var cachedKeys = await db.SetMembersAsync(tag);

        var keys = cachedKeys
            .Select(x => new RedisKey(x))
            .Concat(new[] { new RedisKey(tag) })
            .ToArray();

        await db.KeyDeleteAsync(keys);
    }

    /// <inheritdoc />
    public async ValueTask<byte[]?> GetAsync(string key, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(key);

        var db = _connectionMultiplexer.GetDatabase();
        return await db.StringGetAsync(key);
    }

    /// <inheritdoc />
    public async ValueTask SetAsync(
        string key,
        byte[] value,
        string[]? tags,
        TimeSpan validFor,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(value);

        var db = _connectionMultiplexer.GetDatabase();
        foreach (var tag in tags ?? Array.Empty<string>())
        {
            await db.SetAddAsync(tag, key);
        }

        await db.StringSetAsync(key, value, validFor);
    }
}

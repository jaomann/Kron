using System.Text.Json;
using KronApi.Core.Contracts.Service;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace KronApi.Infrastructure.Cache;

public class RedisService : IRedisService
{
    private readonly IDistributedCache _cache;
    private readonly RedisConfiguration _config;
    
    public RedisService(IOptions<RedisConfiguration> config, IDistributedCache cache)
    {
        _cache = cache;
        _config = config.Value;
    }

    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        var value = await _cache.GetStringAsync(key);
        return value == null ? null : JsonSerializer.Deserialize<T>(value);
    }

    public async Task SetAsync<T>(string key, T value, int? expirationMinutes = null) where T : class
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(
                expirationMinutes ?? _config.DefaultExpirationMinutes)
        };

        await _cache.SetStringAsync(
            key,
            JsonSerializer.Serialize(value),
            options);
    }

    public async Task SetTokenAsync(string key, string token)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(_config.TokenExpirationHours)
        };

        await _cache.SetStringAsync(key, token, options);
    }

    public async Task<string?> GetTokenAsync(string key)
    {
        return await _cache.GetStringAsync(key);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
} 
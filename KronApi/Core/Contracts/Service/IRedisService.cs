namespace KronApi.Core.Contracts.Service;

public interface IRedisService
{
    Task<T?> GetAsync<T>(string key) where T : class;
    Task SetAsync<T>(string key, T value, int? expirationMinutes = null) where T : class;
    Task SetTokenAsync(string key, string token);
    Task<string?> GetTokenAsync(string key);
    Task RemoveAsync(string key);
} 
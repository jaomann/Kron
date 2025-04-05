namespace KronApi.Infrastructure.Cache;

public class RedisConfiguration
{
    public string ConnectionString { get; set; } = string.Empty;
    public int DefaultExpirationMinutes { get; set; } = 30;
    public int TokenExpirationHours { get; set; } = 24;
} 
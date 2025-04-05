using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using KronApi.Infrastructure.Cache;
using KronApi.Infrastructure.Email;
using KronApi.Repository;
using KronApi.Services;

namespace KronApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Infrastructure Services
        services.AddScoped<IEmailService, EmailService>();
        
        // Auth Services
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        
        // Domain Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IAddressService, AddressService>();
        
        // Scheduling Services
        services.AddScoped<IWeekService, WeekService>();
        services.AddScoped<IDayService, DayService>();
        
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IWeekRepository, WeekRepository>();
        services.AddScoped<IDayRepository, DayRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Redis
        var redisConfig = configuration.GetSection("Redis").Get<RedisConfiguration>();
        services.Configure<RedisConfiguration>(configuration.GetSection("Redis"));
        
        // Add Redis Cache
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfig?.ConnectionString ?? "localhost:6379";
            options.InstanceName = "KronApi_";
        });
        
        // Add Redis Service
        services.AddSingleton<IRedisService, RedisService>();
        
        return services;
    }
} 
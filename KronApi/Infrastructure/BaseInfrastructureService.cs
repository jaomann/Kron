using Microsoft.Extensions.Configuration;

namespace KronApi.Infrastructure;

public abstract class BaseInfrastructureService
{
    protected readonly IConfiguration Configuration;
    
    protected BaseInfrastructureService(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    protected string GetConnectionString(string name)
    {
        return Configuration.GetConnectionString(name) 
               ?? throw new InvalidOperationException($"Connection string '{name}' not found.");
    }
    
    protected T GetSection<T>(string sectionName) where T : class, new()
    {
        var section = Configuration.GetSection(sectionName);
        var config = new T();
        section.Bind(config);
        return config;
    }
} 
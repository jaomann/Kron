using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using Microsoft.Extensions.Configuration;

namespace KronApi.Services;

public abstract class BaseService<T> : IBaseService<T> where T : class
{
    protected readonly IConfiguration Configuration;
    protected readonly IBaseRepository<T> Repository;

    protected BaseService(IConfiguration configuration, IBaseRepository<T> repository)
    {
        Configuration = configuration;
        Repository = repository;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id) => await Repository.GetByIdAsync(id);

    public virtual async Task<List<T>?> GetAllAsync()
    {
        var result = await Repository.GetAllAsync();
        return result?.ToList();
    }

    public virtual async Task Create(T entity)
    {
        await Repository.CreateAsync(entity);
    }

    public virtual async Task Update(T entity)
    {
        await Repository.UpdateAsync(entity);
    }

    public virtual async Task Delete(Guid id)
    {
        await Repository.DeleteAsync(id);
    }

    protected string GetConnectionString(string name)
    {
        return Configuration.GetConnectionString(name) 
               ?? throw new InvalidOperationException($"Connection string '{name}' not found.");
    }
} 
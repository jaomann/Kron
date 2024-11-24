namespace KronApi.Core.Contracts.Service;

public interface IBaseService<T>
{
    Task Delete(Guid id);
    Task Update(T entity);
    Task Create(T entity);
    Task<List<T>?> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
}
namespace KronApi.Core.Contracts.Repository;

public interface IBaseRepository<T>
{ 
    Task DeleteAsync(Guid id);
    Task UpdateAsync(T entity);
    Task CreateAsync(T entity);
    Task<List<T>?> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
}
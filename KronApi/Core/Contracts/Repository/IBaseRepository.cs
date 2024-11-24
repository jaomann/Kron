namespace KronApi.Core.Contracts.Repository;

public interface IBaseRepository<T>
{ 
    Task Delete(Guid id);
    Task Update(T entity);
    Task Create(T entity);
    Task<List<T>?> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
}
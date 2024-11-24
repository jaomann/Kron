using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;

namespace KronApi.Services;

public class BaseService<T> : IBaseService<T> where T : class
{
    private readonly IBaseRepository<T> _repository;

    public BaseService(IBaseRepository<T> repository)
    {
        _repository = repository;
    }
    
    public async Task Delete(Guid id) =>  await _repository.Delete(id);

    public async Task Update(T entity) => await _repository.Update(entity);

    public async Task Create(T entity) => await _repository.Create(entity);

    public async Task<List<T>?> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<T?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
}
using KronApi.Core.Contracts.Repository;
using KronApi.Core.Entities;
using KronApi.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace KronApi.Repository;
public abstract class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
{
    private readonly Context _context;
    protected BaseRepository(Context context)
    {
        _context = context;
    }
    public async Task Delete(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return;
        entity.IsDeleted = true;
        await Update(entity);
    }
    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Create(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<T?> GetByIdAsync(Guid id) => await _context.Set<T>().FindAsync(id);
    public async Task<List<T>?> GetAllAsync() => await _context.Set<T>().ToListAsync();
}
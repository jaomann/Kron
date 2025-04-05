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
    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return;
        entity.isDeleted = true;
        await UpdateAsync(entity);
    }
    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task CreateAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<T?> GetByIdAsync(Guid id) => await _context.Set<T>().FirstOrDefaultAsync(e => e.id == id);
    public async Task<List<T>?> GetAllAsync() => await _context.Set<T>().ToListAsync();
}
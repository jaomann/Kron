using KronApi.Core.Contracts.Repository;
using KronApi.Core.Entities;
using KronApi.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace KronApi.Repository;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    private readonly Context _context;
    public CompanyRepository(Context context) : base(context)
    {
        _context = context;
    }
    
    public async Task<bool> IsExistAsync(Guid id) => await _context.Set<Company>().AsNoTracking()
            .AnyAsync(c => c.id == id && !c.isDeleted);
    public async Task<bool> IsExistByCnpjAsync(string cnpj) => await _context.Set<Company>().AsNoTracking()
            .AnyAsync(c => c.CNPJ == cnpj && !c.isDeleted);
    public async Task<Company?> GetByIdAsync(Guid Id) => await _context.Set<Company>().AsNoTracking()
        .Include(c => c.Users)
        .Include(c => c.Address)
        .FirstOrDefaultAsync(c => c.id == Id);
    public async Task<Company?> GetByCnpjAsync(string cnpj) => await _context.Set<Company>().AsNoTracking()
        .Include(c => c.Users)
        .Include(c => c.Address)
        .FirstOrDefaultAsync(c => c.CNPJ == cnpj);
}
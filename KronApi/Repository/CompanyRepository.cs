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
    
    public async Task<bool> IsExistAsync(Guid id) => await _context.Set<Company>().AnyAsync(c => c.Id == id && !c.IsDeleted);
    public async Task<bool> IsExistByCnpjAsync(string cnpj) => await _context.Set<Company>().AnyAsync(c => c.CNPJ == cnpj && !c.IsDeleted);
    public async Task<Company> GetByCnpjAsync(string cnpj) => await _context.Set<Company>().FirstOrDefaultAsync(c => c.CNPJ == cnpj);
}
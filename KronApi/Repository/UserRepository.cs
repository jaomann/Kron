using KronApi.Core.Contracts.Repository;
using KronApi.Core.Entities;
using KronApi.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace KronApi.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly Context _context;
    public UserRepository(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsExistAsync(string email, string cpf)
    {
        return await _context.User.AnyAsync(x => x.Email == email && x.Cpf == cpf);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
    }
}
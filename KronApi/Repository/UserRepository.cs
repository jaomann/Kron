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

    public async Task<bool> IsExistAsync(string email, string cpf) => await _context.Set<User>().AnyAsync(u => u.Email == email && u.Cpf == cpf && !u.IsDeleted);
}
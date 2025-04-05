using KronApi.Core.Contracts.Repository;
using KronApi.Core.Entities;
using KronApi.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace KronApi.Repository
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        private readonly Context _context;
        public AddressRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Address> GetByCompanyIdAsync(Guid id) => await _context.Set<Address>().FirstOrDefaultAsync(x => x.CompanyId == id);
    }
}

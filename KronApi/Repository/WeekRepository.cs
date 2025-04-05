using KronApi.Core.Contracts.Repository;
using KronApi.Core.Entities;
using KronApi.Repository.Database;
using Microsoft.EntityFrameworkCore;

namespace KronApi.Repository
{
    public class WeekRepository : BaseRepository<Week>, IWeekRepository
    {
        private readonly Context _context;
        public WeekRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Week> GetByCompanyIdAsync(Guid id) => await _context.Set<Week>().FirstOrDefaultAsync(x => x.CompanyId == id);

    }
}

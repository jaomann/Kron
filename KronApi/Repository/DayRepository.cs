using KronApi.Core.Contracts.Repository;
using KronApi.Core.Entities;
using KronApi.Repository.Database;

namespace KronApi.Repository
{
    public class DayRepository : BaseRepository<Day>, IDayRepository
    {
        private readonly Context _context;
        public DayRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task AddDays(List<Day> days)
        {
            await _context.Set<Day>().AddRangeAsync(days);
        }
    }
}

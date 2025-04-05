using KronApi.Core.Entities;

namespace KronApi.Core.Contracts.Repository
{
    public interface IDayRepository : IBaseRepository<Day>
    {
        public Task AddDays(List<Day> days);
    }
}

using KronApi.Core.Entities;

namespace KronApi.Core.Contracts.Service
{
    public interface IDayService : IBaseService<Day>
    {
        public Task AddDays(List<Day> days);
    }
}

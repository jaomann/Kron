using KronApi.Core.Entities;

namespace KronApi.Core.Contracts.Repository;

public interface IWeekRepository : IBaseRepository<Week>
{
    Task<Week> GetByCompanyIdAsync(Guid id);
}
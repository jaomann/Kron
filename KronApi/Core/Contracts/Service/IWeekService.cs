using KronApi.Core.Entities;
using KronApi.Models.WeekDTO;

namespace KronApi.Core.Contracts.Service;

public interface IWeekService : IBaseService<Week>
{
    Task<Week> GetByCompanyIdAsync(Guid id);
    GetWeekDTO MapDTO(Week week);
}
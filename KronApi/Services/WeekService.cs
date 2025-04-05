using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using KronApi.Core.Entities;
using KronApi.Models.WeekDTO;

namespace KronApi.Services;

public class WeekService : BaseService<Week>, IWeekService
{
    private readonly IWeekRepository _weekRepository;

    public WeekService(IConfiguration configuration, IWeekRepository weekRepository)
        : base(configuration, weekRepository)
    {
        _weekRepository = weekRepository;
    }

    public async Task<Week> GetByCompanyIdAsync(Guid companyId)
    {
        return await _weekRepository.GetByCompanyIdAsync(companyId);
    }

    public GetWeekDTO MapDTO(Week week)
    {
        if (week == null) return new GetWeekDTO();

        return new GetWeekDTO
        {
            Id = week.id,
            CompanyId = week.CompanyId,
            Active = week.Active,
            TotalHours = week.TotalHours,
            Days = week.Days
        };
    }
}

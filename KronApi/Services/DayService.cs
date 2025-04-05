using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using KronApi.Core.Entities;

namespace KronApi.Services;

public class DayService : BaseService<Day>, IDayService
{
    private readonly IDayRepository _dayRepository;

    public DayService(IConfiguration configuration, IDayRepository dayRepository) 
        : base(configuration, dayRepository)
    {
        _dayRepository = dayRepository;
    }

    public async Task AddDays(List<Day>? days)
    {
        if (days == null || !days.Any()) return;
        
        foreach (var day in days)
        {
            day.id = Guid.NewGuid();
            await Create(day);
        }
    }
} 
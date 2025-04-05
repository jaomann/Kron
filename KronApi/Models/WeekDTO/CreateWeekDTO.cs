using KronApi.Core.Entities;

namespace KronApi.Models.WeekDTO;

public class CreateWeekDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
    public List<Day> Days { get; set; } = new();
} 
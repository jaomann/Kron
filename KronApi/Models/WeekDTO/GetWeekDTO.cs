using KronApi.Core.Entities;

namespace KronApi.Models.WeekDTO;

public class GetWeekDTO : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; }
    public decimal TotalHours { get; set; }
    public List<Day> Days { get; set; } = new();
    public Guid CompanyId { get; set; }
}

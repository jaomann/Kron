using KronApi.Models;

namespace KronApi.Core.Entities;

public class Day : EntityBase
{
    public Day(Guid weekId)
    {
        id = Guid.NewGuid();
        WeekId = weekId;
        Hours = 0;
    }
    public decimal Hours { get; set; }
    public Guid WeekId { get; set; }
    public IEnumerable<Service>? Services { get; set; }
    
}
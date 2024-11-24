namespace KronApi.Core.Entities;

public class Day : EntityBase
{
    public int Hours { get; set; }
    public Guid WeekId { get; set; }
    public IEnumerable<Service>? Services { get; set; }
}
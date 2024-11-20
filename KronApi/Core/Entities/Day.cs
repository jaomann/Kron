namespace KronApi.Core.Entities;

public class Day
{
    public uint Id { get; set; }
    public int Hours { get; set; }
    public Guid WeekId { get; set; }
    public IEnumerable<Service>? Services { get; set; }
}
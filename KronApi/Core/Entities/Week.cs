namespace KronApi.Core.Entities;

public class Week
{
    public Guid Id { get; set; }
    public bool Active { get; set; }
    public int TotalHours { get; set; }
    public DateTime CreateTime { get; set; }
    public List<Day>? Days { get; set; }
}
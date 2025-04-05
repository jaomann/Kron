namespace KronApi.Core.Entities;

public class Week : EntityBase
{
    public Week()
    {
        id = Guid.NewGuid();
        Active = true;
        Days = Enumerable.Range(0, 7)
                         .Select(_ => new Day(id))
                         .ToList();
        CreateTime = DateTime.Now;
        TotalHours = Days.Sum(d => d.Hours);
    }
    public bool Active { get; set; }
    public decimal TotalHours { get; set; }
    public DateTime CreateTime { get; set; }
    public List<Day>? Days { get; set; }
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
}
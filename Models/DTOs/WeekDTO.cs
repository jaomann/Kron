namespace KronApi.Models.DTOs;

public class WeekDTO : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    public List<DayDTO> Days { get; set; } = new();
    public Guid CompanyId { get; set; }
}

public class DayDTO : BaseDTO
{
    public Guid WeekId { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}

public class CreateWeekDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
    public List<CreateDayDTO> Days { get; set; } = new();
}

public class CreateDayDTO
{
    public string Name { get; set; } = string.Empty;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
} 
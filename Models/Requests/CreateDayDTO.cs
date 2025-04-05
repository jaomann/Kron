namespace KronApi.Models.Requests;

public class CreateDayDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid WeekId { get; set; }
} 
namespace KronApi.Models.Requests;

public class UpdateDayDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid WeekId { get; set; }
} 
namespace KronApi.Models.Requests;

public class CreateServiceDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public Guid DayId { get; set; }
} 
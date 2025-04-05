namespace KronApi.Models.Requests;

public class UpdateWeekDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
} 
namespace KronApi.Models.Requests;

public class CreateWeekDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
} 
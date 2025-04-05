namespace KronApi.Models.Responses;

public class CompanyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public Guid Owner { get; set; }
    public AddressDTO? Address { get; set; }
    public WeekDTO? Week { get; set; }
} 
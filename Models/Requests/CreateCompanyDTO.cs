using KronApi.Core.Entities;

namespace KronApi.Models.Requests;

public class CreateCompanyDTO
{
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public Address? Address { get; set; }
} 
using KronApi.Core.Entities;

namespace KronApi.Models.Requests;

public class UpdateCompanyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public Guid Owner { get; set; }
    public Address? Address { get; set; }
} 
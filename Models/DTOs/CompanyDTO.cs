using KronApi.Core.Entities;

namespace KronApi.Models.DTOs;

public class CompanyDTO : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public Guid Owner { get; set; }
    public bool IsDeleted { get; set; }
    public AddressDTO? Address { get; set; }
    public WeekDTO? Week { get; set; }
}

public class CreateCompanyDTO
{
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public AddressDTO? Address { get; set; }
}

public class UpdateCompanyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public AddressDTO? Address { get; set; }
} 
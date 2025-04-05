using KronApi.Models.AddressDTO;

namespace KronApi.Models.CompanyDTO;

public class CreateCompanyDTO
{
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public CreateAddressDTO? Address { get; set; }
} 
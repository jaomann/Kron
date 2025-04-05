using KronApi.Models.AddressDTO;

namespace KronApi.Models.CompanyDTO;

public class UpdateCompanyDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public UpdateAddressDTO? Address { get; set; }
} 
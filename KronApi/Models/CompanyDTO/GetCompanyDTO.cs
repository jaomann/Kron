using KronApi.Models.AddressDTO;
using KronApi.Models.WeekDTO;

namespace KronApi.Models.CompanyDTO;

public class GetCompanyDTO : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public Guid Owner { get; set; }
    public bool IsDeleted { get; set; }
    public GetAddressDTO? Address { get; set; }
    public GetWeekDTO? Week { get; set; }
}

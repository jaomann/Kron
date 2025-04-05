using KronApi.Models.CompanyDTO;

namespace KronApi.Models.User;

public class GetUserDTO : BaseDTO
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Cpf { get; set; }
    public bool Admin { get; set; }
    public Guid? CompanyId { get; set; }
    public GetCompanyDTO? Company { get; set; }
}

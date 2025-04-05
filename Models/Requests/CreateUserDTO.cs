namespace KronApi.Models.Requests;

public class CreateUserDTO
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
} 
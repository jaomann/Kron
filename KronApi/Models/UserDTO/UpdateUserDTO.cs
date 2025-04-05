namespace KronApi.Models.UserDTO;

public class UpdateUserDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Cpf { get; set; }
    public Guid? CompanyId { get; set; }
} 
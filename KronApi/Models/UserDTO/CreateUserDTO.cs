namespace KronApi.Models.UserDTO;

public class CreateUserDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? CPF { get; set; }
} 
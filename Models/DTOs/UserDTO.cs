namespace KronApi.Models.DTOs;

public class UserDTO : BaseDTO
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Cpf { get; set; }
    public bool Admin { get; set; }
    public Guid? CompanyId { get; set; }
    public CompanyDTO? Company { get; set; }
}

public class CreateUserDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Sobrenome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Cpf { get; set; }
}

public class UpdateUserDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Cpf { get; set; }
    public Guid? CompanyId { get; set; }
}

public class LoginUserDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
} 
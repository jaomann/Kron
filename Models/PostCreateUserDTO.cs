namespace KronApi.Models;

public class PostCreateUserDTO
{
    public string Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string Email { get; set; }
    public string? CPF { get; set; }
    public string Password { get; set; }
} 
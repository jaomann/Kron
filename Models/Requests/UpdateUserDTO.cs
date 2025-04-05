namespace KronApi.Models.Requests;

public class UpdateUserDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public bool IsActive { get; set; }
} 
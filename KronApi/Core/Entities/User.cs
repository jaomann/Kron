namespace KronApi.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public bool Admin { get; set; }
    public string? Cpf { get; set; }
    public Guid CompanyID { get; set; }
    public Company? Company { get; set; }
}
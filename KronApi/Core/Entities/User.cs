using System.ComponentModel.DataAnnotations;

namespace KronApi.Core.Entities;

public class User : EntityBase
{
    [MaxLength(45)]
    public string? Username { get; set; }
    [MaxLength(322)]
    public string? Email { get; set; }
    [MaxLength(45)]
    public string? Password { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public bool Admin { get; set; }
    [MaxLength(11)]
    public string? Cpf { get; set; }
    public Guid CompanyID { get; set; }
    public Company? Company { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace KronApi.Core.Entities;

public class User : EntityBase
{
    [Required]
    [MaxLength(45)]
    public string Username { get; set; }
    
    [Required]
    [MaxLength(322)]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(45)]
    public string Password { get; set; }
    
    public DateTime CreateTime { get; set; } = DateTime.Now;
    
    public bool Admin { get; set; }
    
    [MaxLength(11)]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter exatamente 11 dígitos")]
    public string? Cpf { get; set; }
    
    public Guid? CompanyID { get; set; }
    
    public Company? Company { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace KronApi.Core.Entities;

public class Company : EntityBase
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(14)]
    [RegularExpression(@"^\d{14}$", ErrorMessage = "CNPJ deve conter exatamente 14 dígitos")]
    public string CNPJ { get; set; }
    
    [Required]
    public Guid Owner { get; set; }
    
    public DateTime CreateTime { get; set; } = DateTime.Now;
    
    public Week? Week { get; set; }
    
    public Address? Address { get; set; }
    
    public IEnumerable<User>? Users { get; set; }
}
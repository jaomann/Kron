using System.ComponentModel.DataAnnotations;

namespace KronApi.Core.Entities;

public class Company : EntityBase
{
    public string? Name { get; set; }
    [MaxLength(14)]
    public string? CNPJ { get; set; }
    public Guid Owner { get; set; }
    public DateTime CreateTime { get; set; }
    public IEnumerable<User>? Users { get; set; }
    
}
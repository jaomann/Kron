namespace KronApi.Core.Entities;

public class Company
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? CNPJ { get; set; }
    public Guid Owner { get; set; }
    public DateTime CreateTime { get; set; }
    public IEnumerable<User>? Users { get; set; }
    
}
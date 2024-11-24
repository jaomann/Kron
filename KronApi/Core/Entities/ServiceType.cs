using System.ComponentModel.DataAnnotations;

namespace KronApi.Core.Entities;

public class ServiceType : EntityBase
{
    [MaxLength(45)]
    public string? Name { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }
    public uint ServiceId { get; set; }
}
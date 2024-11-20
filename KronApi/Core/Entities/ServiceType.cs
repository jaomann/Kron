namespace KronApi.Core.Entities;

public class ServiceType
{
    public uint Id { get; set; }
    public string? Name { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }
    public uint ServiceId { get; set; }
}
namespace KronApi.Core.Entities;

public class Service
{
    public uint Id { get; set; }
    public string? ClientName { get; set; }
    public string? Phone { get; set; }
    public int Duration { get; set; }
    public Guid CompanyID { get; set; }
    public Guid UserID { get; set; }
    public uint DayID { get; set; }
    public IEnumerable<ServiceType>? ServicesTypes { get; set; }
}
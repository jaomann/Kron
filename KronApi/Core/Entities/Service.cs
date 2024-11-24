using System.ComponentModel.DataAnnotations;

namespace KronApi.Core.Entities;

public class Service : EntityBase
{
    [MaxLength(45)] 
    public string? ClientName { get; set; }
    [MaxLength(11)]
    public string? Phone { get; set; }
    public int Duration { get; set; }
    public Guid CompanyID { get; set; }
    public Guid UserID { get; set; }
    public uint DayID { get; set; }
    public IEnumerable<ServiceType>? ServicesTypes { get; set; }
}
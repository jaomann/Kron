using KronApi.Core.Entities;

namespace KronApi.Models
{
    public class WeekDTO
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public decimal TotalHours { get; set; }
        public List<Day>? Days { get; set; }
        public Guid CompanyId { get; set; }
    }
}

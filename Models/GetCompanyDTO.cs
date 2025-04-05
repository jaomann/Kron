using System.ComponentModel.DataAnnotations;

namespace KronApi.Models
{
    public class GetCompanyDTO
    {
        public string? Name { get; set; }
        [MaxLength(14)]
        public string? CNPJ { get; set; }
        public Guid Owner { get; set; }
        public WeekDTO? Week { get; set; }
        public AddressDTO? Address { get; set; }
    }
}

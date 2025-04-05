using KronApi.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace KronApi.Models
{
    public class PostCompanyDTO
    {
        public string? Name { get; set; }
        [MaxLength(14)]
        public string? CNPJ { get; set; }
        public AddressDTO Address { get; set; }
    }
}

using KronApi.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace KronApi.Models
{
    public class PutCompanyDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [MaxLength(14)]
        public string? CNPJ { get; set; }
        public bool IsDeleted { get; set; }
        public AddressDTO Address { get; set; }
    }
}

using KronApi.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace KronApi.Models
{
    public class PostUserDTO
    {
        [MaxLength(45)]
        public string? Username { get; set; }
        [MaxLength(322)]
        public string? Email { get; set; }
        [MaxLength(45)]
        public string? Password { get; set; }
        public bool Admin { get; set; }
        [MaxLength(11)]
        public string? Cpf { get; set; }
        public Guid CompanyID { get; set; }
        public Company? Company { get; set; }
    }
}

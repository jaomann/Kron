using KronApi.Core.Entities;

namespace KronApi.Models
{
    public class AddressDTO
    {
        public Guid Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public Address ToEntity()
        {
            return new Address
            {
                id = this.Id,
                Street = this.Street,
                Number = this.Number,
                Complement = this.Complement,
                Neighborhood = this.Neighborhood,
                City = this.City,
                State = this.State,
                Country = this.Country,
                ZipCode = this.ZipCode,
                CompanyId = this.CompanyId
            };
        }
    }
}

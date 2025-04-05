using KronApi.Models.AddressDTO;

namespace KronApi.Core.Entities
{
    public class Address : EntityBase
    {
        public string Street { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public Guid CompanyId {  get; set; }
        public Company? Company { get; set; }
        public override string ToString()
        {
            return $"{Street}, {Number} - {Neighborhood}, {City} - {State}, {Country}, {ZipCode}";
        }
        public GetAddressDTO ToDTO()
        {
            return new GetAddressDTO
            {
                Id = id,
                Street = Street,
                Number = Number,
                Complement = Complement,
                Neighborhood = Neighborhood,
                City = City,
                State = State,
            };
        }
    }
}

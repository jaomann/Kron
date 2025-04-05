using KronApi.Core.Entities;

namespace KronApi.Models.AddressDTO;

public class UpdateAddressDTO
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
    public Address ToAddress(Guid companyId)
    {
        return new Address
        {
            Street = Street,
            Number = Number,
            Complement = Complement,
            Neighborhood = Neighborhood,
            City = City,
            State = State,
            CompanyId = companyId,
            Country = Country,
            ZipCode = ZipCode,
        };
    }
} 
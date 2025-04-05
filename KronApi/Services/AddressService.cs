using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using KronApi.Core.Entities;
using KronApi.Models.AddressDTO;

namespace KronApi.Services;

public class AddressService : BaseService<Address>, IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IConfiguration configuration, IAddressRepository addressRepository) 
        : base(configuration, addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<Address> GetByCompanyIdAsync(Guid companyId)
    {
        return await _addressRepository.GetByCompanyIdAsync(companyId);
    }

    public GetAddressDTO MapDTO(Address address)
    {
        if (address == null) return new GetAddressDTO();
        
        return new GetAddressDTO
        {
            Id = address.id,
            Street = address.Street,
            Number = address.Number,
            City = address.City,
            State = address.State,
            Country = address.Country,
            ZipCode = address.ZipCode,
            CompanyId = address.CompanyId
        };
    }
} 
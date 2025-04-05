using KronApi.Core.Entities;
using KronApi.Models.AddressDTO;

namespace KronApi.Core.Contracts.Service
{
    public interface IAddressService : IBaseService<Address>
    {
        Task<Address> GetByCompanyIdAsync(Guid companyId);
        GetAddressDTO MapDTO(Address address);
    }
}

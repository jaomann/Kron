using KronApi.Core.Entities;

namespace KronApi.Core.Contracts.Repository
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        Task<Address> GetByCompanyIdAsync(Guid id);
    }
}

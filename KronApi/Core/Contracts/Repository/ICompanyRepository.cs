using KronApi.Core.Entities;

namespace KronApi.Core.Contracts.Repository;

public interface ICompanyRepository : IBaseRepository<Company>
{
    Task<bool> IsExistAsync(Guid id);
    Task<bool> IsExistByCnpjAsync(string cnpj);
    Task<Company> GetByCnpjAsync(string cnpj);
}
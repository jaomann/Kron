using KronApi.Core.Entities;

namespace KronApi.Core.Contracts.Service;

public interface ICompanyService : IBaseService<Company>
{
    Task<bool> IsExistAsync(Guid id);
    Task<bool> IsExistByCnpjAsync(string cnpj);
}
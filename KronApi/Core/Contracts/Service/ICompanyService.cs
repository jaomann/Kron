using KronApi.Core.Entities;
using KronApi.Models.CompanyDTO;

namespace KronApi.Core.Contracts.Service;

public interface ICompanyService : IBaseService<Company>
{
    Task<bool> IsExistAsync(Guid id);
    Task<bool> IsExistByCnpjAsync(string cnpj);
    Task<Company> GetByCnpjAsync(string cnpj);
    Company MapPostDTO(CreateCompanyDTO company);
    Company MapPutDTO(UpdateCompanyDTO company);
    GetCompanyDTO MapEntityToGetDTO(Company company);
}
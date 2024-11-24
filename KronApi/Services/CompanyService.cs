using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using KronApi.Core.Entities;

namespace KronApi.Services;

public class CompanyService : BaseService<Company>, ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    public CompanyService(ICompanyRepository repository) : base(repository)
    {
        _companyRepository = repository;
    }

    public async Task<bool> IsExistAsync(Guid id) => await _companyRepository.IsExistAsync(id);
    public async Task<bool> IsExistByCnpjAsync(string cnpj) => await _companyRepository.IsExistByCnpjAsync(cnpj);
    public async Task<Company> GetByCnpjAsync(string cnpj) => await _companyRepository.GetByCnpjAsync(cnpj);
}
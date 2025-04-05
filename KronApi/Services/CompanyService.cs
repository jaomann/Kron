using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using KronApi.Core.Entities;
using KronApi.Models.CompanyDTO;
using System.Text.RegularExpressions;

namespace KronApi.Services;

public class CompanyService : BaseService<Company>, ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserService _userService;

    public CompanyService(
        ICompanyRepository repository,
        IUserService userService,
        IConfiguration configuration) : base(configuration, repository)
    {
        _companyRepository = repository;
        _userService = userService;
    }

    public async Task<bool> IsExistAsync(Guid id) => await _companyRepository.IsExistAsync(id);

    public async Task<bool> IsExistByCnpjAsync(string cnpj)
    {
        if (!IsValidCnpj(cnpj))
            return false;
            
        return await _companyRepository.IsExistByCnpjAsync(cnpj);
    }
    public async Task<Company?> GetByIdAsync(Guid id) => await _companyRepository.GetByIdAsync(id);

    public async Task<Company?> GetByCnpjAsync(string cnpj)
    {
        if (!IsValidCnpj(cnpj))
            throw new ArgumentException("Invalid CNPJ format");
            
        return await _companyRepository.GetByCnpjAsync(cnpj);
    }

    public override async Task Create(Company entity)
    {
        if (!IsValidCnpj(entity.CNPJ))
            throw new ArgumentException("Invalid CNPJ format");

        if (string.IsNullOrWhiteSpace(entity.Name))
            throw new ArgumentException("Company name is required");

        var ownerExists = await _userService.GetByIdAsync(entity.Owner) != null;
        if (!ownerExists)
            throw new ArgumentException("Owner user not found");

        await base.Create(entity);
    }

    public override async Task Update(Company entity)
    {
        if (!IsValidCnpj(entity.CNPJ))
            throw new ArgumentException("Invalid CNPJ format");

        if (string.IsNullOrWhiteSpace(entity.Name))
            throw new ArgumentException("Company name is required");

        await base.Update(entity);
    }

    public Company MapPostDTO(CreateCompanyDTO company) => 
        new Company
        {
            Name = company.Name ?? throw new ArgumentException("Company name is required"),
            CNPJ = company.CNPJ ?? throw new ArgumentException("CNPJ is required"),
            isDeleted = false,
            Address = company.Address.ToAddress(),
            CreateTime = DateTime.Now,
            id = Guid.NewGuid()
        };

    public Company MapPutDTO(UpdateCompanyDTO company) =>
        new Company
        {
            Name = company.Name ?? throw new ArgumentException("Company name is required"),
            CNPJ = company.CNPJ ?? throw new ArgumentException("CNPJ is required"),
            isDeleted = company.IsDeleted,
            Address = company.Address?.ToAddress(company.Id),
            id = company.Id
        };

    public GetCompanyDTO MapEntityToGetDTO(Company company) =>
        new GetCompanyDTO
        {
            Id = company.id,
            Name = company.Name,
            CNPJ = company.CNPJ,
            Owner = company.Owner,
            IsDeleted = company.isDeleted,
            Address = company.Address.ToDTO()
        };

    private static bool IsValidCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        // Remove caracteres não numéricos
        cnpj = Regex.Replace(cnpj, "[^0-9]", "");

        // Verifica se tem 14 dígitos
        if (cnpj.Length != 14)
            return false;

        // Verifica se todos os dígitos são iguais
        if (cnpj.Distinct().Count() == 1)
            return false;

        // Calcula os dígitos verificadores
        var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCnpj = cnpj[..12];
        var soma = 0;

        for (var i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        var digito = resto.ToString();
        tempCnpj += digito;
        soma = 0;

        for (var i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }
}
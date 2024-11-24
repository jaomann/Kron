using KronApi.Core.Entities;

namespace KronApi.Core.Contracts.Service;

public interface IUserService : IBaseService<User>
{
    Task<bool> IsExistAsync(string email, string cpf);
}
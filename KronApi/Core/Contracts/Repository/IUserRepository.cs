using KronApi.Core.Entities;

namespace KronApi.Core.Contracts.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<bool> IsExistAsync(string email, string cpf);
}
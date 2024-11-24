using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using KronApi.Core.Entities;

namespace KronApi.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository):base(userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<bool> IsExistAsync(string email, string cpf) => await _userRepository.IsExistAsync(email, cpf);
}
using KronApi.Core.Entities;
using KronApi.Models.UserDTO;

namespace KronApi.Core.Contracts.Service;

public interface IUserService : IBaseService<User>
{
    Task<bool> ValidateUserCredentials(string email, string password);
    Task<bool> CheckUserExistsByCpf(string email, string cpf);
    Task<bool> CheckUserExistsByEmail(string email);
    Task<(bool success, string message)> InitiateUserRegistration(CreateUserDTO userDto);
    Task<(bool success, string message)> CompleteUserRegistration(string email, string token);
}
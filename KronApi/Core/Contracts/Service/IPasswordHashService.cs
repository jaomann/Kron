namespace KronApi.Core.Contracts.Service;

public interface IPasswordHashService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
} 
namespace KronApi.Core.Contracts.Service;

public interface IEmailService
{
    Task SendConfirmationEmailAsync(string email, string token);
    Task<bool> VerifyConfirmationTokenAsync(string email, string token);
} 
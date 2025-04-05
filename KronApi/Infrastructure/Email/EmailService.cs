using KronApi.Core.Contracts.Service;
using System.Net;
using System.Net.Mail;

namespace KronApi.Infrastructure.Email;

public class EmailService : BaseInfrastructureService, IEmailService
{
    private readonly EmailConfiguration _config;
    
    public EmailService(IConfiguration configuration) : base(configuration)
    {
        _config = GetSection<EmailConfiguration>("Email");
    }

    public async Task SendConfirmationEmailAsync(string email, string token)
    {
        return;
    }

    public async Task<bool> VerifyConfirmationTokenAsync(string email, string token)
    {
        // TODO: Implement token verification using Redis
        return await Task.FromResult(true);
    }
} 
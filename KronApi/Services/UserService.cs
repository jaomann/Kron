using System.Text.RegularExpressions;
using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using KronApi.Core.Entities;
using KronApi.Infrastructure.Cache;
using KronApi.Models.User;
using KronApi.Models.UserDTO;

namespace KronApi.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IRedisService _redisService;
    private const string REGISTRATION_PREFIX = "registration:";
    private static readonly int REGISTRATION_EXPIRY = 24;
    private readonly IPasswordHashService _passwordHashService;

    public UserService(
        IUserRepository userRepository,
        IEmailService emailService,
        IRedisService redisService,
        IConfiguration configuration,
        IPasswordHashService passwordHashService) : base(configuration,userRepository)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _redisService = redisService;
        _passwordHashService = passwordHashService;
    }

    public async Task<bool> ValidateUserCredentials(string email, string password)
    {
        if (!IsValidEmail(email))
            return false;

        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return false;
        return _passwordHashService.VerifyPassword(password, user.Password);
    }

    public async Task<bool> CheckUserExistsByCpf(string email, string cpf)
    {
        if (!IsValidCpf(cpf))
            return false;
            
        return await _userRepository.IsExistAsync(email, cpf);
    }

    public async Task<bool> CheckUserExistsByEmail(string email)
    {
        if (!IsValidEmail(email))
            return false;
            
        var user = await _userRepository.GetByEmailAsync(email);
        return user != null;
    }

    public async Task<User?> GetByIdAsync(Guid id) => await _userRepository.GetByIdAsync(id);

    public async Task<(bool success, string message)> InitiateUserRegistration(CreateUserDTO userDto)
    {
        try
        {
            // Validar email
            if (!IsValidEmail(userDto.Email))
                return (false, "Invalid email format");

            // Validar CPF se fornecido
            if (!string.IsNullOrEmpty(userDto.CPF) && !IsValidCpf(userDto.CPF))
                return (false, "Invalid CPF format");

            // Validar senha
            if (!IsValidPassword(userDto.Password))
                return (false, "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number and one special character");

            // Verificar se o email já existe
            if (await CheckUserExistsByEmail(userDto.Email))
                return (false, "Email already registered");

            // Gerar token de confirmação
            var confirmationToken = Guid.NewGuid().ToString();

            // Enviar email de confirmação
            await _emailService.SendConfirmationEmailAsync(userDto.Email, confirmationToken);

            // Armazenar dados no Redis
            try
            {
                await StoreTemporaryRegistration(userDto, confirmationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return (true, confirmationToken);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool success, string message)> CompleteUserRegistration(string email, string token)
    {
        try
        {
            // Verificar token
            var isValid = await _emailService.VerifyConfirmationTokenAsync(email, token);
            if (!isValid)
                return (false, "Invalid or expired token");

            // Recuperar dados do Redis
            var userData = await GetTemporaryRegistration(email, token);
            if (userData == null)
                return (false, "Registration data not found or expired");

            // Criar usuário
            var user = new User
            {
                id = Guid.NewGuid(),
                Email = userData.Email,
                Username = $"{userData.Nome}{userData.Sobrenome}",
                Password = _passwordHashService.HashPassword(userData.Password),
                CreateTime = DateTime.Now,
                Admin = false,
                Cpf = userData.CPF
            };

            await Create(user);

            // Limpar dados temporários
            await DeleteTemporaryRegistration(email, token);

            return (true, "User created successfully");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    private async Task StoreTemporaryRegistration(CreateUserDTO userDto, string token)
    {
        var key = $"{REGISTRATION_PREFIX}{userDto.Email}:{token}";
        await _redisService.SetAsync(key, userDto, REGISTRATION_EXPIRY);
    }

    private async Task<CreateUserDTO?> GetTemporaryRegistration(string email, string token)
    {
        var key = $"{REGISTRATION_PREFIX}{email}:{token}";
        return await _redisService.GetAsync<CreateUserDTO>(key);
    }

    private async Task DeleteTemporaryRegistration(string email, string token)
    {
        var key = $"{REGISTRATION_PREFIX}{email}:{token}";
        await _redisService.RemoveAsync(key);
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsValidCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        return Regex.IsMatch(cpf, @"^\d{11}$");
    }

    private static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            return false;

        var hasNumber = password.Any(char.IsDigit);
        var hasUpper = password.Any(char.IsUpper);
        var hasLower = password.Any(char.IsLower);
        var hasSpecial = password.Any(c => !char.IsLetterOrDigit(c));

        return hasNumber && hasUpper && hasLower && hasSpecial;
    }
}
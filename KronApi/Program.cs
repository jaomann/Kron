using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using KronApi.Core.Entities;
using KronApi.Extensions;
using KronApi.Infrastructure.Cache;
using KronApi.Infrastructure.Email;
using KronApi.Models.CompanyDTO;
using KronApi.Models.UserDTO;
using KronApi.Repository;
using KronApi.Repository.Database;
using KronApi.Services;
using Microsoft.EntityFrameworkCore;

namespace KronApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Database
        builder.Services.AddDbContext<Context>(opt =>
            opt.UseMySql(builder.Configuration.GetConnectionString("cnMySql"),
                new MySqlServerVersion(new Version(8, 0, 0)))); 
        
        // Auth & Infrastructure
        builder.Services.AddAuthorization();
        builder.Services.AddInfrastructure(builder.Configuration);

        // Application Services
        builder.Services.AddApplicationServices();

        // API Documentation
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        // CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseAuthorization();

        // Health Check endpoint
        app.MapGet("/health", async (Context dbContext) =>
        {
            try
            {
                // Tenta conectar ao banco
                var canConnect = await dbContext.Database.CanConnectAsync();
                if (!canConnect)
                    return Results.Problem("Cannot connect to database", statusCode: 503);

                return Results.Ok(new { Status = "Healthy", Database = "Connected" });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message, statusCode: 503);
            }
        })
        .WithName("HealthCheck")
        .WithOpenApi();

        // Company endpoints group
        var companyGroup = app.MapGroup("/company");
        
        companyGroup.MapDelete("/", async (Guid id, ICompanyService companyService) =>
        {
            try
            {
                await companyService.Delete(id);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("DeleteCompany")
        .WithOpenApi();

        companyGroup.MapPut("/", async (UpdateCompanyDTO companyDto, ICompanyService companyService) =>
        {
            try
            {
                var exists = await companyService.IsExistAsync(companyDto.Id);
                if (!exists) return Results.NotFound();
                var company = companyService.MapPutDTO(companyDto);
                await companyService.Update(company);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("UpdateCompany")
        .WithOpenApi();

        companyGroup.MapPost("/", async (CreateCompanyDTO companyDto, ICompanyService companyService, IWeekService weekService, IDayService dayService, IUserService userService, IAddressService addressService) =>
        {
            try
            {
                var company_exists = companyDto.CNPJ is not null ?
                                 await companyService.GetByCnpjAsync(companyDto.CNPJ) : null;

                if (company_exists != null) return Results.Ok(company_exists);
                var company = companyService.MapPostDTO(companyDto);
                var week = new Week();
                var days = week.Days;
                var address = company.Address;
                company.Address = null;
                company.id = Guid.NewGuid();
                company.Owner = Guid.Parse("08dd0c1c-efab-436b-81a6-8a7bfbf2db02");
                await companyService.Create(company);
                
                if (address != null)
                {
                    address.id = Guid.NewGuid();
                    address.CompanyId = company.id;
                    await addressService.Create(address);
                }
                
                week.CompanyId = company.id;
                week.Days = null;
                await weekService.Create(week);
                await dayService.AddDays(days);

                return Results.Ok(company);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("CreateCompany")
        .WithOpenApi();

        companyGroup.MapGet("/", async (Guid id, ICompanyService companyService, IWeekService weekService, IAddressService addressService) =>
        {
            var company = await companyService.GetByIdAsync(id);
            if (company == null)
                return Results.NotFound();
                
            var companyDTO = companyService.MapEntityToGetDTO(company);
            var address = await addressService.GetByCompanyIdAsync(company.id);
            var week = await weekService.GetByCompanyIdAsync(company.id);
            
            if (address != null)
                companyDTO.Address = addressService.MapDTO(address);
                
            if (week != null)
                companyDTO.Week = weekService.MapDTO(week);
                
            return Results.Ok(companyDTO);
        })
        .Produces<GetCompanyDTO>(StatusCodes.Status200OK)
        .WithName("GetCompany")
        .WithOpenApi();

        // User endpoints group
        var userGroup = app.MapGroup("/user");

        userGroup.MapDelete("/", async (Guid id, IUserService userService) =>
        {
            try
            {
                await userService.Delete(id);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("DeleteUser")
        .WithOpenApi();

        userGroup.MapPut("/", async (User user, IUserService userService) =>
        {
            try
            {
                var exists = await userService.ValidateUserCredentials(user.Email, user.Password);
                if (!exists) return Results.NotFound();

                await userService.Update(user);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("UpdateUser")
        .WithOpenApi();

        userGroup.MapPost("/", async (User user, IUserService userService, ICompanyService companyService) =>
        {
            try
            {
                var exists = user is { Email: not null, Cpf: not null } &&
                                 await userService.CheckUserExistsByCpf(user.Email, user.Cpf);
                var checkCompany = await companyService.IsExistAsync(user.CompanyID ?? new Guid());
                if (!exists && checkCompany)
                {
                    user.id = Guid.NewGuid();
                    await userService.Create(user);
                }

                return Results.Ok(await userService.GetByIdAsync(user.id));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("CreateUser")
        .WithOpenApi();

        userGroup.MapGet("/", async (Guid id, IUserService userService) =>
        {
            var user = await userService.GetByIdAsync(id);
            if (user is null) 
                return Results.NotFound("User not found");

            return Results.Ok(user);
        })
        .WithName("GetUser")
        .WithOpenApi();

        // Authentication endpoints group
        var authGroup = app.MapGroup("/auth");

        authGroup.MapPost("/check-email", async (LoginUserDTO loginDto, IUserService userService) =>
        {
            try
            {
                if (string.IsNullOrEmpty(loginDto.Email))
                    return Results.BadRequest("Email is required");

                var userExists = await userService.CheckUserExistsByEmail(loginDto.Email);
                if (!userExists)
                    return Results.NotFound("User not found");

                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("CheckEmail")
        .WithOpenApi();

        authGroup.MapPost("/verify-password", async (LoginUserDTO loginDto, IUserService userService, IEmailService emailService) =>
        {
            try
            {
                if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                    return Results.BadRequest("Email and password are required");

                var credentialsValid = await userService.ValidateUserCredentials(loginDto.Email, loginDto.Password);
                if (!credentialsValid)
                    return Results.NotFound("Invalid credentials");

                // Generate confirmation token (you should implement a proper token generation)
                var confirmationToken = Guid.NewGuid().ToString();

                // Send confirmation email
                await emailService.SendConfirmationEmailAsync(loginDto.Email, confirmationToken);

                return Results.Ok(new { message = "Confirmation email sent", token = confirmationToken });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("VerifyPassword")
        .WithOpenApi();

        authGroup.MapPost("/confirm-email", async (string email, string token, IEmailService emailService) =>
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                    return Results.BadRequest("Email and token are required");

                var isValid = await emailService.VerifyConfirmationTokenAsync(email, token);
                if (!isValid)
                    return Results.BadRequest("Invalid or expired token");

                return Results.Ok(new { message = "Email confirmed successfully" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("ConfirmEmail")
        .WithOpenApi();

        authGroup.MapPost("/register", async (CreateUserDTO userDto, IUserService userService) =>
        {
            try
            {
                (bool success, string message) = await userService.InitiateUserRegistration(userDto);
                if (!success)
                    return Results.BadRequest(message);

                return Results.Ok(new { message = "Registration initiated. Please check your email.", token = message });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("RegisterUser")
        .WithOpenApi();

        authGroup.MapPost("/register/confirm", async (string email, string token, IUserService userService) =>
        {
            try
            {
                (bool success, string message) = await userService.CompleteUserRegistration(email, token);
                if (!success)
                    return Results.BadRequest(message);

                return Results.Ok(new { message = "Registration completed successfully" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithName("ConfirmRegistration")
        .WithOpenApi();

        app.Run();
    }
}

using System.Net;
using KronApi.Core.Contracts.Repository;
using KronApi.Core.Contracts.Service;
using KronApi.Core.Entities;
using KronApi.Repository;
using KronApi.Repository.Database;
using KronApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KronApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<Context>(opt =>
            opt.UseMySql(builder.Configuration.GetConnectionString("cnMySql"),
                new MySqlServerVersion(new Version(8, 0, 11)))); 
        
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
        builder.Services.AddScoped<ICompanyService, CompanyService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        #region API
            #region Company
                app.MapDelete("/company", async (Guid id, ICompanyService companyService) =>
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
                }).WithName("DeleteCompany").WithOpenApi();
                
                app.MapPut("/company", async (Company company, ICompanyService companyService) =>
                {
                    try
                    {
                        var exists = await companyService.IsExistAsync(company.Id);
                        if (!exists) return Results.NotFound();
                        
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
                
                app.MapPost("/company", async (Company company, ICompanyService companyService) =>
                {
                    try
                    {
                        var exists = company.CNPJ is not null &&
                                     await companyService.IsExistByCnpjAsync(company.CNPJ);
                        
                        if (exists) return Results.Ok();

                        company.Id = new Guid();
                        await companyService.Create(company);

                        return Results.Ok();
                    }
                    catch (Exception ex)
                    {
                        return Results.BadRequest(ex.Message);
                    }
                })
                .WithName("CreateCompany")
                .WithOpenApi();
                
                app.MapGet("/company", async (Guid id, ICompanyService companyService) =>
                {
                    var company = await companyService.GetByIdAsync(id);
                    return company ?? new Company();
                })
                .WithName("GetCompany")
                .WithOpenApi();
            #endregion
            #region User
            app.MapDelete("/user", async (Guid id, IUserService userService) =>
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
            }).WithName("DeleteUser")
            .WithOpenApi();
                
            app.MapPut("/user", async (User user, IUserService userService) =>
                {
                    try
                    {
                        var exists = await userService.IsExistAsync(user.Email, user.Password);
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
            
                app.MapPost("/user", async (User user, IUserService userService, ICompanyService companyService) =>
                {
                    var exists = user is { Email: not null, Cpf: not null } && await userService.IsExistAsync(user.Email, user.Cpf);
                    var checkCompany = await companyService.IsExistAsync(user.CompanyID);

                    if (!exists && checkCompany)
                    {
                        user.Id = new Guid();
                        await userService.Create(user);
                    }
                    
                    return Results.Ok();
                })
                .WithName("CreateUser")
                .WithOpenApi();
                
                app.MapGet("/user", async (Guid id, IUserService userService) =>
                {
                    var user = await userService.GetByIdAsync(id);
                    
                    return user ?? new User();
                })
                .WithName("GetUser")
                .WithOpenApi();

            #endregion
        #endregion
        app.Run();
    }
}

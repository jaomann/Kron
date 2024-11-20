using KronApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace KronApi.Repository.Database;

public class Context : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Week> Week { get; set; }
    public DbSet<Service> Service { get; set; }
    public DbSet<Day> Day { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<ServiceType> ServiceType { get; set; }
}
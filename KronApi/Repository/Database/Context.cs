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
    public DbSet<Address> Address { get; set; }
    public DbSet<ServiceType> ServiceType { get; set; }
    private readonly string _connectionString;

    public Context(DbContextOptions options) : base(options)
    { }
    public Context(DbContextOptions options, string connectionString) : this(options)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!string.IsNullOrEmpty(_connectionString))
        {
            optionsBuilder.UseMySql(_connectionString, new MySqlServerVersion(new Version(8,0,11)));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}
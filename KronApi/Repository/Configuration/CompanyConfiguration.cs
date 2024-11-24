using KronApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KronApi.Repository.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("company");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(c => c.CNPJ).HasColumnName("cnpj").IsRequired();
        builder.Property(c => c.Name).HasColumnName("name").IsRequired();
        builder.Property(c => c.Owner).HasColumnName("owner");
        builder.Property(c => c.IsDeleted).HasColumnName("isDeleted");
        builder.HasMany(u => u.Users)
            .WithOne(u => u.Company)
            .HasForeignKey(u => u.CompanyID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
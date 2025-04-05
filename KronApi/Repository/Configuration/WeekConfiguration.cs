using KronApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KronApi.Repository.Configuration;

public class WeekConfiguration : IEntityTypeConfiguration<Week>
{
    public void Configure(EntityTypeBuilder<Week> builder)
    {
        builder.ToTable("week");
        builder.HasKey(w => w.id);
        builder.Property(w => w.isDeleted);
        builder.Property(w => w.Active);
        builder.Property(w => w.CreateTime);
        builder.Property(w => w.TotalHours);
        builder.HasOne(w => w.Company)
            .WithOne(c => c.Week)
            .HasForeignKey<Week>(w => w.CompanyId);
    }
}
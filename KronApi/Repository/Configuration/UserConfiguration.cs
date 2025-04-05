using KronApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KronApi.Repository.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.HasKey(u => u.id);
        builder.Property(u => u.id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(u => u.Email).HasColumnName("email").IsRequired();
        builder.Property(u => u.Username).HasColumnName("username").IsRequired();
        builder.Property(u => u.Cpf).HasColumnName("cpf").IsRequired();
        builder.Property(u => u.Password).HasColumnName("password").IsRequired();
        builder.Property(u => u.Admin).HasColumnName("admin");
        builder.Property(u => u.isDeleted).HasColumnName("isDeleted");
        builder.Property(u => u.CreateTime).HasColumnName("createTime");
    }
}
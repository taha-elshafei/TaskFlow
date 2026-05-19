using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name).IsRequired().HasMaxLength(50);
        builder.HasIndex(r => r.Name).IsUnique();
    }
}

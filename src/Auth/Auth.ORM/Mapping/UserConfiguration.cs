using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.ORM.Mapping;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Username).IsUnique();
        
        builder.Property(u => u.Username)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(u => u.Password)
            .HasColumnType("varchar(max)")
            .IsRequired();

        builder.Property(u => u.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.Property(u => u.ModifiedOn)
            .HasColumnType("datetime2(7)");
    }
}
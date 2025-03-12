using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Domain.Entities;

namespace Products.ORM.Mapping;

public class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasColumnType("text")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.Description)
            .HasColumnType("text")
            .HasMaxLength(300)
            .IsRequired();
        
        builder.Property(p => p.Price)
            .HasColumnType("numeric(18,2)")
            .IsRequired();
        
        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("timestamp with time zone");
    }
}
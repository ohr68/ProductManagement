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
            .HasColumnType("varchar(100)")
            .IsRequired();
        
        builder.Property(p => p.Description)
            .HasColumnType("varchar(300)")
            .IsRequired();
        
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(p => p.CreatedAt)
            .HasColumnType("timestamp(7)")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("timestamp(7)");
    }
}
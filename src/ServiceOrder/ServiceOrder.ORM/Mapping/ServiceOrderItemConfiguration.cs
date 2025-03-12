using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrder.Domain.Entities;

namespace ServiceOrder.ORM.Mapping;

public class ServiceOrderItemConfiguration : IEntityTypeConfiguration<ServiceOrderItem>
{
    public void Configure(EntityTypeBuilder<ServiceOrderItem> builder)
    {
        builder.ToTable("ServiceOrderItems");
        
        builder.HasKey(x => x.Id);

        builder.Property(s => s.ServiceOrderId)
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(s => s.ProductId)
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(s => s.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}
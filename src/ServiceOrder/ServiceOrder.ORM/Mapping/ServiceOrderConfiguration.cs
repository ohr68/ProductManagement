using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceOrder.Domain.Entities;

namespace ServiceOrder.ORM.Mapping;

public class ServiceOrderConfiguration : IEntityTypeConfiguration<Domain.Entities.ServiceOrder>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.ServiceOrder> builder)
    {
        builder.ToTable("ServiceOrders");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Status)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(s => s.TotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnType("datetime2(7)")
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.Property(s => s.CompletedAt)
            .HasColumnType("datetime2(7)");

        builder.HasMany(s => s.Items)
            .WithOne(i => i.ServiceOrder)
            .HasForeignKey(i => i.ServiceOrderId);
    }
}
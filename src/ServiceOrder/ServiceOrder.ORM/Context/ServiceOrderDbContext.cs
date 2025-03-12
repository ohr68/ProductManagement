using Microsoft.EntityFrameworkCore;

namespace ServiceOrder.ORM.Context;

public class ServiceOrderDbContext(DbContextOptions<ServiceOrderDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.ServiceOrder> ServiceOrders { get; set; }
    public DbSet<Domain.Entities.ServiceOrderItem> ServiceOrderItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceOrderDbContext).Assembly);
    }
}
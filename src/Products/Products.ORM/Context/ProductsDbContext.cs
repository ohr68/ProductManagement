using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;

namespace Products.ORM.Context;

public class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);
    }
}
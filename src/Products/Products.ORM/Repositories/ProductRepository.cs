using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.ORM.Context;

namespace Products.ORM.Repositories;

public class ProductRepository(ProductsDbContext context) : IRepository<Product>
{
    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await context.Products.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Products.ToListAsync(cancellationToken);

    public IQueryable<Product> Query(Expression<Func<Product, bool>> filter)
        => context.Products.Where(filter);

    public async Task<bool> CreateAsync(Product entity, CancellationToken cancellationToken = default)
    {
        context.Products.Add(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Product entity, CancellationToken cancellationToken = default)
    {
        context.Products.Remove(entity);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
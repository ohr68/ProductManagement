using System.Linq.Expressions;

namespace Products.Domain.Interfaces;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    IQueryable<T> Query(Expression<Func<T, bool>> filter);
    Task<bool> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken);
}
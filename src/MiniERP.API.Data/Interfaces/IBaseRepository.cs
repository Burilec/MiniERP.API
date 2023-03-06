using MiniERP.API.Models;
using System.Linq.Expressions;

namespace MiniERP.API.Data.Interfaces
{
    public interface IBaseRepository<T> : IAsyncDisposable where T : BaseEntity, new()
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(Guid apiId);
        Task RemoveAsync(int id);
        Task SaveChangesAsync();
    }
}
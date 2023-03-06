using Microsoft.EntityFrameworkCore;
using MiniERP.API.Data.Interfaces;
using MiniERP.API.Models;
using System.Linq.Expressions;

namespace MiniERP.API.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        private readonly MiniERPDbContext _db;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(MiniERPDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.AsNoTracking().Where(predicate).ToListAsync().ConfigureAwait(continueOnCapturedContext: false);

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.AsNoTracking()
                           .Where(predicate)
                           .FirstOrDefaultAsync()
                           .ConfigureAwait(continueOnCapturedContext: false);

        public virtual async Task<List<T>> GetAllAsync()
            => await _dbSet.AsNoTracking().ToListAsync().ConfigureAwait(continueOnCapturedContext: false);

        public virtual async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public virtual async Task RemoveAsync(Guid apiId)
        {
            _dbSet.Remove(new T { ApiId = apiId });
            await SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public virtual async Task RemoveAsync(int id)
        {
            _dbSet.Remove(new T { Id = id });
            await SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task SaveChangesAsync()
            => await _db.SaveChangesAsync().ConfigureAwait(continueOnCapturedContext: false);

        public async ValueTask DisposeAsync()
            => await _db.DisposeAsync().ConfigureAwait(continueOnCapturedContext: false);
    }
}
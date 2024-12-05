using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimKhoBau.Data.Repositories;

namespace YourNamespace.Repositories
{
    public class Repository<TContext> : IRepository
            where TContext : DbContext

    {
        private readonly DbContext _context;
        protected readonly TContext DbContext;

        public Repository(DbContext context, TContext dbContext)
        {
            _context = context;
            DbContext = dbContext;
        }


        public virtual Task AddAsync<TEntity>(TEntity entity)
          where TEntity : class, IEntity<Guid>
        {
            return AddAsync<TEntity, Guid>(entity);
        }

        public virtual Task AddAsync<TEntity, TKey>(TEntity entity)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return DbContext.AddAsync(entity).AsTask();
        }
        public async Task AddRangeAsync<TEntity, TKey>(IEnumerable<TEntity> entities) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task DeleteAsync<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task UpdateAsync<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task<TEntity> FindAsync<TEntity, TKey>(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(where);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync<TEntity, TKey>(Expression<Func<TEntity, bool>> where = null, bool isTracking = true, IEnumerable<string> includes = null) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            return await (isTracking ? query.ToListAsync() : query.AsNoTracking().ToListAsync());
        }

        public IQueryable<TEntity> GetQueryable<TEntity, TKey>() where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

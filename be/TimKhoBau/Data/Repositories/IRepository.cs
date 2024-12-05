using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace TimKhoBau.Data.Repositories
{
    public interface IRepository
    {

        Task AddAsync<TEntity>(TEntity entity) where TEntity : class, IEntity<Guid>;


        Task AddRangeAsync<TEntity, TKey>(IEnumerable<TEntity> entities) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>;

            Task DeleteAsync<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>;

            Task UpdateAsync<TEntity, TKey>(TEntity entity) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>;

            Task<TEntity> FindAsync<TEntity, TKey>(Expression<Func<TEntity, bool>> where, IEnumerable<string> includes = null) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>;

            Task<IEnumerable<TEntity>> FindAllAsync<TEntity, TKey>(Expression<Func<TEntity, bool>> where = null, bool isTracking = true, IEnumerable<string> includes = null) where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>;

            IQueryable<TEntity> GetQueryable<TEntity, TKey>() where TEntity : class, IEntity<TKey> where TKey : IEquatable<TKey>;

            Task SaveChangeAsync();
        
    }
}

using RSSApp.Entity.Models.Interfaces;
using System.Linq.Expressions;

namespace RSSApp.Data.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public Task CreateAsync(TEntity entity);
        public Task CreateRangeAsync(IEnumerable<TEntity> entities);
        public Task<IEnumerable<TEntity>> LoadAsync();
        public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);
        public Task DeleteAsync(TEntity entity);
        public Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        public Task UpdateAsync(TEntity entity);
        public Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    }
}

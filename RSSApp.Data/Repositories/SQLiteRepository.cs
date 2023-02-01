using Microsoft.EntityFrameworkCore;
using RSSApp.Data.Repositories.Contexts;
using RSSApp.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace RSSApp.Data.Repositories
{
    public class SQLiteRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        DbContext _context;
        DbSet<TEntity> _dbSet;

        public SQLiteRepository()
        {
            _context = new SQLiteContext();
            _dbSet = _context.Set<TEntity>();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> LoadAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            IEnumerable<TEntity> query = null;

            foreach (var include in includes)
            {
                query = _dbSet.Include(include);
            }

            return query ?? _dbSet;
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}

using API.ApplicationDb;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RepositoryImpl<T, TKey> : IGenericRepository<T, TKey> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetFilteredAsync(FilterParameter<T> parameters)
        {
            IQueryable<T> query = _dbSet;

            if (parameters.Filter != null)
            {
                query = query.Where(parameters.Filter);
            }

            if (parameters.Include != null)
            {
                query = parameters.Include(query);
            }

            if (parameters.OrderBy != null)
            {
                query = parameters.OrderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetQueryResultAsync(QueryParameter<T> parameters)
        {
            IQueryable<T> query = _dbSet;

            if (parameters.Where != null)
            {
                query = query.Where(parameters.Where);
            }

            if (parameters.Include != null)
            {
                query = parameters.Include(query);
            }

            if (parameters.OrderBy != null)
            {
                query = parameters.IsAscending
                            ? query.AsEnumerable().OrderBy(parameters.OrderBy).AsQueryable()
                            : query.AsEnumerable().OrderByDescending(parameters.OrderBy).AsQueryable();
            }

            return await query.ToListAsync();
        }




    }
}

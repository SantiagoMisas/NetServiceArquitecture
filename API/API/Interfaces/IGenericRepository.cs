using API.Generics;
using API.Repositories;
using System.Linq.Expressions;

namespace API.Interfaces
{
    public interface IGenericRepository<T, TKey> where T : class
    {
        Task<T> GetByIdAsync(TKey id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TKey id);
        Task SaveChangesAsync();
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetFilteredAsync(FilterParameter<T> parameters);
        Task<IEnumerable<T>> GetQueryResultAsync(QueryParameter<T> parameters);
    }
}

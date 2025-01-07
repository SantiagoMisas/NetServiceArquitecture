using API.Generics;
using API.Repositories;

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
        Task<IEnumerable<T>> GetFilteredAsync(FilterParameter<T> parameters);
        Task<IEnumerable<T>> GetQueryResultAsync(QueryParameter<T> parameters);
    }
}

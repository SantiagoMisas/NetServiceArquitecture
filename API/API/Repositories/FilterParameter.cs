using System.Linq.Expressions;

namespace API.Repositories
{
    public class FilterParameter<T>
    {
        public Expression<Func<T, bool>> Filter { get; set; }
        public Func<IQueryable<T>, IQueryable<T>> Include { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }
    }
}

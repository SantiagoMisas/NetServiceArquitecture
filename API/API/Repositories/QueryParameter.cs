using System.Linq.Expressions;

namespace API.Repositories
{
    public class QueryParameter<T>
    {
        public Expression<Func<T, bool>> Where { get; set; }
        public Func<T, object> OrderBy { get; set; }
        public bool IsAscending { get; set; }
        public Func<IQueryable<T>, IQueryable<T>> Include { get; set; }
    }
}

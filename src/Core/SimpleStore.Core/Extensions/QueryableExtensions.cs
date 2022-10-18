using System.Linq.Expressions;

namespace SimpleStore.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int page, int pageSize)
            => queryable.Skip((page - 1) * pageSize).Take(pageSize);

        public static IQueryable<T> Filter<T, TProperty>(this IQueryable<T> queryable, Expression<Func<T, TProperty>> expression, object value)
            => throw new NotImplementedException();
    }
}

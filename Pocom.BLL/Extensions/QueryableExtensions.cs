using System.Linq;
using System.Linq.Expressions;

namespace Pocom.BLL.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool desc)
    {
        return desc ? source.OrderByDescending(ToLambda<T>(propertyName)) : source.OrderBy(ToLambda<T>(propertyName));
    }

    public static IQueryable<T> ThenBy<T>(this IQueryable<T> source, string propertyName, bool desc)
    {
        return desc ? ((IOrderedQueryable<T>)source).ThenByDescending(ToLambda<T>(propertyName)) : ((IOrderedQueryable<T>)source).ThenBy(ToLambda<T>(propertyName));
    }


    private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> source,int page, int size)
    {
        return source.Skip((page - 1) * size).Take(size);
    }
}
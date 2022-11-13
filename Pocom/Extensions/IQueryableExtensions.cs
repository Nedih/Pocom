using System.Linq;
using System.Linq.Expressions;

namespace Pocom.Api.Extensions;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool desc)
    {
        return desc ? source.OrderByDescending(ToLambda<T>(propertyName)) : source.OrderBy(ToLambda<T>(propertyName));
    }

    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName, bool desc)
    {
        return desc ? source.ThenByDescending(ToLambda<T>(propertyName)) : source.ThenBy(ToLambda<T>(propertyName));
    }


    private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }
}
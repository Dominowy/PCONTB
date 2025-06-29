using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace PCONTB.Panel.Application.Table
{
    public enum FilterOperation
{
    Equals,
    Contains,
    GreaterThan,
    LessThan
}

public static class QueryHelper
{
    public static IQueryable<TEntity> ApplyFilter<TEntity>(
        this IQueryable<TEntity> query,
        string propertyName,
        string value,
        FilterOperation operation = FilterOperation.Contains)
    {
        if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(value))
            return query;

        var param = Expression.Parameter(typeof(TEntity), "x");
        Expression property = BuildPropertyExpression(param, propertyName);

        Expression? condition = null;

        if (property.Type == typeof(string))
        {
            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
            var equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(StringComparison) })!;

            var toLowerProperty = Expression.Call(property, toLowerMethod);
            var loweredValue = Expression.Constant(value.ToLower());

            switch (operation)
            {
                case FilterOperation.Contains:
                    condition = Expression.Call(toLowerProperty, containsMethod, loweredValue);
                    break;
                case FilterOperation.Equals:
                    condition = Expression.Call(property, equalsMethod, Expression.Constant(value), Expression.Constant(StringComparison.OrdinalIgnoreCase));
                    break;
            }
        }
        else if (property.Type == typeof(int) || property.Type == typeof(int?))
        {
            if (int.TryParse(value, out var intValue))
            {
                var constant = Expression.Constant(intValue, typeof(int));

                switch (operation)
                {
                    case FilterOperation.Equals:
                        condition = Expression.Equal(property, constant);
                        break;
                    case FilterOperation.GreaterThan:
                        condition = Expression.GreaterThan(property, constant);
                        break;
                    case FilterOperation.LessThan:
                        condition = Expression.LessThan(property, constant);
                        break;
                }
            }
        }

        if (condition == null)
            return query;

        var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, param);
        return query.Where(lambda);
    }

    private static Expression BuildPropertyExpression(Expression param, string propertyName)
    {
        Expression property = param;
        foreach (var prop in propertyName.Split('.'))
        {
            property = Expression.PropertyOrField(property, prop);
        }
        return property;
    }

    public static IQueryable<TEntity> ApplyGlobalSearch<TEntity>(
        this IQueryable<TEntity> query,
        string search,
        params string[] propertyNames)
    {
        if (string.IsNullOrWhiteSpace(search) || propertyNames == null || propertyNames.Length == 0)
            return query;

        var param = Expression.Parameter(typeof(TEntity), "x");
        var loweredSearch = Expression.Constant(search.ToLower());

        MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;
        MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;

        Expression? orExpression = null;

        foreach (var propName in propertyNames)
        {
            Expression property = BuildPropertyExpression(param, propName);
            if (property.Type != typeof(string))
                continue; // Pomijamy nie-stringi

            var notNull = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
            var toLowerProperty = Expression.Call(property, toLowerMethod);
            var containsCall = Expression.Call(toLowerProperty, containsMethod, loweredSearch);

            var combined = Expression.AndAlso(notNull, containsCall);

            orExpression = orExpression == null ? combined : Expression.OrElse(orExpression, combined);
        }

        if (orExpression == null)
            return query;

        var lambda = Expression.Lambda<Func<TEntity, bool>>(orExpression, param);
        return query.Where(lambda);
    }

    public static IOrderedQueryable<TEntity> ApplyOrder<TEntity>(
        this IQueryable<TEntity> source,
        string propertyName,
        bool descending = false,
        bool thenBy = false,
        IOrderedQueryable<TEntity>? orderedQuery = null)
    {
        var param = Expression.Parameter(typeof(TEntity), "x");
        Expression property = BuildPropertyExpression(param, propertyName);
        var lambda = Expression.Lambda(property, param);

        string methodName;
        if (!thenBy)
            methodName = descending ? "OrderByDescending" : "OrderBy";
        else
            methodName = descending ? "ThenByDescending" : "ThenBy";

        var method = typeof(Queryable).GetMethods()
            .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
            .Single()
            .MakeGenericMethod(typeof(TEntity), property.Type);

        var result = method.Invoke(null, new object[] { orderedQuery ?? source, lambda })!;
        return (IOrderedQueryable<TEntity>)result;
    }

    public static IQueryable<TEntity> ApplyPagination<TEntity>(
        this IQueryable<TEntity> query,
        int page,
        int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}
}



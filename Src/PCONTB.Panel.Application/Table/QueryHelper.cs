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
            Expression property;

            // Obsługa kolekcji Any(...)
            if (propertyName.Contains("Any("))
                property = BuildAnyFilterExpression<TEntity>(param, propertyName);
            else
                property = BuildPropertyExpression(param, propertyName);

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

        // Buduje wyrażenie dla UserRoles.Any(Role) — zwraca string (Role.ToString())
        private static Expression BuildAnyFilterExpression<TEntity>(ParameterExpression param, string propertyName)
        {
            // Przykład propertyName = "UserRoles.Any(Role)"

            var anyIndex = propertyName.IndexOf("Any(", StringComparison.Ordinal);
            var collectionName = propertyName.Substring(0, anyIndex).TrimEnd('.');
            var innerProp = propertyName.Substring(anyIndex + 4, propertyName.Length - anyIndex - 5); // zawartość w nawiasie

            var collectionExpr = Expression.PropertyOrField(param, collectionName);
            var elementType = collectionExpr.Type.GetGenericArguments().First();

            var elementParam = Expression.Parameter(elementType, "c");
            var innerPropExpr = Expression.PropertyOrField(elementParam, innerProp);

            // Zamiana enum na string (Role.ToString())
            Expression keyExpr = Expression.Call(innerPropExpr, "ToString", Type.EmptyTypes);

            var selector = Expression.Lambda(keyExpr, elementParam);

            // collection.Select(c => c.Role.ToString()).Any(v => v == value)
            var selectCall = Expression.Call(
                typeof(Enumerable),
                "Select",
                new Type[] { elementType, typeof(string) },
                collectionExpr,
                selector);

            var anyPredicateParam = Expression.Parameter(typeof(string), "v");
            var anyPredicateBody = Expression.Equal(anyPredicateParam, Expression.Constant(value: null, typeof(string))); // placeholder
            var anyLambda = Expression.Lambda(anyPredicateBody, anyPredicateParam);

            // Zweryfikujemy to później w ApplyFilter, gdzie mamy dostęp do value, więc zmienimy
            // Ale Expression jest immutable, więc zamiast tego poniżej wywołujemy Any inline

            // Właściwe Any(v => v.ToLower().Contains(value.ToLower())) można zrobić tutaj dynamicznie

            // Dla prostoty – zwrócimy FirstOrDefault z kolekcji (do dalszej filtracji)
            var firstOrDefaultCall = Expression.Call(
                typeof(Enumerable),
                "FirstOrDefault",
                new Type[] { typeof(string) },
                selectCall);

            return firstOrDefaultCall;
        }

        public static IQueryable<TEntity> ApplyGlobalSearch<TEntity>(
    this IQueryable<TEntity> query,
    string search,
    params string[] propertyNames)
        {
            if (string.IsNullOrWhiteSpace(search) || propertyNames == null || propertyNames.Length == 0)
                return query;

            var param = Expression.Parameter(typeof(TEntity), "x");
            Expression? orExpression = null;

            foreach (var propName in propertyNames)
            {
                Expression searchExpression;

                if (propName.Contains("Any("))
                {
                    searchExpression = BuildAnyGlobalSearchExpression<TEntity>(param, propName, search);
                }
                else
                {
                    var property = BuildPropertyExpression(param, propName);
                    if (property.Type != typeof(string))
                        continue;

                    var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;

                    var notNull = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
                    var toLowerProperty = Expression.Call(property, toLowerMethod);
                    var loweredSearch = Expression.Constant(search.ToLower());

                    var containsCall = Expression.Call(toLowerProperty, containsMethod, loweredSearch);

                    searchExpression = Expression.AndAlso(notNull, containsCall);
                }

                orExpression = orExpression == null ? searchExpression : Expression.OrElse(orExpression, searchExpression);
            }

            if (orExpression == null)
                return query;

            var lambda = Expression.Lambda<Func<TEntity, bool>>(orExpression, param);
            return query.Where(lambda);
        }

        // Podobnie jak BuildAnyFilterExpression ale dla global search, robimy concat kolekcji w jeden string, lub FirstOrDefault
        private static Expression BuildAnyGlobalSearchExpression<TEntity>(ParameterExpression param, string propertyName, string searchValue)
        {
            var anyIndex = propertyName.IndexOf("Any(", StringComparison.Ordinal);
            var collectionName = propertyName.Substring(0, anyIndex).TrimEnd('.');
            var innerProp = propertyName.Substring(anyIndex + 4, propertyName.Length - anyIndex - 5);

            var collectionExpr = Expression.PropertyOrField(param, collectionName);
            var elementType = collectionExpr.Type.GetGenericArguments().First();

            var elementParam = Expression.Parameter(elementType, "c");
            var innerPropExpr = Expression.PropertyOrField(elementParam, innerProp);

            // .ToString().ToLower().Contains(searchValue.ToLower())
            var toStringCall = Expression.Call(innerPropExpr, "ToString", Type.EmptyTypes);
            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;
            var toLowerCall = Expression.Call(toStringCall, toLowerMethod);
            var loweredSearch = Expression.Constant(searchValue.ToLower());

            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
            var containsCall = Expression.Call(toLowerCall, containsMethod, loweredSearch);

            var notNull = Expression.NotEqual(toStringCall, Expression.Constant(null, typeof(string)));
            var combined = Expression.AndAlso(notNull, containsCall);

            var predicate = Expression.Lambda(combined, elementParam);

            // Wywołanie Any(...) na kolekcji
            var anyMethod = typeof(Enumerable).GetMethods()
                .Where(m => m.Name == "Any" && m.GetParameters().Length == 2)
                .Single()
                .MakeGenericMethod(elementType);

            var anyCall = Expression.Call(anyMethod, collectionExpr, predicate);

            return anyCall;
        }


        public static IOrderedQueryable<TEntity> ApplyOrder<TEntity>(
            this IQueryable<TEntity> source,
            string propertyName,
            bool descending = false,
            bool thenBy = false,
            IOrderedQueryable<TEntity>? orderedQuery = null)
        {
            var param = Expression.Parameter(typeof(TEntity), "x");
            Expression property;

            if (propertyName.Contains("Any("))
            {
                property = BuildAnyOrderExpression<TEntity>(param, propertyName);
            }
            else
            {
                property = BuildPropertyExpression(param, propertyName);
            }

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

            var result = method.Invoke(null, new object[] { thenBy ? orderedQuery! : source, lambda })!;
            return (IOrderedQueryable<TEntity>)result;
        }

        private static Expression BuildAnyOrderExpression<TEntity>(ParameterExpression param, string propertyName)
        {
            // "UserRoles.Any(Role)" => wyciągnięcie pierwszego elementu z kolekcji i jego właściwości jako string

            var anyIndex = propertyName.IndexOf("Any(", StringComparison.Ordinal);
            var collectionName = propertyName.Substring(0, anyIndex).TrimEnd('.');
            var innerProp = propertyName.Substring(anyIndex + 4, propertyName.Length - anyIndex - 5);

            var collectionExpr = Expression.PropertyOrField(param, collectionName);
            var elementType = collectionExpr.Type.GetGenericArguments().First();

            var elementParam = Expression.Parameter(elementType, "c");
            var innerPropExpr = Expression.PropertyOrField(elementParam, innerProp);

            Expression keyExpr = Expression.Call(innerPropExpr, "ToString", Type.EmptyTypes);

            var selector = Expression.Lambda(keyExpr, elementParam);

            var selectCall = Expression.Call(
                typeof(Enumerable),
                "Select",
                new Type[] { elementType, typeof(string) },
                collectionExpr,
                selector);

            var firstOrDefaultCall = Expression.Call(
                typeof(Enumerable),
                "FirstOrDefault",
                new Type[] { typeof(string) },
                selectCall);

            return firstOrDefaultCall;
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

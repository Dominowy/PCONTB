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
            Expression property = BuildSearchExpression(param, propertyName, value);

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
            else if (property.Type == typeof(bool))
            {
                condition = condition == null ? property : Expression.OrElse(condition, property);
            }

            if (condition == null)
                return query;

            var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, param);
            return query.Where(lambda);
        }

        private static Expression BuildPropertyExpression(Expression param, string propertyPath)
        {
            Expression current = param;
            foreach (var prop in propertyPath.Split('.'))
            {
                var propertyInfo = current.Type.GetProperty(prop);
                if (propertyInfo == null)
                    throw new ArgumentException($"Property '{prop}' not found on type '{current.Type.Name}'");

                if (IsEnumerableButNotString(propertyInfo.PropertyType))
                {
                    // Jeśli to kolekcja, wyciągamy FirstOrDefault()
                    var elementType = propertyInfo.PropertyType.GetGenericArguments().First();
                    var firstMethod = typeof(Enumerable)
                        .GetMethods()
                        .First(m => m.Name == "FirstOrDefault" && m.GetParameters().Length == 1)
                        .MakeGenericMethod(elementType);

                    var collection = Expression.PropertyOrField(current, prop);
                    current = Expression.Call(firstMethod, collection);
                }
                else
                {
                    current = Expression.PropertyOrField(current, prop);
                }
            }
            return current;
        }

        private static Expression BuildSearchExpression(Expression param, string propertyName, string search)
        {
            var parts = propertyName.Split('.');
            Expression property = param;

            for (int i = 0; i < parts.Length; i++)
            {
                var prop = parts[i];
                var propInfo = property.Type.GetProperty(prop);

                if (propInfo == null)
                    throw new ArgumentException($"Property '{prop}' not found on type '{property.Type.Name}'");

                if (IsEnumerableButNotString(propInfo.PropertyType))
                {
                    // Kolekcja -> musimy zbudować Any() z predykatem na elementach

                    if (i + 1 >= parts.Length)
                        throw new ArgumentException("Expected property name after collection property");

                    var elementType = propInfo.PropertyType.GetGenericArguments()[0];
                    var elementParam = Expression.Parameter(elementType, "e");
                    var innerPropName = parts[i + 1];

                    // Budujemy predykat dla elementu kolekcji, który zwraca bool (np. contains)
                    var predicate = BuildContainsPredicate(elementParam, innerPropName, search);

                    // Metoda Any<T>(IEnumerable<T>, Func<T,bool>)
                    var anyMethod = typeof(Enumerable).GetMethods()
                        .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                        .MakeGenericMethod(elementType);

                    // property kolekcji (np. x.UserRoles)
                    var collectionProperty = Expression.PropertyOrField(property, prop);

                    // wywołanie Any(collectionProperty, predicate)
                    property = Expression.Call(anyMethod, collectionProperty, predicate);

                    i++; // przeskakujemy nazwę właściwości elementu, bo już ją przerobiliśmy
                }
                else
                {
                    // Zwykła właściwość
                    property = Expression.PropertyOrField(property, prop);
                }
            }

            return property;
        }

        private static LambdaExpression BuildContainsPredicate(ParameterExpression param, string propertyName, string search)
        {
            var property = Expression.PropertyOrField(param, propertyName);

            if (property.Type == typeof(string))
            {
                // Dla string — zawiera tekst (Contains)
                var notNull = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;

                var toLowerProperty = Expression.Call(property, toLowerMethod);
                var loweredSearch = Expression.Constant(search.ToLower());

                var containsCall = Expression.Call(toLowerProperty, containsMethod, loweredSearch);

                var body = Expression.AndAlso(notNull, containsCall);
                return Expression.Lambda(body, param);
            }
            else if (property.Type.IsEnum)
            {
                // Dla enum: zamieniamy enum na string (ToString()), porównujemy z search po lowercase
                var toStringMethod = typeof(object).GetMethod("ToString", Type.EmptyTypes)!;
                var toStringCall = Expression.Call(property, toStringMethod);

                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;
                var toLowerCall = Expression.Call(toStringCall, toLowerMethod);

                var loweredSearch = Expression.Constant(search.ToLower());

                var equalsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;

                // Możesz też użyć Contains, jeśli chcesz partial match:
                // var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
                // var containsCall = Expression.Call(toLowerCall, containsMethod, loweredSearch);

                var equalsCall = Expression.Call(toLowerCall, equalsMethod, loweredSearch);

                return Expression.Lambda(equalsCall, param);
            }
            else
            {
                throw new ArgumentException($"Property '{propertyName}' is neither string nor enum.");
            }
        }

        private static bool IsEnumerableButNotString(Type type)
        {
            return typeof(System.Collections.IEnumerable).IsAssignableFrom(type) && type != typeof(string);
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
                Expression propertyExpression;

                try
                {
                    propertyExpression = BuildSearchExpression(param, propName, search);
                }
                catch (ArgumentException)
                {
                    // Jeśli property nie istnieje lub inny błąd, pomiń
                    continue;
                }

                // propertyExpression może być typu bool (np. Any), lub string (gdy np. ostatnia właściwość)
                if (propertyExpression.Type == typeof(bool))
                {
                    orExpression = orExpression == null ? propertyExpression : Expression.OrElse(orExpression, propertyExpression);
                }
                else if (propertyExpression.Type == typeof(string))
                {
                    // Budujemy warunek Contains jak wcześniej (dla string)

                    var notNull = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(string)));
                    var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes)!;
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) })!;
                    var loweredSearch = Expression.Constant(search.ToLower());

                    var toLowerProperty = Expression.Call(propertyExpression, toLowerMethod);
                    var containsCall = Expression.Call(toLowerProperty, containsMethod, loweredSearch);
                    var combined = Expression.AndAlso(notNull, containsCall);

                    orExpression = orExpression == null ? combined : Expression.OrElse(orExpression, combined);
                }
                else
                {
                    // Inny typ - pomijamy
                    continue;
                }
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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ComboRox.Core.Utilities;
using ComboRox.Models;

namespace ComboRox.Core.Filters
{
    public static class FiltersExpressionBuilder
    {
        private static Dictionary<string, MethodInfo> cachedPropertyTypes = new Dictionary<string, MethodInfo>();

        private static BinaryExpression ConcatenateAndOrExpressions(
            IEnumerable<Filter> filters,
            ParameterExpression itemParameter,
            Type filterableClassType)
        {
            BinaryExpression where = null;
            MemberExpression property;
            BinaryExpression filterExpression;

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    property = RecursivelyPropertyInfoGetter.CreatePropertyExpression(itemParameter, filter.PropertyName);

                    filter.Value = ChangeValueType(filterableClassType, filter.PropertyName, filter.Value);

                    filterExpression = CompareExpressionByOperator(
                                filter.Operator,
                                property,
                                Expression.Constant(filter.Value));

                    if (filter.OrFilters != null && filter.OrFilters.Count > 0)
                    {
                        BinaryExpression orFiltersExpressions = ConcatenateAndOrExpressions(filter.OrFilters, itemParameter, filterableClassType);
                        if (orFiltersExpressions != null)
                        {
                            filterExpression = Expression.OrElse(filterExpression, orFiltersExpressions);
                        }
                    }

                    if (where != null)
                    {
                        where = Expression.AndAlso(where, filterExpression);
                    }
                    else
                    {
                        where = filterExpression;
                    }
                }
            }

            return where;
        }

        private static object ChangeValueType(Type filterableClassType, string propertyName, object value)
        {
            if ((value as string) != null)
            {
                var parseMethodInfo = GetMethodInfoFromCache(filterableClassType, propertyName);
                if (parseMethodInfo != null)
                {
                    value = parseMethodInfo.Invoke(null, new[] { value });
                }

                return value;
            }

            return value;
        }

        private static MethodInfo GetMethodInfoFromCache(Type filterableClassType, string propertyName)
        {
            string propertyPath = string.Format("{0}.{1}", filterableClassType.Assembly.FullName, propertyName);

            if (cachedPropertyTypes.ContainsKey(propertyPath))
            {
                return cachedPropertyTypes[propertyPath];
            }

            MethodInfo methodInfo = RecursivelyPropertyInfoGetter
                .GetPropertyTypeInfoRecursively(filterableClassType, propertyName)
                .GetMethod("Parse");

            cachedPropertyTypes.Add(propertyPath, methodInfo);

            return methodInfo;
        }

        private static BinaryExpression CompareExpressionByOperator(Operator op, MemberExpression property, ConstantExpression filterValue)
        {
            switch (op)
            {
                case Operator.Contains:
                    return null; //TODO

                case Operator.GreaterThan:
                    return Expression.GreaterThan(property, filterValue);

                case Operator.LessThan:
                    return Expression.LessThan(property, filterValue);

                case Operator.NotEquals:
                    return Expression.NotEqual(property, filterValue);

                default:
                    return Expression.Equal(property, filterValue);
            }
        }

        public static Expression<Func<TType, bool>> Create<TType>(List<Filter> filters) where TType : class
        {
            if (filters != null && filters.Count > 0)
            {
                Type filterableClassType = typeof(TType);
                var itemParameter = Expression.Parameter(filterableClassType, "dataItem");

                return Expression.Lambda<Func<TType, bool>>(ConcatenateAndOrExpressions(filters, itemParameter, filterableClassType), itemParameter);
            }

            return null;
        }
    }
}
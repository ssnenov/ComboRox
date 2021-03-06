﻿using ComboRox.Core.Utilities;
using ComboRox.Core.Utilities.SimpleGuard;
using ComboRox.Models;
using ComboRox.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ComboRox.Core.Filters
{
    public static class FiltersExpressionFactory
    {
        public static Expression<Func<TType, bool>> Create<TType>(ICollection<Filter> filters) where TType : class
        {
            if (filters != null && filters.Count > 0)
            {
                Type filterableClassType = typeof(TType);
                var itemParameter = Expression.Parameter(filterableClassType, "dataItem");

                return Expression.Lambda<Func<TType, bool>>(ConcatenateAndOrExpressions(filters, itemParameter, filterableClassType), itemParameter);
            }

            return null;
        }

        private static Expression ConcatenateAndOrExpressions(
        IEnumerable<Filter> filters,
        ParameterExpression itemParameter,
        Type filterableClassType)
        {
            Expression where = null;
            MemberExpression property;
            Expression filterExpression;

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    property = RecursivelyPropertyInfoGetter.CreatePropertyExpression(itemParameter, filter.PropertyName);

                    filter.Value = ValueParser.ParseValueToPropertyType(property, filter.Value);

                    filterExpression = CompareExpressionByOperator(
                                filter.Operator,
                                property,
                                Expression.Constant(filter.Value));

                    if (filter.OrFilters != null && filter.OrFilters.Count > 0)
                    {
                        Expression filtersExpressions = ConcatenateAndOrExpressions(filter.OrFilters, itemParameter, filterableClassType);
                        if (filtersExpressions != null)
                        {
                            filterExpression = Expression.OrElse(filterExpression, filtersExpressions);
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

        private static Expression CompareExpressionByOperator(Operator op, MemberExpression property, ConstantExpression filterValue)
        {
            switch (op)
            {
                case Operator.Contains: // TODO: Test this functionality
                    Guard.Requires(
                        property.Type == typeof(string) && filterValue.Type == typeof(string),
                        string.Format(@"You cannot use ""Contains"" operator for types {0} and {1}", property.Type, filterValue.Type));

                    return Expression.Call(property, typeof(string).GetMethod("Contains"), filterValue);

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
    }
}
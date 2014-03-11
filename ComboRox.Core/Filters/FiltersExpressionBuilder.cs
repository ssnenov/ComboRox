using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ComboRox.Models;

namespace ComboRox.Core.Filters
{
    public static class FiltersExpressionBuilder
    {
        private static BinaryExpression ConcatenateAndOrExpressions(IEnumerable<Filter> filters, ParameterExpression itemParameter)
        {
            BinaryExpression where = null;
            MemberExpression property;
            BinaryExpression filterExpression;

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    property = Expression.PropertyOrField(itemParameter, filter.PropertyName); 
                    //TODO: Create Property parser that parses the string and select inner propperties

                    filterExpression = CompareExpressionByOperator(
                                filter.Operator,
                                property,
                                Expression.Constant(filter.Value));

                    if (filter.OrFilters != null && filter.OrFilters.Count > 0)
                    {
                        BinaryExpression orFiltersExpressions = ConcatenateAndOrExpressions(filter.OrFilters, itemParameter);
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
            var itemParameter = Expression.Parameter(typeof(TType), "dataItem");
            return Expression.Lambda<Func<TType, bool>>(ConcatenateAndOrExpressions(filters, itemParameter), itemParameter);
        }
    }
}
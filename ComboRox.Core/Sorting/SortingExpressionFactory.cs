using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ComboRox.Core.Utilities;
using ComboRox.Core.Utilities.Guard;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core.Sorting
{
    public class SortingExpressionFactory
    {
        private static ParameterExpression itemParameter;

        public static IEnumerable<TType> Create<TType>(
            IEnumerable<TType> collection,
            IList<SortingObject> ascendingSortings,
            IList<SortingObject> descendingSortings)
            where TType : class
        {
            Guard.Requires(ascendingSortings, "sortingSettings").IsNotNull();
            Guard.Requires(ascendingSortings, "sortingSettings").IsNotNull();

            itemParameter = Expression.Parameter(typeof(TType), "item");
            IOrderedEnumerable<TType> result = null;
            MemberExpression propertyAccessor;

            if (ascendingSortings.Count > 0)
            {
                propertyAccessor = RecursivelyPropertyInfoGetter.CreatePropertyExpression(itemParameter, ascendingSortings[0].Prop);

                result = collection.OrderBy(Expression.Lambda<Func<TType, object>>(propertyAccessor, itemParameter).Compile());

                for (int i = 1; i < ascendingSortings.Count; i++)
                {
                    propertyAccessor = RecursivelyPropertyInfoGetter.CreatePropertyExpression(itemParameter, ascendingSortings[i].Prop);
                    result = ApplySortExpressionAscending(result, propertyAccessor);
                }
            }

            if (descendingSortings.Count > 0)
            {
                propertyAccessor = RecursivelyPropertyInfoGetter.CreatePropertyExpression(itemParameter, descendingSortings[0].Prop);

                result = (result ?? collection).OrderBy(Expression.Lambda<Func<TType, object>>(propertyAccessor, itemParameter).Compile());

                for (int i = 1; i < descendingSortings.Count; i++)
                {
                    propertyAccessor = RecursivelyPropertyInfoGetter.CreatePropertyExpression(itemParameter, descendingSortings[i].Prop);
                    result = ApplySortExpressionDescending(result, propertyAccessor);
                }
            }

            return result;
        }

        private static IOrderedEnumerable<TType> ApplySortExpressionAscending<TType>(IOrderedEnumerable<TType> collection, MemberExpression propertyAccessor)
        {
            return collection.ThenBy(Expression.Lambda<Func<TType, object>>(propertyAccessor, itemParameter).Compile());
        }

        private static IOrderedEnumerable<TType> ApplySortExpressionDescending<TType>(IOrderedEnumerable<TType> collection, MemberExpression propertyAccessor)
        {
            return collection.ThenByDescending(Expression.Lambda<Func<TType, object>>(propertyAccessor, itemParameter).Compile());
        }
    }
}
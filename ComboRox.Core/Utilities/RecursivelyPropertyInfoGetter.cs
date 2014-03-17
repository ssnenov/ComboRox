using System;
using System.Linq.Expressions;

namespace ComboRox.Core.Utilities
{
    internal class RecursivelyPropertyInfoGetter
    {
        internal static MemberExpression CreatePropertyExpression(ParameterExpression itemParameter, string propertyName)
        {
            var properties = propertyName.Split('.');
            MemberExpression result = Expression.Property(itemParameter, properties[0]);

            for (int i = 1; i < properties.Length; i++)
            {
                result = Expression.Property(result, properties[i]);
            }

            return result;
        }

        internal static Type GetPropertyTypeInfoRecursively(Type filterableClassType, string propertyName)
        {
            var properties = propertyName.Split('.');
            Type result = filterableClassType.GetProperty(properties[0]).PropertyType;

            for (int i = 1; i < properties.Length; i++)
            {
                result = result.GetProperty(properties[i]).PropertyType;
            }

            return result;
        }
    }
}
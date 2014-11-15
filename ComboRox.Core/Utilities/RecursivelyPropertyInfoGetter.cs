using System;
using System.Linq.Expressions;
using ComboRox.Core.Utilities.SimpleGuard;

namespace ComboRox.Core.Utilities
{
    internal static class RecursivelyPropertyInfoGetter
    {
        internal static MemberExpression CreatePropertyExpression(ParameterExpression itemParameter, string propertyName)
        {
			SimpleGuard.Guard.Requires(propertyName, "propertyName").IsNotNullOrEmpty();

            var properties = propertyName.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            SimpleGuard.Guard.Requires(properties, "properties").IsNotEmpty();

            MemberExpression result = Expression.Property(itemParameter, properties[0]);

            for (int i = 1; i < properties.Length; ++i)
            {
                result = Expression.Property(result, properties[i]);
            }

            return result;
        }
    }
}
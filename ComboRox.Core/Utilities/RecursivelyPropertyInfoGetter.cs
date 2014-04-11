using System.Linq.Expressions;
using ComboRox.Core.Utilities.Guard;

namespace ComboRox.Core.Utilities
{
    internal class RecursivelyPropertyInfoGetter
    {
        internal static MemberExpression CreatePropertyExpression(ParameterExpression itemParameter, string propertyName)
        {
            var properties = propertyName.Split('.');
            Guard.Guard.Requires(properties, "properties").IsNotEmpty();

            MemberExpression result = Expression.Property(itemParameter, properties[0]);

            for (int i = 1; i < properties.Length; i++)
            {
                result = Expression.Property(result, properties[i]);
            }

            return result;
        }
    }
}
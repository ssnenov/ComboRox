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
    }
}
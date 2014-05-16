using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ComboRox.Core.Utilities
{
    internal class ValueParser
    {
        private static readonly Dictionary<string, Func<string, object>> CachedLambdas = new Dictionary<string, Func<string, object>>(10); 

        internal static object ParseValueToPropertyType(Type classType, Expression propertyExpression, object value)
        {
            var valueAsString = value as string;
            if (propertyExpression.Type == typeof(string) || valueAsString == null)
            {
                return value;
            }

            if (valueAsString == "null")
            {
                return null;
            }

            var parseMethod = GetLambdaFromCache(propertyExpression);
            
            return parseMethod(valueAsString);
        }

        private static Func<string, object> GetLambdaFromCache(Expression propertyExpression)
        {
            string typeAssemblyFullName = propertyExpression.Type.FullName;
            if (CachedLambdas.ContainsKey(typeAssemblyFullName))
            {
                return CachedLambdas[typeAssemblyFullName];
            }

            var parseMethodParameters = new[] { Expression.Parameter(typeof(string), "value") };

            var callExpr = Expression.Call(propertyExpression.Type.GetMethod("Parse", new[] { typeof(string) }), parseMethodParameters);

            var downcastToObject = Expression.Convert(callExpr, typeof(object));

            var result = Expression.Lambda<Func<string, object>>(downcastToObject, parseMethodParameters).Compile();

            CachedLambdas.Add(typeAssemblyFullName, result);

            return result;
        }
    }
}
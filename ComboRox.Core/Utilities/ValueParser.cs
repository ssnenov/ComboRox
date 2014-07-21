using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Caching;

namespace ComboRox.Core.Utilities
{
    internal class ValueParser
    {
		private const string CacheKeyPrefix = "Types_";

        internal static object ParseValueToPropertyType(Expression propertyExpression, object value)
        {
			SimpleGuard.Guard.Requires(propertyExpression, "propertyExpression").IsNotNull();
			SimpleGuard.Guard.Requires(value, "value").IsNotNull();

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
			Func<string, object> lambda = MemoryCache.Default.Get(string.Format(
				"{0}{1}",
				ValueParser.CacheKeyPrefix,
				typeAssemblyFullName)) as Func<string, object>;
			
            if (lambda != null)
            {
                return lambda;
            }

            var parseMethodParameters = new[] { Expression.Parameter(typeof(string), "value") };

            var callExpr = Expression.Call(propertyExpression.Type.GetMethod("Parse", new[] { typeof(string) }), parseMethodParameters);

            var downcastToObject = Expression.Convert(callExpr, typeof(object));

            var result = Expression.Lambda<Func<string, object>>(downcastToObject, parseMethodParameters).Compile();

			MemoryCache.Default.Set(
				string.Format("{0}{1}", ValueParser.CacheKeyPrefix, typeAssemblyFullName),
				result,
				new CacheItemPolicy());

            return result;
        }
    }
}
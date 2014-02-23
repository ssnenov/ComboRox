using System.Collections.Generic;
using System.Linq;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core
{
    public class ComboRox
    {
        public static IQueryable<TType> Magic<TType>(IQueryable<TType> query, ComboRequestJson requestJson) where TType : class 
        {
            var modulesManager = new ModulesManager();

            return modulesManager.ApplyModulesExpressions(query, requestJson);
        }

        public static IEnumerable<TType> Magic<TType>(IEnumerable<TType> collection, ComboRequestJson requestJson) where TType : class
        {
            var modulesManager = new ModulesManager();

            return modulesManager.ApplyModulesExpressions(collection, requestJson);
        }
    }
}

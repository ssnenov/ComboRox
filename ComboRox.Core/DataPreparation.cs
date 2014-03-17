using System.Collections.Generic;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core
{
    public class DataPreparation
    {
        public static IResultData Prepare<TType>(IEnumerable<TType> collection, ComboRequestJson requestJson) where TType : class
        {
            var modulesManager = new ModulesManager();

            return modulesManager.ApplyModulesExpressions(collection, requestJson);
        }
    }
}
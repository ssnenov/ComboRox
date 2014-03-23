using System.Collections.Generic;
using System.Linq;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core.Filters
{
    public class FiltersModule : IModule
    {
        private const string Name = "Filters";

        public string ModuleName
        {
            get { return Name; }
        }

        public IModulesSettings Initialize(IComboRequestJson requestJson, IModulesSettings modulesSettings)
        {
            modulesSettings.Filters = new FiltersFactory().Create(requestJson.Filters);

            return modulesSettings;
        }

        public IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, IModulesSettings modulesSettings) where TType : class
        {
            var expressionToApply = FiltersExpressionFactory.Create<TType>(modulesSettings.Filters);
            
            if (expressionToApply == null)
            {
                return collection;
            }

            var query = collection as IQueryable<TType>;

            if (query != null)
            {
                return query.Where(expressionToApply);
            }

            return collection.Where(expressionToApply.Compile());
        }

        public IResultData ConstructResult<TType>(IEnumerable<TType> collection, IResultData resultObject)
        {
            resultObject.Data = collection;

            return resultObject;
        }
    }
}
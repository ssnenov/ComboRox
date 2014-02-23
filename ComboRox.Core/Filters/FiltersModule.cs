using System.Collections.Generic;
using System.Linq;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core.Filters
{
    public class FiltersModule : IModule
    {
        public IModulesSettings Initialize(IComboRequestJson requestJson, IModulesSettings request)
        {
            request.Filters = new FiltersBuilder().Create(requestJson.Filters);

            return request;
        }

        public IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, IModulesSettings request) where TType : class
        {
            var expressionToApply = FiltersExpressionBuilder.Create<TType>(request.Filters);

            return collection.Where(expressionToApply.Compile());
        }

        public IQueryable<TType> ApplyExpression<TType>(IQueryable<TType> collection, IModulesSettings request) where TType : class
        {
            var expressionToApply = FiltersExpressionBuilder.Create<TType>(request.Filters);

            return collection.Where(expressionToApply);
        }

        public IResultData ConstructResult<TType>(IEnumerable<TType> collection, IResultData resultObject)
        {
            resultObject.Data = collection;

            return resultObject;
        }
    }
}
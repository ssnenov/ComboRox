using System.Collections.Generic;
using System.Linq;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core.Pagination
{
    public class PaginationModule : IModule
    {
        private const string _moduleName = "Pagination";

        public string ModuleName
        {
            get
            {
                return _moduleName;
            }
        }

        public IModulesSettings Initialize(IComboRequestJson requestJson, IModulesSettings modulesSettings)
        {
            if (requestJson.Pagination == null)
            {
                return modulesSettings;
            }

            modulesSettings.Pagination = requestJson.Pagination;

            return modulesSettings;
        }

        public IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, IModulesSettings modulesSettings) where TType : class
        {
            if (modulesSettings.Pagination == null)
            {
                return collection;
            }

            int pageSize = modulesSettings.Pagination.PageSize;

            return collection.Skip((modulesSettings.Pagination.Page - 1) * pageSize)
                             .Take(pageSize);
        }

        public IResultData ConstructResult<TType>(IEnumerable<TType> collection, IResultData resultObject)
        {
            resultObject.Data = collection;
            resultObject.Total = collection.Count();

            return resultObject;
        }
    }
}
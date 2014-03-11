using System.Collections.Generic;
using System.Linq;
using ComboRox.Models;

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

        public IModulesSettings Initialize(Models.JsonObjects.IComboRequestJson requestJson, IModulesSettings request)
        {
            if (requestJson.Pagination == null)
            {
                return request;
            }

            request.Pagination = requestJson.Pagination;

            return request;
        }

        public IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, IModulesSettings request) where TType : class
        {
            if (request.Pagination == null)
            {
                return collection;
            }

            int pageSize = request.Pagination.PageSize;

            return collection.Skip((request.Pagination.Page - 1) * pageSize)
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComboRox.Core.UnitTests.ModulesManager
{
    class CustomTestingModule : IModule
    {
        private const string _moduleName = "CustomTestingModule";

        public Models.IModulesSettings Initialize(Models.JsonObjects.IComboRequestJson requestJson, Models.IModulesSettings request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, Models.IModulesSettings request) where TType : class
        {
            throw new NotImplementedException();
        }

        public IQueryable<TType> ApplyExpression<TType>(IQueryable<TType> collection, Models.IModulesSettings request) where TType : class
        {
            throw new NotImplementedException();
        }

        public Models.IResultData ConstructResult<TType>(IEnumerable<TType> collection, Models.IResultData resultObject)
        {
            throw new NotImplementedException();
        }

        string IModule.ModuleName
        {
            get { return _moduleName; }
        }
    }
}

using ComboRox.Core.Utilities.Guard;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;
using System;
using System.Collections.Generic;

namespace ComboRox.Core.Sorting
{
    public class SortingModule : IModule
    {
        private const string Name = "Sorting";

        public string ModuleName
        {
            get { return Name; }
        }

        public IModulesSettings Initialize(IComboRequestJson requestJson, IModulesSettings modulesSettings)
        {
            Guard.Requires(requestJson, "requestJson")
                .IsNotNull();

            modulesSettings.Sorting = requestJson.Sorting;

            return modulesSettings;
        }

        public IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, IModulesSettings request) where TType : class
        {
            throw new NotImplementedException();
        }

        public IResultData ConstructResult<TType>(IEnumerable<TType> collection, IResultData resultObject)
        {
            throw new NotImplementedException();
        }
    }
}
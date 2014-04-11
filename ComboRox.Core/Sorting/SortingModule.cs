using ComboRox.Core.Utilities.Guard;
using ComboRox.Models;
using ComboRox.Models.Enums;
using ComboRox.Models.JsonObjects;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, IModulesSettings modulesSettings) where TType : class
        {
            var ascendingSorts = modulesSettings.Sorting.Where(x => x.Direction == SortingDirection.Asc).ToList();
            var descendingSorts = modulesSettings.Sorting.Where(x => x.Direction == SortingDirection.Desc).ToList();

            IEnumerable<TType> result = SortingExpressionFactory.Create(collection, ascendingSorts, descendingSorts);

            return result;
        }

        public IResultData ConstructResult<TType>(IEnumerable<TType> collection, IResultData resultObject)
        {
            throw new NotImplementedException();
        }
    }
}
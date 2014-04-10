using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;
using ComboRox.Core.Utilities.Guard;

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
using System.Collections.Generic;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core
{
    public interface IModule
    {
        string ModuleName { get; }

        IModulesSettings Initialize(IComboRequestJson requestJson, IModulesSettings modulesSettings);

        IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, IModulesSettings modulesSettings) where TType : class;

        IResultData ConstructResult<TType>(IEnumerable<TType> collection, IResultData resultObject);
    }
}
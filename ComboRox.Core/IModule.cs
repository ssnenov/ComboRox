using System.Collections.Generic;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core
{
    public interface IModule
    {
        IModulesSettings Initialize(IComboRequestJson requestJson, IModulesSettings request);

        IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, IModulesSettings request) where TType : class;

        IResultData ConstructResult<TType>(IEnumerable<TType> collection, IResultData resultObject);
    }
}
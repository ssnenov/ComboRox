using System.Collections.Generic;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core
{
    public interface IModule
    {
        /// <summary>
        /// Gets the name of the module
        /// </summary>
        string ModuleName { get; }

        /// <summary>
        /// This is the first method of the module pipeline where the module is initializing it's settings,
        /// properties and e.t.c
        /// </summary>
        /// <param name="requestJson">The raw settings object that comes from the request</param>
        /// <param name="modulesSettings">Object that represents setting of the modules</param>
        /// <returns>Modified modulesSettings object</returns>
        IModulesSettings Initialize(IComboRequestJson requestJson, IModulesSettings modulesSettings);

        /// <summary>
        /// Modifies the collection
        /// </summary>
        /// <typeparam name="TType">The type of collection's object</typeparam>
        /// <param name="collection">It will be applied some filtrations, sorting and e.t.c on this collection</param>
        /// <param name="modulesSettings">The settings of the module</param>
        /// <returns>Modified collection (e.g filtered)</returns>
        IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, IModulesSettings modulesSettings) where TType : class;

        /// <summary>
        /// Generates/Returns new <see cref="IResultData"/>
        /// </summary>
        /// <typeparam name="TType">The type of collection's object</typeparam>
        /// <param name="collection">Already modified collection</param>
        /// <param name="resultObject">The result of preformed actions</param>
        /// <returns><see cref="IResultData"/></returns>
        IResultData ConstructResult<TType>(IEnumerable<TType> collection, IResultData resultObject);
    }
}
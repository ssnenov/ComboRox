﻿using System.Collections.Generic;
using System.Linq;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core.Filters
{
    public class FiltersModule : IModule
    {
        public IModulesSettings Initialize(IComboRequestJson requestJson, IModulesSettings modulesSettings)
        {
            modulesSettings.Filters = new FiltersBuilder().Create(requestJson.Filters);

            return modulesSettings;
        }

        public IEnumerable<TType> ApplyExpression<TType>(IEnumerable<TType> collection, IModulesSettings modulesSettings) where TType : class
        {
            var expressionToApply = FiltersExpressionBuilder.Create<TType>(modulesSettings.Filters);

            return collection.Where(expressionToApply.Compile());
        }

        public IQueryable<TType> ApplyExpression<TType>(IQueryable<TType> collection, IModulesSettings modulesSettings) where TType : class
        {
            var expressionToApply = FiltersExpressionBuilder.Create<TType>(modulesSettings.Filters);

            return collection.Where(expressionToApply);
        }

        public IResultData ConstructResult<TType>(IEnumerable<TType> collection, IResultData resultObject)
        {
            resultObject.Data = collection;

            return resultObject;
        }
    }
}
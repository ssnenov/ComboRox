﻿using System.Collections.Generic;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;
using System.Threading.Tasks;

namespace ComboRox.Core
{
    public static class DataPreparation
    {
        public static IResultData Prepare<TType>(IEnumerable<TType> collection, IComboRequestJson requestJson) where TType : class
        {
            return ModulesManager.GetManager.ApplyModulesExpressions(collection, requestJson);
        }

        public static async Task<IResultData> PrepareAsync<TType>(IEnumerable<TType> collection, IComboRequestJson requestJson) where TType : class
        {
            var backgroundTask =
                Task<IResultData>.Factory.StartNew(() => 
                    ModulesManager.GetManager.ApplyModulesExpressions(collection, requestJson));

            return await backgroundTask;
        }
    }
}
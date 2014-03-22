using System;
using System.Collections.Generic;
using ComboRox.Core.Utilities;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core
{
    public class ModulesManager
    {
        private static ModulesManager instance;

        public ModulesManager(List<IModule> additionalModules = null)
        {
            this.Modules = new Dictionary<string, IModule>(BuiltInModules.GetModules());

            if (additionalModules != null)
            {
                additionalModules.ForEach(x => this.Modules.Add(x.ModuleName, x));
            }
        }

        public event Action<IResultData> DataPrepared;

        public static ModulesManager GetManager
        {
            get { return instance ?? (instance = new ModulesManager()); }
        }

        public Dictionary<string, IModule> Modules { get; private set; }

        public IModulesSettings GetModulesSettings(IComboRequestJson comboRequestJson, IModulesSettings modulesSettingsObj)
        {
            if (comboRequestJson == null)
            {
                throw new ArgumentNullException("comboRequestJson");
            }

            if (modulesSettingsObj == null)
            {
                modulesSettingsObj = new ModulesSettings();
            }

            foreach (var module in this.Modules.Values)
            {
                modulesSettingsObj = module.Initialize(comboRequestJson, modulesSettingsObj);
            }

            return modulesSettingsObj;
        }

        public IResultData ApplyModulesExpressions<TType>(
            IEnumerable<TType> collection,
            ComboRequestJson comboRequestJson,
            IModulesSettings modulesSettings = null)
            where TType : class
        {
            IModulesSettings settings = this.GetModulesSettings(comboRequestJson, modulesSettings);
            IResultData resultObject = new ResultData();

            foreach (var module in this.Modules.Values)
            {
                collection = module.ApplyExpression(collection, settings);
                resultObject = module.ConstructResult(collection, resultObject);
            }

            this.DataPrepared.TriggerEvent(resultObject);

            return resultObject;
        }
    }
}
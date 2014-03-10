using System;
using System.Collections.Generic;
using ComboRox.Models;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Core
{
    public class ModulesManager
    {
        private static ModulesManager _instance;

        public static ModulesManager GetManager
        {
            get { return _instance ?? (_instance = new ModulesManager()); }
        }

        public Dictionary<string, IModule> Modules { get; private set; }

        public ModulesManager(List<IModule> additionalModules = null)
        {
            this.Modules = new Dictionary<string, IModule>(BuiltInModules.Modules);

            if (additionalModules != null)
            {
                additionalModules.ForEach(x => this.Modules.Add(x.ModuleName, x));
            }
        }

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

            foreach (var module in Modules.Values)
            {
                module.Initialize(comboRequestJson, modulesSettingsObj);
            }

            return modulesSettingsObj;
        }

        public IResultData ApplyModulesExpressions<TType>(IEnumerable<TType> collection,
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

            return resultObject;
        }
    }
}
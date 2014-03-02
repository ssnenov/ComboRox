using System;
using System.Collections.Generic;
using ComboRox.Core.Filters;
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

        public LinkedList<IModule> Modules { get; private set; }

        public ModulesManager(List<IModule> additionalModules = null)
        {
            IModule filtersModule = new FiltersModule();

            this.Modules = new LinkedList<IModule>();

            Modules.AddLast(filtersModule);

            if (additionalModules != null)
            {
                additionalModules.ForEach(x => this.Modules.AddLast(x));
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

            foreach (var module in Modules)
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

            foreach (var module in this.Modules)
            {
                collection = module.ApplyExpression(collection, settings);
                resultObject = module.ConstructResult(collection, resultObject);
            }

            return resultObject;
        }
    }
}
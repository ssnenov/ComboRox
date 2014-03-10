using System.Collections.Generic;
using ComboRox.Core.Filters;

namespace ComboRox.Core
{
    public class BuiltInModules
    {
        public static Dictionary<string, IModule> Modules = new Dictionary<string, IModule>();

        public BuiltInModules()
        {
            IModule filtersModule = new FiltersModule();
            Modules.Add(filtersModule.ModuleName, filtersModule);
        }
    }
}
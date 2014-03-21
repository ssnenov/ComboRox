using System.Collections.Generic;
using ComboRox.Core.Filters;
using ComboRox.Core.Pagination;

namespace ComboRox.Core
{
    public class BuiltInModules
    {
        private static Dictionary<string, IModule> modules;

        public static Dictionary<string, IModule> GetModules()
        {
            modules = new Dictionary<string, IModule>();

            IModule filtersModule = new FiltersModule();
            IModule paginationModule = new PaginationModule();

            modules.Add(filtersModule.ModuleName, filtersModule);
            modules.Add(paginationModule.ModuleName, paginationModule);

            return modules;
        }
    }
}
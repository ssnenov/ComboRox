using System.Collections.Generic;
using ComboRox.Core.Filters;
using ComboRox.Core.Pagination;

namespace ComboRox.Core
{
    public class BuiltInModules
    {
        private static Dictionary<string, IModule> _modules;

        public static Dictionary<string, IModule> GetModules()
        {
            _modules = new Dictionary<string, IModule>();

            IModule filtersModule = new FiltersModule();
            IModule paginationModule = new PaginationModule();

            _modules.Add(filtersModule.ModuleName, filtersModule);
            _modules.Add(paginationModule.ModuleName, paginationModule);

            return _modules;
        }
    }
}
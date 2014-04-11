using System.Collections.Generic;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Models
{
    public class ModulesSettings : IModulesSettings
    {
        public List<Filter> Filters { get; set; }

        public List<SortingObject> Sorting { get; set; }

        public PaginationObject Pagination { get; set; }
    }
}
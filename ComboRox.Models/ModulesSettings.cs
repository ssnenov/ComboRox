using System.Collections.Generic;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Models
{
    public class ModulesSettings : IModulesSettings
    {
        public List<Filter> Filters { get; set; }

        public SortingObject Sorting { get; set; }

        public PaginationObject Pagination { get; set; }
    }
}
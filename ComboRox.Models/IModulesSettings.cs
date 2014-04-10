using System.Collections.Generic;
using ComboRox.Models.JsonObjects;

namespace ComboRox.Models
{
    public interface IModulesSettings
    {
        List<Filter> Filters { get; set; }

        SortingObject Sorting { get; set; }

        PaginationObject Pagination { get; set; }
    }
}
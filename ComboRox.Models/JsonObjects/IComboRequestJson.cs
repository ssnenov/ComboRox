using System.Collections.Generic;

namespace ComboRox.Models.JsonObjects
{
    public interface IComboRequestJson
    {
        List<FilterObject> Filters { get; set; }

        List<SortingObject> Sorting { get; set; }

        PaginationObject Pagination { get; set; }
    }
}
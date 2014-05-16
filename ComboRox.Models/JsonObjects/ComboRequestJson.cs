using System.Collections.Generic;

namespace ComboRox.Models.JsonObjects
{
    public class ComboRequestJson : IComboRequestJson
    {
        public List<FilterObject> Filters { get; set; }

        public List<SortingObject> Sorting { get; set; } 

        public PaginationObject Pagination { get; set; }
    }
}
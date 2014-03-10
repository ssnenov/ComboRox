using System.Collections.Generic;

namespace ComboRox.Models.JsonObjects
{
    public class ComboRequestJson : IComboRequestJson
    {
        public List<FilterObject> Filters { get; set; }

        public PaginationObject Pagination { get; set; }
    }
}
using System.Collections.Generic;
using ComboRox.Models.Enums;

namespace ComboRox.Models
{
    public class Filter
    {
        public Operator Operator { get; set; }

        public string PropertyName { get; set; }

        public object Value { get; set; }

        public List<Filter> OrFilters { get; set; }
    }
}
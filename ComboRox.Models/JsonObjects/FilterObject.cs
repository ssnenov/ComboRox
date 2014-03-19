using System.Collections.Generic;

namespace ComboRox.Models.JsonObjects
{
    public class FilterObject
    {
        public string Op { get; set; }

        public string Prop { get; set; }

        public object Value { get; set; }

        public List<FilterObject> Or { get; set; }
    }
}
﻿using System.Collections.Generic;

namespace ComboRox.Models.JsonObjects
{
    public class FilterObject
    {
        public string op { get; set; }

        public string prop { get; set; }

        public object value { get; set; }

        public List<FilterObject> or { get; set; }
    }
}
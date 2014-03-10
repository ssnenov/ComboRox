﻿using System.Collections.Generic;

namespace ComboRox.Models.JsonObjects
{
    public interface IComboRequestJson
    {
        List<FilterObject> Filters { get; set; }

        PaginationObject Pagination { get; set; }
    }
}
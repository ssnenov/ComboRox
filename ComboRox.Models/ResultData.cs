﻿using System.Collections;

namespace ComboRox.Models
{
    public class ResultData : IResultData
    {
        public IEnumerable Data { get; set; }

        public int Total { get; set; }
    }
}
﻿using JWLibrary.Core.Data.TUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace JWLibrary.Core.Data {
    public class PagingResultDto<T> : ResultDto<T>, ITUIPagingBase {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int VisiblePages { get; set; }
        public int Page { get; set; }
    }
}
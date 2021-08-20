﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResultWithPaging<T> : IResult
    {
        T Data { get; }
        int TotalPage { get; set; }
        int CurrentPage { get; set; }
        int PerPage { get; set; }
    }
}

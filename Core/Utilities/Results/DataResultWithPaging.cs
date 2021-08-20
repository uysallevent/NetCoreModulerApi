using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResultWithPaging<T> : Result, IDataResultWithPaging<T>
    {
        public DataResultWithPaging(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public DataResultWithPaging(T data, int totalPage, int currentPage, int perPage, bool success) : base(success)
        {
            Data = data;
            TotalPage = totalPage;
            CurrentPage = currentPage;
            PerPage = perPage;
        }

        public T Data { get; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
    }
}
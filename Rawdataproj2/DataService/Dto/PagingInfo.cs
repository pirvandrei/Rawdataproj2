using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class PagingInfo
    {
        const int MaxPageSize = 25;
        private int _pageSize = 10;

        public int Page { get; set; }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > MaxPageSize) _pageSize = MaxPageSize;
                else _pageSize = value;
            }
        }
    }
}

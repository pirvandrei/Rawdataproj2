using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class PagingInfo
    {
        const int MaxPageSize = 25;
        private int _pageSize = 10;
        private int _page = 1;

        public int Page { get { return _page; } set { _page = value; } }
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

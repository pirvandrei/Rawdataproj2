using DataService;
using System;
using System.Collections.Generic;

namespace WebService.Models
{
    public static class PagingHelper
    {
        public static object GetPagingResult(PagingInfo pagingInfo, int total, IEnumerable<object> model, ReturnTypeConstants returnType, string prev, string next)
        {
            SetPaging(pagingInfo, total, out int pages, ref prev, ref next);

            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "prev", prev },
                { "next", next },
                { "currentpage", pagingInfo.Page },
                { "total", total },
                { "pages", pages },
                { returnType.ToString(), model }
            };

            return dictionary;
        }

        private static void SetPaging(PagingInfo pagingInfo, int total, out int pages, ref string prev, ref string next)
        {
            pages = (int)Math.Ceiling(total / (double)pagingInfo.PageSize);
            prev = pagingInfo.Page > 1 ? prev : null;
            next = pagingInfo.Page < pages ? next : null;
        }
    }

}

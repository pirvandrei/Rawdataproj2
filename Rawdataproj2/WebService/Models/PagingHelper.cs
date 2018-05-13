using DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace WebService.Models
{
    public static class PagingHelper
    {
        public static object GetPagingResult(PagingInfo pagingInfo, int total, IEnumerable<object> model, string returnType, string prev, string next)
        {
            SetPaging(pagingInfo, total, out int pages, ref prev, ref next);

            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "prev", prev },
                { "next", next },
                { "total", total },
                { "pages", pages },
                { returnType, model }
            };

            return dictionary;

            //return new
            //{
            //    Prev = prev,
            //    Next = next,
            //    Total = total,
            //    Pages = pages,
            //    Type = returnType,
            //    Elements = model
            //};

        }

        private static void SetPaging(PagingInfo pagingInfo, int total, out int pages, ref string prev, ref string next)
        {
            pages = (int)Math.Ceiling(total / (double)pagingInfo.PageSize);
            prev = pagingInfo.Page > 0 ? prev : null;
            next = pagingInfo.Page < pages - 1 ? next : null;
        }
    }

}
